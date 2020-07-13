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
        Debug.Log("Starting round end behaviors with " + RunningEnemyBehaviors.Count.ToString());
        while (RunningEnemyBehaviors.Count > 0)
        {
            EnemyBehaviorScript NewScript = RunningEnemyBehaviors[0];
            if (NewScript != null)
            {
                // Kludge: Skip dead enemies.
                if (NewScript.GetComponent<BattleTarget>().enabled)
                {
                    Debug.Log("Starting script for unit " + NewScript.gameObject.name);
                    // Target is being destroyed while coroutine is running.
                    Coroutine NewCoroutine = NewScript.StartCoroutine(NewScript.EndOfRoundBehavior());
                    yield return NewCoroutine;
                }
                else
                {
                    Debug.Log("Skipping unit " + NewScript.gameObject.name + " because it is marked dead");
                }
            }
            RunningEnemyBehaviors.RemoveAt(0);
            Debug.Log("Remaining behaviors: " + RunningEnemyBehaviors.Count);
        }

        if (owner.OnRoundEnd != null) owner.OnRoundEnd.Invoke(owner);

        if (owner.IsLevelComplete())
        {
            Debug.Log(("Completing Level"));
            owner.stateMachine.ChangeState(new State_LevelComplete(owner));
        }
        else
        {
            Debug.Log("Starting new round");
            owner.stateMachine.ChangeState(new State_RoundStart(owner));
        }
    }

    public void Update()
    {
        if (RunningEnemyBehaviors.Count == 0)
        {
            // Safety check?
            if (owner.IsLevelComplete())
            {
                owner.stateMachine.ChangeState(new State_LevelComplete(owner));
            }
            else
            {
                owner.stateMachine.ChangeState(new State_RoundStart(owner));
            }
        }
    }

    public void Exit()
    {
        // unbind all.
        RunningEnemyBehaviors.Clear();
    }

}