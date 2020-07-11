using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GridMovementComponent : MonoBehaviour
{
    public float MoveTime = 0.5f;
    public delegate void GridMovementComponentDelegate(GridMovementComponent source);
    public delegate void MoveResultDelegate(GridMovementComponent source, EMoveResult result);

    public MoveResultDelegate OnMoveCompleted;

	public GameObject DustEffectPrefab;
    
    public EMoveResult AttemptMovement(Vector2 Direction, bool FlipX)
    {
        if (IsDirectionOccupied(Direction))
        {
			DoBounceMove(Direction);
			return EMoveResult.FAILURE;
        }
        else
        {
            DoMove(Direction, FlipX);
            return EMoveResult.SUCCESS;
        }
    }

    public void DoMove(Vector2 Direction, bool FlipX)
    {
        Vector2 Movepoint = new Vector2(transform.position.x, transform.position.y) + Direction;
        this.transform.DOMove(Movepoint, MoveTime).SetEase(Ease.OutExpo).OnComplete(InvokeMoveCompleted);

		var effectObject = Instantiate(DustEffectPrefab, transform.position, Quaternion.identity) as GameObject;
		var OSE = effectObject.GetComponent<OneShotEffect>();
		OSE.EffectRenderer.flipX = FlipX;
	}

	public void DoBounceMove(Vector2 Direction)
	{
		Sequence bounceSequence = DOTween.Sequence();

		Vector2 Movepoint1 = new Vector2(transform.position.x, transform.position.y) + (Direction/4f);
		Vector2 Movepoint2 = new Vector2(transform.position.x, transform.position.y);

		bounceSequence.Append(transform.DOMove(Movepoint1, MoveTime/2f).SetEase(Ease.OutExpo));
		bounceSequence.Append(transform.DOMove(Movepoint2, MoveTime/2f).SetEase(Ease.OutExpo).OnComplete(InvokeMoveFailed));

		bounceSequence.Play();
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

    protected void InvokeMoveCompleted()
    {
        OnMoveCompleted.Invoke(this, EMoveResult.SUCCESS);
    }
	protected void InvokeMoveFailed()
	{
		OnMoveCompleted.Invoke(this, EMoveResult.FAILURE);
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
