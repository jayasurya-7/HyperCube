using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundmover_Car2D : MonoBehaviour
{
	public float materialSpeedX = 0.0f;
	public float materialSpeedY = 1.0f;
	private Vector2 materialOffset;
	private Renderer materialRenderer;
	private float gamespeed;

	void Start()
	{
		materialRenderer = GetComponent<Renderer>();
	}

	void Update()
	{
		materialOffset = new Vector2(materialSpeedX * Time.time, materialSpeedY * Time.time);
		materialRenderer.material.mainTextureOffset = materialOffset;
	}
}
