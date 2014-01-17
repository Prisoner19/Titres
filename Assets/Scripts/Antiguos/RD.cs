using UnityEngine;
using System.Collections;

public class RD : MonoBehaviour {

	private Vector3 pos;

	// Use this for initialization
	void Start () {
		//rigidbody2D.Sleep();
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * 5 * Time.deltaTime);
		transform.Translate(Vector3.up * Input.GetAxis("Vertical") * 5 * Time.deltaTime);

		pos = transform.position;
		if(pos.x>3){
			pos.x -= 6;
		}
		else if(pos.x<-3){
			pos.x +=6;
		}

		if(pos.y>5){
			pos.y -= 10;
		}
		else if(pos.y<-5){
			pos.y += 10;
		}

		transform.position = pos;
	}
}
