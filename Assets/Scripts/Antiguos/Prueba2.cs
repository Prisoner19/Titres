using UnityEngine;
using System.Collections;

public class Prueba2 : MonoBehaviour {

	private float offset;

	void Start () {
		offset = 0;
	}

	void OnMouseDown(){
		Debug.Log("asd");
	}

	
	void Update()
	{
		if(Input.GetKey("a")){
			rigidbody2D.velocity = Vector3.right * -3;
			offset--;
		}
		else if(Input.GetKey("d")){
			rigidbody2D.velocity = Vector3.right * 3;
			offset++;
		}
		else if(Input.GetKeyUp("a") || Input.GetKeyUp("d")){
			rigidbody2D.velocity = Vector3.zero;
		}
	}
}
