using UnityEngine;
using System.Collections;

public class sTitulo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0,6,-1);
		iTween.MoveTo(gameObject, iTween.Hash("y", 3, "easeType", "easeInOutExpo", "delay", .1));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
