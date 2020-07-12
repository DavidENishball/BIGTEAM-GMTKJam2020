using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
public class State_LevelIntroduction : IState
{
    GameManager owner;

    public State_LevelIntroduction(GameManager owner) { this.owner = owner; }

    public List<SpawningVisuals> RunningSpawnVisuals;
    public void Enter()
    {
        Debug.Log("Entering State_RoundStart");

        RunningSpawnVisuals = MonoBehaviour.FindObjectsOfType<SpawningVisuals>().Where(x => x.enabled).ToList();
        foreach (SpawningVisuals vis in RunningSpawnVisuals)
        {
            if (vis.CurrentVisualsCoroutine != null)
            {
                vis.StopCoroutine(vis.CurrentVisualsCoroutine);
            }
            vis.SuppressActivation = true; // Prevent them from activating on their own.
            vis.SetInitialSpawnAppearance();
        }

        owner.StartCoroutine(PlayLevelIntroduction());
    }

    public IEnumerator PlayLevelIntroduction()
    {
        // AXE: all you my lad.

        // Here's Grug's implementation:
       

        // Ensure loading is done.
        yield return new WaitForSeconds(0.4f);

        yield return owner.LevelIntroductionUI.transform.DOLocalMove(new Vector3(0, -110.0f), 0.3f).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(2.0f);

        // Kick off unit visuals seperately.
        Coroutine characterCoroutine = owner.StartCoroutine(PlaySpawnVisualsStaggered());

        // Swoop out.
        yield return owner.LevelIntroductionUI.transform.DOLocalMove(Vector3.zero + Vector3.down * 300, 0.3f).SetEase(Ease.InExpo);

        // Wait for all intros to finish.
        yield return characterCoroutine;
        // Done I guess.

        owner.stateMachine.ChangeState(new State_PlanPlayerMoves(owner));
        yield break;
    }

    public IEnumerator PlaySpawnVisualsStaggered()
    {
       
        RunningSpawnVisuals.Shuffle();

        foreach (SpawningVisuals iterVisual in RunningSpawnVisuals)
        {
            iterVisual.StartCoroutine(iterVisual.DoSpawningVisuals());
            yield return new WaitForSeconds(Random.Range(0.0f, 0.1f));
        }
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }

}