using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControlScript : MonoBehaviour
{

    public GridMovementComponent MovementComponent;

     private void Awake()
    {
        if (MovementComponent == null)
        {
            MovementComponent = gameObject.GetComponent<GridMovementComponent>();
        }
    }


    

    public EMoveResult ProcessMoveEnum(EPlayerMoves InputMove)
    {
        switch (InputMove)
        {
            case EPlayerMoves.UP:
                MovementComponent.DoMove (new Vector2(0, 1));
                return EMoveResult.SUCCESS;
                break;
            case EPlayerMoves.RIGHT:
                MovementComponent.DoMove(new Vector2(1, 0));
                return EMoveResult.SUCCESS;
                break;
            case EPlayerMoves.LEFT:
                MovementComponent.DoMove(new Vector2(-1, 0));
                return EMoveResult.SUCCESS;
                break;
            case EPlayerMoves.DOWN:
                MovementComponent.DoMove(new Vector2(0, -1));
                return EMoveResult.SUCCESS;
                break;

            default:
                Debug.LogError("Could not process move " + InputMove.ToString());
                return EMoveResult.SYSTEM_ERROR;
                break;
        }

        return EMoveResult.NEUTRAL;
    }
}
