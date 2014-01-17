using UnityEngine;
using System.Collections;

public class sBtnMusic : MonoBehaviour {

	public Sprite on;
	public Sprite off;
	private SpriteRenderer rend;

	// Use this for initialization
	void Start () {

		rend = GetComponent("SpriteRenderer") as SpriteRenderer;

		if(sControlSupremo.getInstancia.playMusic == true){
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
		sControlSupremo.getInstancia.playMusic = ! sControlSupremo.getInstancia.playMusic;
		if(sControlSupremo.getInstancia.playMusic == true)
			rend.sprite = on;
		else
			rend.sprite = off;
	}
}
