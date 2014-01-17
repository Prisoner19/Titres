using UnityEngine;
using System.Collections;

public class sOptions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(-5,-1,-1);
		iTween.MoveTo(gameObject, iTween.Hash("x", 0, "easeType", "easeInOutExpo", "delay", .7));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Debug.Log ("A opciones!");
	}
}
