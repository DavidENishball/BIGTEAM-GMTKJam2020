using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class State_RoundEnd : IState
{
    GameManager owner;
    Coroutine runningCoroutine;
    public State_RoundEnd(GameManager owner) { this.owner = owner; }

    public List<EnemyBehaviorScript> RunningEnemyBehaviors;

    public void Enter()
    {
        Debug.Log("Entering State_RoundEnd");

        RunningEnemyBehaviors = MonoBehaviour.FindObjectsOfType<EnemyBehaviorScript>().Where(x => x.enabled).ToList();
        owner.StartCoroutine(ProcessAllBehaviors());
    }

    public IEnumerator ProcessAllBehaviors()
    {
        while (RunningEnemyBehaviors.Count > 0)
        {
            EnemyBehaviorScript NewScript = RunningEnemyBehaviors[0];
            if (NewScript != null)
            {
                yield return NewScript.StartCoroutine(NewScript.EndOfRoundBehavior());
            }
            RunningEnemyBehaviors.RemoveAt(0);
        }

        if (owner.OnRoundEnd != null) owner.OnRoundEnd.Invoke(owner);

        if (owner.IsLevelComplete())
        {
            owner.stateMachine.ChangeState(new State_LevelComplete(owner));
        }
        else
        {
            owner.stateMachine.ChangeState(new State_RoundStart(owner));
        }
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