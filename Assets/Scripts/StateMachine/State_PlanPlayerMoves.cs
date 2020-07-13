using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlanPlayerMoves : IState
{
    public GameManager owner;
    Coroutine runningCoroutine;

    public float TimeRemaining = 999.0f;
    public bool IsTimeLimited = true;
    public State_PlanPlayerMoves(GameManager argOwner) { this.owner = argOwner; }

    public void Enter()
    {
        Debug.Log("Entering State_PlanPlayerMoves");
        IsTimeLimited = owner.UseTimer;
        if (IsTimeLimited)
        {
            TimeRemaining = owner.PlanningTimeForPlayer;
        }
    }


    public void Update()
    {
        // INPUT
        if (Input.GetButtonDown("Up"))
        {
            owner.Hero.AddMoveToQueue(EPlayerMoves.UP);
        }
        else if (Input.GetButtonDown("Down"))
        {
            owner.Hero.AddMoveToQueue(EPlayerMoves.DOWN);
        }
        else if (Input.GetButtonDown("Left"))
        {
            owner.Hero.AddMoveToQueue(EPlayerMoves.LEFT);
        }
        else if (Input.GetButtonDown("Right"))
        {
            owner.Hero.AddMoveToQueue(EPlayerMoves.RIGHT);
        }
        else if (Input.GetButtonDown("Sword"))
        {
            owner.Hero.AddMoveToQueue(EPlayerMoves.SWORD);
        }
        else if (Input.GetButtonDown("Sheathe"))
        {
            owner.Hero.AddMoveToQueue(EPlayerMoves.SHEATHE);
        }
        else if (Input.GetButtonDown("Wait"))
        {
            owner.Hero.AddMoveToQueue(EPlayerMoves.WAIT);
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            owner.Hero.RemoveLastQueuedMove();
        }

        if (IsTimeLimited)
        {
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining <= 0)
            {
                owner.stateMachine.ChangeState(new State_ExecutingMoves(owner));
            }
        }

    }

    public void Exit()
    {
         //Nothing for now;
    }
}