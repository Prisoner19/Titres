using UnityEngine;
using System.Collections;

public class BloqueBaseScript : MonoBehaviour {

	private int linea;

	// Use this for initialization
	void Start () {
		linea = 0;
		transform.parent = null;
		linea = Mathf.FloorToInt((transform.position.y + 4.75f)*2);
		transform.parent = Control.getInstancia.lineas[linea].transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
