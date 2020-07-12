using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class State_LevelIntroduction : IState
{
    GameManager owner;

    public State_LevelIntroduction(GameManager owner) { this.owner = owner; }

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

            RunningEnemyBehaviors.Remove(NewScript);
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