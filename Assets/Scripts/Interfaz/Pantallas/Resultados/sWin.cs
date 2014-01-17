using UnityEngine;
using System.Collections;

public class sWin : MonoBehaviour {

	private GameObject centavos;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(-5,0,-1);

		if(sControlSupremo.getInstancia.metas[sControl.getInstancia.nivel-1,1]){
			if(sControlSupremo.getInstancia.metas[sControl.getInstancia.nivel-1,2]){
				centavos = Instantiate(Resources.Load("Prefabs/HUD/SSS"), new Vector3(-5,0,-1), transform.rotation) as GameObject;
			}
			else{
				centavos = Instantiate(Resources.Load("Prefabs/HUD/SSN"), new Vector3(-5,0,-1), transform.rotation) as GameObject;
			}
		}
		else{
			if(sControlSupremo.getInstancia.metas[sControl.getInstancia.nivel-1,2]){
				centavos = Instantiate(Resources.Load("Prefabs/HUD/SNS"), new Vector3(-5,0,-1), transform.rotation) as GameObject;
			}
			else{
				centavos = Instantiate(Resources.Load("Prefabs/HUD/SNN"), new Vector3(-5,0,-1), transform.rotation) as GameObject;
			}
		}

		centavos.transform.parent = transform;

		iTween.MoveTo(gameObject, iTween.Hash("x", 0, "easeType", "easeInOutExpo", "delay", .1));
		Destroy (GameObject.Find("Control"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
