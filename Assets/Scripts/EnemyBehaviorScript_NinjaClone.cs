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
        Vector2 RightTurnFacing = new Vector2(BattleTargetComponent.CurrentFacing.y, BattleTargetComponent.CurrentFacing.x * -1);

        int CloneCount = 0;
        if (!GridMovement.IsDirectionOccupied(RightTurnFacing))
        {
            Vector3 spawnPosition = this.transform.position;
            spawnPosition += new Vector3(RightTurnFacing.x, RightTurnFacing.y);
            GameObject NewClone = Instantiate(ClonePrefab, spawnPosition, Quaternion.identity);

            NewClone.GetComponent<BattleTarget>().CurrentFacing = RightTurnFacing;
            CloneCount += 1;
        }

        // Spawn behind
        if (!GridMovement.IsDirectionOccupied(RightTurnFacing * -1))
        {
            Vector3 spawnPosition = this.transform.position;
            spawnPosition += new Vector3(RightTurnFacing.x * -1, RightTurnFacing.y * -1);
            GameObject NewClone = Instantiate(ClonePrefab, spawnPosition, Quaternion.identity);

            NewClone.GetComponent<BattleTarget>().CurrentFacing = RightTurnFacing * -1;
            CloneCount += 1;
        }

        if (CloneCount == 0)
        {
            // Rotate clockwise
            BattleTargetComponent.CurrentFacing = new Vector2(BattleTargetComponent.CurrentFacing.y, BattleTargetComponent.CurrentFacing.x * -1);
        }

        // Strike!
        ECombatResult Combat = GridCombat.AttemptCombat(BattleTargetComponent.CurrentFacing, true);
        yield return new WaitForSeconds(GridCombat.MoveTime);
        if (Combat == ECombatResult.SUCCESS)
        {
            // Yield break means stop the coroutine completely.
            yield break;
        }
        // No attack.  Let's move instead.
        EMoveResult Move = GridMovement.AttemptMovement(BattleTargetComponent.CurrentFacing, true);
        yield return new WaitForSeconds(GridMovement.MoveTime);
        if (Move == EMoveResult.SUCCESS)
        {
            yield break;
        }

        // Let's turn around if we can't move or attack.
        BattleTargetComponent.CurrentFacing *= -1;

        yield return new WaitForSeconds(0.2f);
    }

    public virtual IEnumerator EndOfRoundBehavior()
    {
        yield return null;
    }


}
