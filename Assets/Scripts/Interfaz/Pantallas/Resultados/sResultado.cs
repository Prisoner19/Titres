using UnityEngine;
using System.Collections;

public class sResultado : MonoBehaviour {

	GameObject popup;

	// Use this for initialization
	void Start () {
		if(sControl.getInstancia.win)
			popup = Instantiate(Resources.Load("Prefabs/HUD/Win"), new Vector3(0,0,-1), transform.rotation) as GameObject;
		else
			popup = Instantiate(Resources.Load("Prefabs/HUD/Lose"), new Vector3(0,0,-1), transform.rotation) as GameObject;

}
	
	// Update is called once per frame
	void Update () {
	
	}
}
