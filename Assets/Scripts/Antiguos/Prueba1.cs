using UnityEngine;
using System.Collections;

public class Prueba1 : MonoBehaviour {

	private Vector3 pos;

	// Use this for initialization

	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")){
			for(int i=transform.childCount-1; i>=0; i--){
				transform.GetChild(i).parent = GameObject.Find("Padre0").transform;
			}
			pos.y = pos.y - 1;
			GameObject.Find("Padre0").transform.position = pos;
		}
	}
}
