﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// A class for controlling game state.
public class GameManager : MonoBehaviour
{
    public HeroControlScript Hero;
	public ActionBar ActionUI;

    public StateMachine stateMachine = new StateMachine();

    public delegate void GameManagerDelegate(GameManager source);

    public GameManagerDelegate OnRoundStart;
    public GameManagerDelegate OnRoundEnd;

    public float PlanningTimeForPlayer = 4.0f;
    public bool UseTimer = true;

    public int MaximumSizeOfPlayerInputQueue = 10;

    public GameObject LevelCompleteUI;
    public GameObject GameOverUI;
    public GameObject LevelIntroductionUI;

	public ScreenShakeEffect CamShake;

    public string AdvanceToThisSceneAfterLevelComplete;

    // Start is called before the first frame update
    void Start()
    {
    // TODO: menu mode, game over, fighting, etc.        
        if (Hero == null)
        {
            Hero = FindObjectOfType<HeroControlScript>();
            Hero.MaximumQueuedMoves = MaximumSizeOfPlayerInputQueue;
        }
		if (ActionUI == null)
		{
			ActionUI = FindObjectOfType<ActionBar>();
		}

		ActionUI.Init(Hero);

        // FIRST STATE
		stateMachine.ChangeState(new State_LevelIntroduction(this));
    }


    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    public bool IsLevelComplete()
    {
        HashSet<BattleTarget> targetSet = new HashSet<BattleTarget>(FindObjectsOfType<BattleTarget>());

        // Ignore player
        targetSet.Remove(Hero.GetComponent<BattleTarget>());

        // Linq magic.
        int LivingTargets = targetSet.Where(x => x.enabled && x.IsDead() == false).Count();

        return LivingTargets <= 0;


    }
}
