using UnityEngine;
using System.Collections;

public class sBtnFX : MonoBehaviour {

	public Sprite on;
	public Sprite off;
	private SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent("SpriteRenderer") as SpriteRenderer;
		
		if(sControlSupremo.getInstancia.playFX == true){
			rend.sprite = on;
		}
		else{
			rend.sprite = off;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		sControlSupremo.getInstancia.playFX = ! sControlSupremo.getInstancia.playFX;
		if(sControlSupremo.getInstancia.playFX == true)
			rend.sprite = on;
		else
			rend.sprite = off;
	}
}
