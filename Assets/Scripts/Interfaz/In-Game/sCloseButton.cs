using UnityEngine;
using System.Collections;

public class sCloseButton : MonoBehaviour {

	public sMensaje mensajePadre;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		mensajePadre.esconder();
	}
}
