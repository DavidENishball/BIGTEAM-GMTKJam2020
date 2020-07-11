using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotEffect : MonoBehaviour
{
	private SpriteRenderer m_effectRenderer;
	public SpriteRenderer EffectRenderer
	{
		get
		{
			if (!m_effectRenderer)
				m_effectRenderer = GetComponent<SpriteRenderer>();

			return m_effectRenderer;
		}
	}
	public float lifetime = 0.3f;

	//2 pixel variance
	public float variance = 3;
	public float pixelSize = 0.0625f;
	public List<Sprite> SpritePool;

	public System.Action OnCompletCallback;

	private void Awake()
	{
		EffectRenderer.sprite = SpritePool[Random.Range(0, SpritePool.Count)];

		//move the effect around a little
		this.transform.position = transform.position + new Vector3(Random.Range(-variance, variance) * pixelSize, Random.Range(-variance, variance) * pixelSize);

		EffectRenderer.DOColor(new Color(1f, 1f, 1f, 0f), lifetime).SetEase(Ease.InQuad);

		//fire and forget
		Destroy(this.gameObject, lifetime);
	}


	private void OnDestroy()
	{
		if (OnCompletCallback != null)
		{
			OnCompletCallback();
		}
	}
}
