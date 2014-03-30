using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Mouse is down");
			
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit) 
			{
				Debug.Log("Hit " + hitInfo.transform.gameObject.name);
				if (hitInfo.transform.gameObject.name == "ChamferBox014")
				{
					Material newMat = Resources.Load("bloque", typeof(Material)) as Material;
					renderer.material = newMat;
					Debug.Log ("It's working!");
				} else {
					Debug.Log ("nopz");
				}
			} else {
				Debug.Log("No hit");
			}
			//Debug.Log("Mouse is down");
		} 	
	}

	void OnMouseDown(){

	}
}
