using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorScript_NinjaClone : EnemyBehaviorScript
{
    public BattleTarget BattleTargetComponent;
    public GridMovementComponent GridMovement;
    public GridCombatComponent GridCombat;

    public GameObject ClonePrefab;

    public override IEnumerator StartOfRoundBehavior()
    {
        // Spawn Clone

        if (BattleTargetComponent == null)
        {
            BattleTargetComponent = GetComponent<BattleTarget>();
        }
        if (GridMovement == null)
        {
            GridMovement = GetComponent<GridMovementComponent>();
        }
        if (GridCombat == null)
        {
            GridCombat = GetComponent<GridCombatComponent>();
        }

        int CloneCount = 0;
        if (!GridMovement.IsDirectionOccupied(BattleTargetComponent.CurrentFacing))
        {
            Vector3 spawnPosition = this.transform.position;
            spawnPosition += new Vector3(BattleTargetComponent.CurrentFacing.x, BattleTargetComponent.CurrentFacing.y);
            GameObject NewClone = Instantiate(ClonePrefab, spawnPosition, Quaternion.identity);

            NewClone.GetComponent<BattleTarget>().CurrentFacing = BattleTargetComponent.CurrentFacing;
            CloneCount += 1;
        }

        // Spawn behind
        if (!GridMovement.IsDirectionOccupied(BattleTargetComponent.CurrentFacing * -1))
        {
            Vector3 spawnPosition = this.transform.position;
            spawnPosition += new Vector3(BattleTargetComponent.CurrentFacing.x * -1, BattleTargetComponent.CurrentFacing.y * -1);
            GameObject NewClone = Instantiate(ClonePrefab, spawnPosition, Quaternion.identity);

            NewClone.GetComponent<BattleTarget>().CurrentFacing = BattleTargetComponent.CurrentFacing * -1;
            CloneCount += 1;
        }

        if (CloneCount == 0)
        {
            // Rotate clockwise
            BattleTargetComponent.CurrentFacing = new Vector2(BattleTargetComponent.CurrentFacing.y, BattleTargetComponent.CurrentFacing.x * -1);
        }

        yield return new WaitForSeconds(0.2f);
    }

    public virtual IEnumerator EndOfRoundBehavior()
    {
        yield return null;
    }


}
