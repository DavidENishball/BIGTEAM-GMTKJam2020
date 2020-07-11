using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_DirectHeroControl : MonoBehaviour
{

    public HeroControlScript HeroControl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Test script for arrow key movement.

        if (Input.GetButtonDown("Up"))
        {
            HeroControl.ProcessMoveEnum(EPlayerMoves.UP);
        }
        else if (Input.GetButtonDown("Down"))
        {
            HeroControl.ProcessMoveEnum(EPlayerMoves.DOWN);
        }
        else if(Input.GetButtonDown("Left"))
        {
            HeroControl.ProcessMoveEnum(EPlayerMoves.LEFT);
        }
        else if(Input.GetButtonDown("Right"))
        {
            HeroControl.ProcessMoveEnum(EPlayerMoves.RIGHT);
        }
        else if (Input.GetButtonDown("Sword"))
        {
            HeroControl.ProcessMoveEnum(EPlayerMoves.SWORD);
        }
        else if (Input.GetButtonDown("Sheathe"))
        {
            HeroControl.ProcessMoveEnum(EPlayerMoves.SHEATHE);
        }
        else if (Input.GetButtonDown("Wait"))
        {
            HeroControl.ProcessMoveEnum(EPlayerMoves.WAIT);
        }

    }
}
