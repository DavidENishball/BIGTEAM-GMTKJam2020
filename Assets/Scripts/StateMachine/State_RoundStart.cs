using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class State_RoundStart : IState
{
    GameManager owner;
    Coroutine runningCoroutine;
    public State_RoundStart(GameManager owner) { this.owner = owner; }

    public List<EnemyBehaviorScript> RunningEnemyBehaviors;

    public void Enter()
    {
        Debug.Log("Entering State_RoundStart");


        RunningEnemyBehaviors = MonoBehaviour.FindObjectsOfType<EnemyBehaviorScript>().Where(x => x.enabled).ToList();
        owner.StartCoroutine(ProcessAllBehaviors());
    }

    public IEnumerator ProcessAllBehaviors()
    {
        if (owner.OnRoundStart != null) owner.OnRoundStart.Invoke(owner);

        while (RunningEnemyBehaviors.Count > 0)
        {
            EnemyBehaviorScript NewScript = RunningEnemyBehaviors[0];
            if (NewScript != null)
            {
                yield return NewScript.StartCoroutine(NewScript.StartOfRoundBehavior());
            }
            RunningEnemyBehaviors.RemoveAt(0);
        }

        
        owner.stateMachine.ChangeState(new State_PlanPlayerMoves(owner));
        yield return null;
    }

    public void Update()
    {

    }

    public void Exit()
    {
        // unbind all.
        RunningEnemyBehaviors.Clear();
    }

}