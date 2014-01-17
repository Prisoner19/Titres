using UnityEngine;
using System.Collections;

public class sBack : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		string scene = Application.loadedLevelName;

		if(scene == "Resultado"){
			Application.LoadLevel("Selector");
		}
		else if(scene == "Selector"){
			Application.LoadLevel("Menu");
		}
		else if(scene == "Opciones"){
			Application.LoadLevel("Menu");
		}

	}
}
