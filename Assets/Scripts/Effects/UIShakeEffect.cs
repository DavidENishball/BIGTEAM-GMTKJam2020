using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShakeEffect : MonoBehaviour
{

	public RectTransform transform;
	public float shakeDuration = 0f;
	public float shakeMagnitude = 0.7f;
	public float dampingSpeed = 1.0f;
	public Vector2 initialPosition;

	void Awake()
	{
		if (transform == null)
		{
			transform = GetComponent(typeof(RectTransform)) as RectTransform;
		}
	}

	void OnEnable()
	{
		initialPosition = transform.anchoredPosition;
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			var spherePos = Random.insideUnitSphere;
			transform.anchoredPosition = initialPosition + new Vector2(spherePos.x, spherePos.y) * shakeMagnitude;

			shakeDuration -= Time.deltaTime * dampingSpeed;
		}
		else
		{
			shakeDuration = 0f;
			transform.anchoredPosition = initialPosition;
		}
	}

	public void TriggerShake()
	{
		shakeDuration = 0.2f;
	}
}
