using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Text_SynchronizeWithTimer : MonoBehaviour
{
    GameManager manager;
    public Text TheText;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        TheText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        State_PlanPlayerMoves CurrentState = manager.stateMachine.GetState() as State_PlanPlayerMoves;

        if (CurrentState != null)
        {
            TheText.text = CurrentState.TimeRemaining.ToString();
        }
        else
        {
            TheText.text = "";
        }
    }
}
