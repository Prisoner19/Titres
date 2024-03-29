﻿using UnityEngine;
using System.Collections;

public class sBase : MonoBehaviour {
	
	private int linea;
	private int columna;
	private int tipo;
	public Sprite spriteMetal;
	private SpriteRenderer rend;

	void Awake()
	{
		rend = GetComponent("SpriteRenderer") as SpriteRenderer;
	}

	// Use this for initialization
	void Start () {
		linea = 0;
		columna = 0;
		transform.parent = null;
		linea = Mathf.FloorToInt((transform.position.y + 4.75f)*2);
		columna = Mathf.FloorToInt((transform.position.x + 2.25f)*2);
		sControl.getInstancia.matrizOcupados[linea,columna] = true;

		if(linea<20)
		{
			transform.parent = sControl.getInstancia.lineas[linea].transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y >= 5.2f){
			sControl.getInstancia.terminarEscena();
			Destroy(transform.gameObject);
		}
	}

	public void metalear()
	{
		rend.sprite = spriteMetal;
	}
}
