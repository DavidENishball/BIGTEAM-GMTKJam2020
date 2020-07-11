using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControlScript : MonoBehaviour
{

    public delegate void HeroControlScriptDelegate(HeroControlScript source);
    public delegate void HeroControlScriptMoveResultDelegate(HeroControlScript source, EPlayerMoves chosenMove, EMoveResult result);

    public HeroControlScriptMoveResultDelegate OnMoveCompleted;

    protected EPlayerMoves MoveInProgress = EPlayerMoves.NONE;

    public GridMovementComponent MovementComponent;

     private void Awake()
    {
        if (MovementComponent == null)
        {
            MovementComponent = gameObject.GetComponent<GridMovementComponent>();
        }
    }
    public List<EPlayerMoves> MoveQueue = new List<EPlayerMoves>();


    public EMoveResult ProcessNextQueuedMove()
    {
        if (MoveQueue.Count == 0)
        {
            return EMoveResult.SYSTEM_ERROR;
        }

        if (MoveInProgress != EPlayerMoves.NONE)
        {
            return EMoveResult.NEUTRAL;
        }
        EPlayerMoves ChosenMove = MoveQueue[0];
        MoveQueue.RemoveAt(0);

        return ProcessMoveEnum(ChosenMove);
    }

    public EMoveResult ProcessMoveEnum(EPlayerMoves InputMove)
    {
        if (MoveInProgress != EPlayerMoves.NONE)
        {
            return EMoveResult.NEUTRAL;
        }

        MoveInProgress = InputMove;
        switch (InputMove)
        {
            case EPlayerMoves.UP:
                MovementComponent.OnMoveCompleted += HandleMovementComponentDone;
                MovementComponent.AttemptMovement(new Vector2(0, 1));
                return EMoveResult.SUCCESS;
                break;
            case EPlayerMoves.RIGHT:
                MovementComponent.OnMoveCompleted += HandleMovementComponentDone;
                MovementComponent.AttemptMovement(new Vector2(1, 0));
                return EMoveResult.SUCCESS;
                break;
            case EPlayerMoves.LEFT:
                MovementComponent.OnMoveCompleted += HandleMovementComponentDone;
                MovementComponent.AttemptMovement(new Vector2(-1, 0));
                return EMoveResult.SUCCESS;
                break;
            case EPlayerMoves.DOWN:
                MovementComponent.OnMoveCompleted += HandleMovementComponentDone;
                MovementComponent.AttemptMovement(new Vector2(0, -1));
                return EMoveResult.SUCCESS;
                break;

            default:
                Debug.LogError("Could not process move " + InputMove.ToString());
                return EMoveResult.SYSTEM_ERROR;
                break;
        }

        return EMoveResult.NEUTRAL;
    }




    public void HandleMovementComponentDone(GridMovementComponent source, EMoveResult result)
    {
        source.OnMoveCompleted -= HandleMovementComponentDone;
        EPlayerMoves finishedMove = MoveInProgress;
        MoveInProgress = EPlayerMoves.NONE;
        OnMoveCompleted.Invoke(this, MoveInProgress, result);
    }
}
