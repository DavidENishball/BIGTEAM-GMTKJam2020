using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpritewhenStunned : MonoBehaviour
{

    public Sprite StunnedSprite;

    public Sprite CachedOriginalSprite;

    protected SpriteRenderer cachedRenderer;

    public BattleTarget foundCharacter;

    // Start is called before the first frame update
    void Awake()
    {
        cachedRenderer = GetComponent<SpriteRenderer>();
        CachedOriginalSprite = cachedRenderer.sprite;

        foundCharacter = GetComponent<BattleTarget>();
        foundCharacter.OnStunned += HandleCharacterStunned;
        foundCharacter.OnNotStunned += HandleCharacterNotStunned;
    }



    public void HandleCharacterStunned(BattleTarget MyCharacter, BattleTarget other)
    {
        if (cachedRenderer != null)
        {
            cachedRenderer.sprite = StunnedSprite;
        }
    }

    public void HandleCharacterNotStunned(BattleTarget MyCharacter)
    {
        if (cachedRenderer != null)
        {
            cachedRenderer.sprite = CachedOriginalSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
