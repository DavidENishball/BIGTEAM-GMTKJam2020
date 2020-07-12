﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeEffect : MonoBehaviour
{

	public Transform transform;
	public float shakeDuration = 0f;
	public float shakeMagnitude = 0.7f;
	public float dampingSpeed = 1.0f;
	public Vector3 initialPosition;

	void Awake()
	{
		if (transform == null)
		{
			transform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		initialPosition = transform.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

			shakeDuration -= Time.deltaTime * dampingSpeed;
		}
		else
		{
			shakeDuration = 0f;
			transform.localPosition = initialPosition;
		}
	}

	public void TriggerShake()
	{
		shakeDuration = 0.2f;
	}
}
