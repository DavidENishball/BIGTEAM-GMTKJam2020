using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridCombatComponent : MonoBehaviour
{
	public float MoveTime = 0.3f;
	public delegate void GridCombatComponentDelegate(GridCombatComponent source);
	public delegate void CombatResultDelegate(GridCombatComponent source, ECombatResult result);

	public CombatResultDelegate OnCombatCompleted;

	public GameObject SlashEffectPrefab;

	public ECombatResult AttemptCombat(Vector2 Direction, bool FlipX)
	{
		if (IsDirectionOccupied(Direction))
		{
			//DID DAMAGE??
			DoSlash(Direction, FlipX);
			return ECombatResult.SUCCESS;
		}
		else
		{
			//whiff???
			DoSlash(Direction, FlipX);
			return ECombatResult.FAILURE;
		}
	}

	public void DoSlash(Vector2 Direction, bool FlipX)
	{
		Vector2 AttackPoint = new Vector2(transform.position.x, transform.position.y) + Direction;

		//gonna have to get the target here, for now ill just spawn an effect
		var effectObject = Instantiate(SlashEffectPrefab, AttackPoint, Quaternion.identity) as GameObject;
		var OSE = effectObject.GetComponent<OneShotEffect>();
		OSE.OnCompletCallback = InvokeCombatCompleted;
		OSE.EffectRenderer.flipX = FlipX;
	}
	public void DoWhiff(Vector2 Direction, bool FlipX)
	{
		Vector2 AttackPoint = new Vector2(transform.position.x, transform.position.y) + Direction;

		//gonna have to get the target here, for now ill just spawn an effect
		var effectObject = Instantiate(SlashEffectPrefab, AttackPoint, Quaternion.identity) as GameObject;
		var OSE = effectObject.GetComponent<OneShotEffect>();
		OSE.OnCompletCallback = InvokeCombatFailed;
		OSE.EffectRenderer.flipX = FlipX;
	}


	public bool IsDirectionOccupied(Vector2 Direction)
	{
		Vector3 Movepoint = transform.position + new Vector3(Direction.x, Direction.y);

		Collider2D[] AllHits = Physics2D.OverlapCircleAll(Movepoint, 0.4f);

		if (AllHits.Length > 0)
		{
			return true;
		}
		return false;
	}

	protected void InvokeCombatCompleted()
	{
		OnCombatCompleted.Invoke(this, ECombatResult.SUCCESS);
	}
	protected void InvokeCombatFailed()
	{
		OnCombatCompleted.Invoke(this, ECombatResult.FAILURE);
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
