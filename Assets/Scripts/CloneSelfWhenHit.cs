using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSelfWhenHit : MonoBehaviour
{
    public BattleTarget SelfBattleTarget;
    public GameObject ClonePrefab;
    void Start()
    {
        if (SelfBattleTarget == null) SelfBattleTarget = GetComponent<BattleTarget>();

        SelfBattleTarget.OnTakeDamage += HandleHitTaken;
    }

    void HandleHitTaken(BattleTarget MyCharacter, BattleTarget other)
    {
        if (other == MyCharacter)
        {
            return;
        }

        if (MyCharacter.IsStunned)
        {
            // Do nothing if stunned.
            return;
        }

        // SPAWN NEW CLONES

        Vector2 IncomingAttackVector = MyCharacter.transform.position - other.transform.position;
        IncomingAttackVector.Normalize();

        // Check adjacent.

        GridMovementComponent MovementComponent = MyCharacter.GetComponent<GridMovementComponent>();

        Vector2 FirstSpawnVector = new Vector2(IncomingAttackVector.y, IncomingAttackVector.x * -1);


        if (!MovementComponent.IsDirectionOccupied(FirstSpawnVector))
        {
            Vector3 spawnPosition = this.transform.position;
            spawnPosition += new Vector3(FirstSpawnVector.x, FirstSpawnVector.y);
            GameObject NewClone = Instantiate(ClonePrefab, spawnPosition, Quaternion.identity);

            NewClone.GetComponent<BattleTarget>().CurrentFacing = MyCharacter.CurrentFacing;
            // Start stunned
            NewClone.GetComponent<BattleTarget>().TakeStun(null);
        }

        // Prevent death.
        MyCharacter.HitsUntilDeath += 1;
        MyCharacter.TakeStun(other);
        Vector2 MovementVector = FirstSpawnVector * -1;

        if (!MovementComponent.IsDirectionOccupied(MovementVector))
        {
            MovementComponent.AttemptMovement(MovementVector, true);
        }
    }


}
