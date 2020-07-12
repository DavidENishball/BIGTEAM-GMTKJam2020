using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class State_PlayerLose : IState
{
    GameManager owner;
    Coroutine runningCoroutine;
    public State_PlayerLose(GameManager owner) { this.owner = owner; }


    public void Enter()
    {
        Debug.Log("Entering State_PlayerLose");
        
        // AXE Do game over here.
        // Sample:
        if (owner.GameOverUI != null)
        {
            owner.GameOverUI.SetActive(true);
        }
        // Grug note: One day I need to tweak this pattern so I can embed default values into game states, instead of tossing everything onto the Game Manager.
    }


    public void Update()
    {

    }

    public void Exit()
    {
        if (owner.GameOverUI != null)
        {
            owner.GameOverUI.SetActive(false);
        }
    }

}