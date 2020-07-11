using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_ExecutingMoves : IState
{
    GameManager owner;
    Coroutine runningCoroutine;
    public State_ExecutingMoves(GameManager owner) { this.owner = owner; }

    public void Enter()
    {
        Debug.Log("Entering State_ExecutingMoves");
        runningCoroutine = owner.StartCoroutine(ExecutionPhase());
		// TODO: play end animation.

		owner.Hero.NotifyPlayQueue();
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
        owner.stateMachine.ChangeState(new State_PlanPlayerMoves(owner));
    }

    public void Update()
    {

    }

    public void Exit()
    {
        owner.StopCoroutine(runningCoroutine);
        runningCoroutine = null;
		owner.Hero.ClearQueue();
    }
}