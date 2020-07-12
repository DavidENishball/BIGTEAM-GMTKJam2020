using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BattleTarget : MonoBehaviour
{
    public delegate void BattleTargetDelegate(BattleTarget Source);
    public delegate void BattleTargetWithOtherDelegate(BattleTarget Source, BattleTarget Other);

    public BattleTargetWithOtherDelegate OnTakeDamage;
    public BattleTargetWithOtherDelegate OnKilled;
    public BattleTargetWithOtherDelegate OnStunned;
    public BattleTargetDelegate OnNotStunned;


    public bool IsStunned;

    public enum EBattleTeam
    {
        HERO,
        NINJA
    }
    public Vector2 CurrentFacing = new Vector2(0, 1);
    public int HitsUntilDeath = 1;
    public GameObject TempDeathEffectPrefab;

    public EBattleTeam Team = EBattleTeam.NINJA;
    public bool IsDead()
    {
        return HitsUntilDeath <= 0;
    }

    public bool IsHostile(BattleTarget other)
    {
        return other.Team != this.Team;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeStun(BattleTarget Source = null)
    {
        IsStunned = true;
        if (OnStunned != null) OnStunned.Invoke(this, Source);
    }

    public virtual void RemoveStun()
    {
        if (IsStunned)
        {
            IsStunned = false;
            if (OnNotStunned != null) OnNotStunned.Invoke(this);
        }
    }


    public virtual void TakeHit(BattleTarget Source = null)
    {
        HitsUntilDeath -= 1;
        if (OnTakeDamage != null) OnTakeDamage.Invoke(this, Source);
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

        if (OnKilled != null) OnKilled.Invoke(this, KillSource);

        // Play effects and stuff here.
        // Temp effect
        StartCoroutine(DeathEffectsCoroutine(KillSource));
         
    }

    public IEnumerator DeathEffectsCoroutine(BattleTarget KillSource)
    {
        Vector3 IncomingAttackVector = this.transform.position - KillSource.transform.position;
        IncomingAttackVector.Normalize();

        // Nudge them a bit.
        yield return transform.DOLocalMove(transform.position + IncomingAttackVector * 0.2f, 0.1f).SetEase(Ease.OutCubic);

        yield return new WaitForSeconds(0.3f);
        
        Instantiate(TempDeathEffectPrefab, this.transform.position - Vector3.forward * 2, transform.rotation);
        // For now, just destroy.  AXE CHANGE THIS
        Destroy(gameObject);
    }
}
