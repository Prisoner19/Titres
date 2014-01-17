using UnityEngine;
using System.Collections;

public class sNext : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Debug.Log("llegue");
		Application.LoadLevel("Nivel "+(sControl.getInstancia.nivel+1));
	}
}
