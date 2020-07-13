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
                // Kludge: Skip dead enemies.
                if (NewScript.GetComponent<BattleTarget>().enabled)
                {
                    Debug.Log("Starting RoundStart script for unit " + NewScript.gameObject.name);
                    // Target is being destroyed while coroutine is running.
                    Coroutine NewCoroutine = NewScript.StartCoroutine(NewScript.StartOfRoundBehavior());
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

        // KLUDGE!~  Don't change state if this isn't the current state.
        if (owner.stateMachine.GetState() == this)
        {
            owner.stateMachine.ChangeState(new State_PlanPlayerMoves(owner));
        }
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