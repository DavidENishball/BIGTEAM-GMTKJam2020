using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlanPlayerMoves : IState
{
    GameManager owner;
    Coroutine runningCoroutine;
    public State_PlanPlayerMoves(GameManager owner) { this.owner = owner; }

    public void Enter()
    {

        runningCoroutine = owner.StartCoroutine(ExecutionPhase());
        // TODO: play end animation.
    }


    public IEnumerator ExecutionPhase()
    {
        // Make a player do a move.
        while (owner.Hero.MoveQueue.Count > 0)
        {
            if (owner.Hero.MoveQueue.Count > 0)
            {
                bool trigger = false;
                //(HeroControlScript source, EPlayerMoves chosenMove, EMoveResult result);

                HeroControlScript.HeroControlScriptMoveResultDelegate waitForMoveDoneLambda = (HeroControlScript source, EPlayerMoves chosenMove, EMoveResult result) => trigger = true;
                owner.Hero.OnMoveCompleted += waitForMoveDoneLambda;
                if (owner.Hero.ProcessNextQueuedMove() == EMoveResult.SYSTEM_ERROR)
                {
                    owner.Hero.OnMoveCompleted -= waitForMoveDoneLambda;
                    break;
                }
                else
                {
                    yield return new WaitUntil(() => trigger);
                    owner.Hero.OnMoveCompleted -= waitForMoveDoneLambda;
                }
                
            }
        }
        //  CHANGE TO SOMETHING ELSE owner.stateMachine.ChangeState();
    }

    public void Update()
    {
        // INPUT
        if (Input.GetButtonDown("Up"))
        {
            owner.Hero.MoveQueue.Add(EPlayerMoves.UP);
        }
        else if (Input.GetButtonDown("Down"))
        {
            owner.Hero.MoveQueue.Add(EPlayerMoves.DOWN);
        }
        else if (Input.GetButtonDown("Left"))
        {
            owner.Hero.MoveQueue.Add(EPlayerMoves.LEFT);
        }
        else if (Input.GetButtonDown("Right"))
        {
            owner.Hero.MoveQueue.Add(EPlayerMoves.RIGHT);
        }
        else if (Input.GetButtonDown("Sword"))
        {
            owner.Hero.MoveQueue.Add(EPlayerMoves.SWORD);
        }
        else if (Input.GetButtonDown("Sheathe"))
        {
            owner.Hero.MoveQueue.Add(EPlayerMoves.SHEATHE);
        }
        else if (Input.GetButtonDown("Wait"))
        {
            owner.Hero.MoveQueue.Add(EPlayerMoves.WAIT);
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            owner.Hero.MoveQueue.RemoveAt(owner.Hero.MoveQueue.Count - 1);
        }
    }

    public void Exit()
    {
        owner.Hero.MoveQueue.Clear();
    }
}