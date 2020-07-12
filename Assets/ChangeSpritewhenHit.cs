using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpritewhenHit : MonoBehaviour
{

    public Sprite HitSprite;

    public Sprite CachedOriginalSprite;

    protected SpriteRenderer cachedRenderer;

    public BattleTarget foundCharacter;

    // Start is called before the first frame update
    void Start()
    {
        cachedRenderer = GetComponent<SpriteRenderer>();
        CachedOriginalSprite = cachedRenderer.sprite;

        foundCharacter = GetComponent<BattleTarget>();
        foundCharacter.OnTakeDamage += HandleCharacterHit;
        FindObjectOfType<GameManager>().OnRoundStart += HandleCharacterRecover;

    }

    public void HandleCharacterHit(BattleTarget MyCharacter, BattleTarget other)
    {
        if (cachedRenderer != null)
        {
            cachedRenderer.sprite = HitSprite;
        }
    }

    public void HandleCharacterRecover(GameManager source)
    {
        if (foundCharacter != null && foundCharacter.enabled) // Don't recover if the character is dead.
        {
            if (cachedRenderer != null)
            {
                cachedRenderer.sprite = CachedOriginalSprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
