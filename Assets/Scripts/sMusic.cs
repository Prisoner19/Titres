using UnityEngine;
using System.Collections;

public class sMusic : MonoBehaviour {

	public AudioClip song;
	
	// Use this for initialization
	void Start () {
		if(sControlSupremo.getInstancia.playMusic == true){
			audio.PlayOneShot(song);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
