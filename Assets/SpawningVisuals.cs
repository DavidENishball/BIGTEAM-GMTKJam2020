using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpawningVisuals : MonoBehaviour
{
    public GameObject DefaultSmokeEffect;
    public Coroutine CurrentVisualsCoroutine;
    public float WaitTime = 0.4f;
    public bool SuppressActivation = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!SuppressActivation)
        {
            CurrentVisualsCoroutine = StartCoroutine(DoSpawningVisuals());
        }
    }


    // AXE: subclass this class and override this function for variations.
    public virtual IEnumerator DoSpawningVisuals()
    {
        yield return null; // Stall one frame at the start, just in case.

        if (DefaultSmokeEffect != null)
        {
            var effectObject = Instantiate(DefaultSmokeEffect, transform.position, Quaternion.identity) as GameObject;
            var OSE = effectObject.GetComponent<OneShotEffect>();
        }


        // Do a fadein

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            yield break;
        }

        sprite.color = Color.black;

        // AXE: Turns out you can yield for Tweens.  nice.
        yield return sprite.DOColor(Color.white, WaitTime);

        // Done
          
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
