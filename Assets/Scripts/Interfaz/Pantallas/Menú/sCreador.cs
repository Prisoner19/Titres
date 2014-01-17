using UnityEngine;
using System.Collections;

public class sCreador : MonoBehaviour {


	// Use this for initialization
	void Start () {
		if(GameObject.Find("ControlSupremo") == null){
			GameObject controlSupremo = new GameObject("ControlSupremo");
			controlSupremo.AddComponent("sControlSupremo");
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
