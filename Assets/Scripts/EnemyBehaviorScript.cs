using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorScript : MonoBehaviour
{
    public GameManager manager;

    public delegate void EnemyBehaviorScriptDelegate(EnemyBehaviorScript source);

    public EnemyBehaviorScriptDelegate OnBehaviorFinished;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();

        

    }

    public virtual IEnumerator StartOfRoundBehavior()
    {
        yield return null;
    }

    public virtual IEnumerator EndOfRoundBehavior()
    {
        yield return null;
    }


}
