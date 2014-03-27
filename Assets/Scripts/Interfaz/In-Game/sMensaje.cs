using UnityEngine;
using System.Collections;

public class sMensaje : MonoBehaviour {

	public bool escondido;
	
	// Use this for initialization
	void Start () {
		escondido = false;
		transform.position = new Vector3(-6,0,-1);
		iTween.MoveTo(gameObject, iTween.Hash("x", -0.2f, "easeType", "easeInOutExpo", "delay", .2f, "oncomplete", "esperarEsconder"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void esperarEsconder()
	{
		StartCoroutine("esperar");
	}

	IEnumerator esperar()
	{
		yield return new WaitForSeconds(2);
		sControl.getInstancia.iniciar();
		yield return new WaitForSeconds(1.5f);
		esconder();
	}

	void esconder()
	{
		iTween.MoveTo(gameObject, iTween.Hash("x", -6f, "easeType", "easeInOutExpo", "delay", .2f));
		escondido = true;

	}
}
