using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
	public List<QueueIcon> ActionList;

	public GameObject QueueItemPrefab;

	public RectTransform QueuePanel, LeftBracket, RightBracket;

	public int CurrentAction = 0;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Init(HeroControlScript Hero)
	{
		Hero.OnMoveCompleted += HandleActionCompleted;
		Hero.OnActionQueued += AddToBar;
		Hero.OnRemoveLastQueued += RemoveLastFromBar;
		Hero.OnQueueCleared += ClearBar;
		Hero.OnActionQueuePlay += HandleBeginPlay;
	}

	void HandleBeginPlay(HeroControlScript source)
	{
		CurrentAction = 0;

		foreach (var action in ActionList)
		{
			action.HighLighted = false;
		}

	}

	void HandleActionCompleted(HeroControlScript source, EPlayerMoves chosenMove, EMoveResult result)
	{
		//leave a highlighter failure
		if (result == EMoveResult.FAILURE)
		{
			ActionList[CurrentAction].SetIcon(EPlayerMoves.ERROR);
			ActionList[CurrentAction].HighLighted = true;
		}
		else
		{
			ActionList[CurrentAction].HighLighted = false;
		}

		if (CurrentAction < ActionList.Count-1)
		{
			CurrentAction++;
			ActionList[CurrentAction].HighLighted = true;
		}

	}

	void ClearBar(HeroControlScript source)
	{
		var items = ActionList.ToArray();

		for (int i = 0; i < items.Length; i++)
		{
			ActionList.Remove(items[i]);
			Destroy(items[i].gameObject);
		}

		LayoutBrackets();
	}

	void AddToBar(HeroControlScript source, EPlayerMoves Move)
	{

		if (Move == EPlayerMoves.NONE)
		{
			return;
		}

		//make new boy
		var itemObject = Instantiate(QueueItemPrefab, QueuePanel) as GameObject;
		var itemComponent = itemObject.GetComponent<QueueIcon>();

		itemComponent.SetIcon(Move);
		ActionList.Add(itemComponent);

		LayoutBrackets();
	}

	void RemoveLastFromBar(HeroControlScript source)
	{
		//make new boy
		var itemObject = Instantiate(QueueItemPrefab) as GameObject;
		var itemComponent = itemObject.GetComponent<QueueIcon>();

		var lastItemIdx = ActionList.Count;

		//if it is 0 then its empty
		if (lastItemIdx == 0)
		{
			return;
		}

		var lastItem = ActionList[lastItemIdx - 1];
		ActionList.RemoveAt(lastItemIdx - 1);
		Destroy(lastItem.gameObject);

		LayoutBrackets();
	}

	private void LayoutBrackets()
	{
		var bracketOffset = ActionList.Count * 16;

		LeftBracket.anchoredPosition = new Vector2(240 - bracketOffset, -248.7f);
		RightBracket.anchoredPosition = new Vector2(240 + bracketOffset, -248.7f);
	}

}
