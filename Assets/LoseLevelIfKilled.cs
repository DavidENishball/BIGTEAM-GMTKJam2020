using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseLevelIfKilled : MonoBehaviour
{
    public BattleTarget MyCharacter;
    // Start is called before the first frame update
    void Start()
    {
        MyCharacter = GetComponent<BattleTarget>();

        MyCharacter.OnKilled += HandleCharacterKilled;
    }

    public void HandleCharacterKilled(BattleTarget MyCharacter, BattleTarget other)
    {
        FindObjectOfType<GameManager>().stateMachine.ChangeState(new State_PlayerLose(FindObjectOfType<GameManager>()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
