using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControlScript : MonoBehaviour
{

    public delegate void HeroControlScriptDelegate(HeroControlScript source);
	public delegate void HeroControlScriptAddActionDelegate(HeroControlScript source, EPlayerMoves InputMove);
	public delegate void HeroControlScriptMoveResultDelegate(HeroControlScript source, EPlayerMoves chosenMove, EMoveResult result);

    public HeroControlScriptMoveResultDelegate OnMoveCompleted;
	public HeroControlScriptAddActionDelegate OnActionQueued;
	public HeroControlScriptDelegate OnRemoveLastQueued;
    public HeroControlScriptDelegate OnMoveQueueUpdated;
	public HeroControlScriptDelegate OnQueueCleared;
	public HeroControlScriptDelegate OnActionQueuePlay;


	private SpriteRenderer m_heroRenderer;
	public SpriteRenderer HeroRenderer
	{
		get
		{
			if (!m_heroRenderer)
				m_heroRenderer = GetComponent<SpriteRenderer>();

			return m_heroRenderer;
		}
	}

	private SpriteRenderer m_slashRenderer;
	public SpriteRenderer SlashRenderer
	{
		get
		{
			if (!m_slashRenderer)
				m_slashRenderer = GetComponent<SpriteRenderer>();

			return m_slashRenderer;
		}
	}

	protected Vector2 LastDirection = new Vector2(1, 0);

	protected EPlayerMoves MoveInProgress = EPlayerMoves.NONE;

    public GridMovementComponent MovementComponent;
	public GridCombatComponent CombatComponent;


	private void Awake()
    {
        if (MovementComponent == null)
        {
            MovementComponent = gameObject.GetComponent<GridMovementComponent>();
        }
		if (CombatComponent == null)
		{
			CombatComponent = gameObject.GetComponent<GridCombatComponent>();
		}
	}
    public List<EPlayerMoves> MoveQueue = new List<EPlayerMoves>();


    public EMoveResult ProcessNextQueuedMove()
    {
        if (MoveQueue.Count == 0)
        {
            return EMoveResult.SYSTEM_ERROR;
        }

        if (MoveInProgress != EPlayerMoves.NONE)
        {
            return EMoveResult.NEUTRAL;
        }
        EPlayerMoves ChosenMove = MoveQueue[0];
        MoveQueue.RemoveAt(0);

        return ProcessMoveEnum(ChosenMove);
    }

    public EMoveResult ProcessMoveEnum(EPlayerMoves InputMove)
    {
        if (MoveInProgress != EPlayerMoves.NONE)
        {
            return EMoveResult.NEUTRAL;
        }

        MoveInProgress = InputMove;
        switch (InputMove)
        {
            case EPlayerMoves.UP:
                MovementComponent.OnMoveCompleted += HandleMovementComponentDone;
				LastDirection = new Vector2(0, 1);
				MovementComponent.AttemptMovement(LastDirection);
                return EMoveResult.SUCCESS;
                break;
            case EPlayerMoves.RIGHT:
                MovementComponent.OnMoveCompleted += HandleMovementComponentDone;
				HeroRenderer.flipX = false;
				LastDirection = new Vector2(1, 0);
				MovementComponent.AttemptMovement(LastDirection);
                return EMoveResult.SUCCESS;
                break;
            case EPlayerMoves.LEFT:
                MovementComponent.OnMoveCompleted += HandleMovementComponentDone;
				HeroRenderer.flipX = true;
				LastDirection = new Vector2(-1, 0);
				MovementComponent.AttemptMovement(LastDirection);
                return EMoveResult.SUCCESS;
                break;
            case EPlayerMoves.DOWN:
                MovementComponent.OnMoveCompleted += HandleMovementComponentDone;
				LastDirection = new Vector2(0, -1);
                MovementComponent.AttemptMovement(LastDirection);
                return EMoveResult.SUCCESS;
                break;
            case EPlayerMoves.WAIT:
                MoveInProgress = EPlayerMoves.NONE;
                OnMoveCompleted.Invoke(this, EPlayerMoves.WAIT, EMoveResult.NEUTRAL);
                return EMoveResult.NEUTRAL;
                break;
            case EPlayerMoves.SWORD:
				CombatComponent.OnCombatCompleted += HandleCombatComponentDone;
				CombatComponent.AttemptCombat(LastDirection, HeroRenderer.flipX);
                return EMoveResult.NEUTRAL;
                break;
            case EPlayerMoves.SHEATHE:
                MoveInProgress = EPlayerMoves.NONE;
                OnMoveCompleted.Invoke(this, EPlayerMoves.SHEATHE, EMoveResult.NEUTRAL);
                return EMoveResult.NEUTRAL;
                break;

            default:
                Debug.LogError("Could not process move " + InputMove.ToString());
                return EMoveResult.SYSTEM_ERROR;
                break;
        }

        return EMoveResult.NEUTRAL;
    }

    public void AddMoveToQueue(EPlayerMoves NewMove)
    {
        MoveQueue.Add(NewMove);
        Debug.Log("added move to queue " + NewMove.ToString());
		if (OnActionQueued != null) OnActionQueued.Invoke(this, NewMove);
		if (OnMoveQueueUpdated != null) OnMoveQueueUpdated.Invoke(this);
    }

    public void RemoveLastQueuedMove()
    {
        if (MoveQueue.Count > 0)
        {
            MoveQueue.RemoveAt(MoveQueue.Count - 1);
            Debug.Log("Removing last queued move.  new count is " + MoveQueue.Count.ToString());
        }
		if (OnRemoveLastQueued != null) OnRemoveLastQueued.Invoke(this);
		if (OnMoveQueueUpdated != null) OnMoveQueueUpdated.Invoke(this);
    }


    public void HandleMovementComponentDone(GridMovementComponent source, EMoveResult result)
    {
        source.OnMoveCompleted -= HandleMovementComponentDone;
        EPlayerMoves finishedMove = MoveInProgress;
        MoveInProgress = EPlayerMoves.NONE;
        OnMoveCompleted.Invoke(this, MoveInProgress, result);
    }

	public void HandleCombatComponentDone(GridCombatComponent source, ECombatResult result)
	{
		source.OnCombatCompleted -= HandleCombatComponentDone;
		EPlayerMoves finishedMove = MoveInProgress;
		MoveInProgress = EPlayerMoves.NONE;

		//messy match im sorry, theyre 1-1 like i really dont know my dude
		EMoveResult res = (EMoveResult)(int)result;

		OnMoveCompleted.Invoke(this, MoveInProgress, res);
	}

	public void ClearQueue()
	{
		MoveQueue.Clear();
		if (OnQueueCleared != null) OnQueueCleared.Invoke(this);
	}

	public void NotifyPlayQueue()
	{
		if (OnActionQueuePlay != null) OnActionQueuePlay.Invoke(this);
	}
}
