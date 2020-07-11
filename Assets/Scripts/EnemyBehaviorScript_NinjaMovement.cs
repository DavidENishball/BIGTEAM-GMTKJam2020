using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorScript_NinjaMovement : EnemyBehaviorScript
{
    public BattleTarget BattleTargetComponent;
    public GridMovementComponent GridMovement;
    public GridCombatComponent GridCombat;
    public override IEnumerator StartOfRoundBehavior()
    {
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

        // Let's turn around.
        BattleTargetComponent.CurrentFacing *= -1;

        // Done
    }

    public virtual IEnumerator EndOfRoundBehavior()
    {
        yield return null;
    }


}
