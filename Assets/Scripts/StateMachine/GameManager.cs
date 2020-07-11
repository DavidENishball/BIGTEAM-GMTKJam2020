using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class for controlling game state.
public class GameManager : MonoBehaviour
{
    public HeroControlScript Hero;
	public ActionBar ActionUI;

    public StateMachine stateMachine = new StateMachine();

    // Start is called before the first frame update
    void Start()
    {
    // TODO: menu mode, game over, fighting, etc.        
        if (Hero == null)
        {
            Hero = FindObjectOfType<HeroControlScript>();
        }
		if (ActionUI == null)
		{
			ActionUI = FindObjectOfType<ActionBar>();
		}

		ActionUI.Init(Hero);


		stateMachine.ChangeState(new State_PlanPlayerMoves(this));
    }


    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
