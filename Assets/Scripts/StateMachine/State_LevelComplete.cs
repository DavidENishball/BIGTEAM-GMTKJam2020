using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class State_LevelComplete : IState
{
    GameManager owner;
    Coroutine runningCoroutine;
    public GameObject UIToShow;
    public State_LevelComplete(GameManager owner) { this.owner = owner; }


    public void Enter()
    {
        Debug.Log("Entering State_LevelComplete");
        
        // AXE Do end credits here.
        // Sample:
        if (owner.LevelCompleteUI != null)
        {
            owner.LevelCompleteUI.SetActive(true);
        }
        // Grug note: One day I need to tweak this pattern so I can embed default values into game states, instead of tossing everything onto the Game Manager.
    }


    public void Update()
    {

    }

    public void Exit()
    {

    }

}