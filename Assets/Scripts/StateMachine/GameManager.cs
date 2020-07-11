using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class for controlling game state.
public class GameManager : MonoBehaviour
{
    public HeroControlScript Hero;

    public StateMachine stateMachine = new StateMachine();

    // Start is called before the first frame update
    void Start()
    {
    // TODO: menu mode, game over, fighting, etc.        
        if (Hero == null)
        {
            Hero = FindObjectOfType<HeroControlScript>();
        }

        stateMachine.ChangeState(new State_PlanPlayerMoves(this));
    }


    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
