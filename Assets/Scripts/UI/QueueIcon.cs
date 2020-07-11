using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueIcon : MonoBehaviour
{
	private Image m_iconRenderer;
	public Image IconRenderer
	{
		get
		{
			if (!m_iconRenderer)
				m_iconRenderer = GetComponent<Image>();

			return m_iconRenderer;
		}
	}
	public bool HighLighted = true;
	public List<Sprite> ActionIcons;

	public void Update()
	{
		if (HighLighted)
		{
			IconRenderer.color = Color.white;
		}
		else
		{
			IconRenderer.color = Color.gray;
		}
	}


	private Sprite GetIcon(EPlayerMoves Move)
	{
		return ActionIcons[(int)Move];
	}

	public void SetIcon(EPlayerMoves Move)
	{
		IconRenderer.sprite = GetIcon(Move);
	}

}
