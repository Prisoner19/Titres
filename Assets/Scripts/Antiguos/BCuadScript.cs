using UnityEngine;
using System.Collections;

public class BCuadScript : MonoBehaviour {

	//public PadreCuad padre;
	private bool visible = false;
	private Vector3 posInicio;

	// Use this for initialization
	void Start () {
		renderer.enabled = false;
		visible = false;
		transform.parent = GameObject.FindGameObjectWithTag("Cuadricula").transform;
		posInicio = transform.position;
		posInicio.z = 1;
		transform.position=posInicio;
		gameObject.tag = "BCuad";
	}
	
	// Update is called once per frame
	void Update () {
		if(Control.getBloque!=null && !visible){
			visible = true;
			renderer.enabled = true;
			transform.parent = null;
		}
		if(Control.getBloque==null && visible){
			transform.parent = GameObject.FindGameObjectWithTag("Cuadricula").transform;
			visible = false;
			renderer.enabled = false;
		}
	}

	private void OnMouseDown(){

		if(Control.bloqueActivo != null){

			Vector3 posBloque = Control.getBloque.transform.position;
			Vector3 posNueva = transform.position;

			posBloque.x = transform.position.x;
			posBloque.y = transform.position.y;
			Control.getBloque.transform.position = posBloque;
			posNueva.z = 1;
			transform.position = posNueva;
			Control.getInstancia.deseleccionarBloque();
			Control.getInstancia.eliminarCuad();
		}
	}
}
