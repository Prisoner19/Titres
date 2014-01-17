using UnityEngine;
using System.Collections;

public class sBase : MonoBehaviour {
	
	private int linea;
	
	// Use this for initialization
	void Start () {
		linea = 0;
		transform.parent = null;
		linea = Mathf.FloorToInt((transform.position.y + 4.75f)*2);
		if(linea<12)
			transform.parent = sControl.getInstancia.lineas[linea].transform;
		sControl.getInstancia.numBloques--;
		//sControl.getInstancia.verificarLinea(linea);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y >= GameObject.Find("Limite").transform.position.y){
			sControl.getInstancia.terminarEscena();
			Destroy(transform.gameObject);
		}
	}
}
