using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTarget : MonoBehaviour
{
    public Vector2 CurrentFacing = new Vector2(0, 1);
    public int HitsUntilDeath = 1;
    public GameObject TempDeathEffectPrefab;
    public bool IsDead()
    {
        return HitsUntilDeath <= 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeHit(BattleTarget Source = null)
    {
        HitsUntilDeath -= 1;
        if (HitsUntilDeath <= 0)
        {
            Kill(Source);
        }
    }

    public virtual void Kill(BattleTarget KillSource = null)
    {

        // disable collider first.

        Collider2D collider = gameObject.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        // Make this script as disabled.
        this.enabled = false;

        // Play effects and stuff here.
        // Temp effect

        Instantiate(TempDeathEffectPrefab, this.transform.position - Vector3.forward * 2, transform.rotation);

       
        // For now, just destroy.
        Destroy(gameObject);
         
    }
}
