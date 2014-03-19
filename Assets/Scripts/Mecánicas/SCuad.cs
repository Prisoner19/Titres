using UnityEngine;
using System.Collections;

public class SCuad : MonoBehaviour {

	public sMovimiento sMov;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Figura");
		sMov = (sMovimiento) go.GetComponent(typeof(sMovimiento));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if(sMov.bloqueActivo != null){
			moverBloque();
		}
	}

	void moverBloque(){

		Vector3 posOriginal = sMov.bloqueActivo.transform.position;
		Vector3 posNueva = transform.position;
		
		posNueva.z = posOriginal.z;
		
		sMov.bloqueActivo.transform.position = posNueva;
		sMov.bloqueActivo.transform.localScale -= new Vector3(0.2f,0.2f,0);
		sMov.bloqueActivo = null;
		sMov.Cuadricular();
		sMov.mostrarCuadricula();
		sMov.actualizarGuia();
	}
}
