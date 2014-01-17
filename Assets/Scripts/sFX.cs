using UnityEngine;
using System.Collections;

public class sFX : MonoBehaviour {

	public AudioClip combo2FX;
	public AudioClip combo3FX;
	public AudioClip combo4FX;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void efectoCombo(int combo){
		switch(combo){
			case 2: audio.PlayOneShot(combo2FX); break;
			case 3: audio.PlayOneShot(combo3FX); break;
			case 4: audio.PlayOneShot(combo4FX); break;
		}
	}
}
