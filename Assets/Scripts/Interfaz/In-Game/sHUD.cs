using UnityEngine;
using System.Collections;

public class sHUD : MonoBehaviour {

	public GUIText txt_timer;
	public GUIText txt_score;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(sControl.getInstancia.activo){
			if(sControl.getInstancia.timer>=0){
				if(sControl.getInstancia.nivel != 1)
					txt_timer.guiText.text = sControl.getInstancia.timer.ToString("F1") + " Secs";
			} 
			else{
				if(sControl.getInstancia.scoreMinimo > sControl.getInstancia.score)
					txt_timer.guiText.text = "YOU LOSE";
				else
					txt_timer.guiText.text = "YOU WIN";
			}
		}
		if(sControl.getInstancia.score >= sControl.getInstancia.scoreMinimo){
			txt_timer.guiText.text = "YOU WIN";
		}
		txt_score.guiText.text = ""+sControl.getInstancia.score+" Pts";
	}
}
