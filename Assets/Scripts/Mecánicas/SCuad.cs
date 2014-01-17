using UnityEngine;
using System.Collections;

public class SCuad : MonoBehaviour {

	public sFigura figura;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Figura");
		figura = (sFigura) go.GetComponent(typeof(sFigura));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if(figura.bloqueActivo != null){
			moverBloque();
		}
	}

	void moverBloque(){

		Vector3 posOriginal = figura.bloqueActivo.transform.position;
		Vector3 posNueva = transform.position;
		
		posNueva.z = posOriginal.z;
		
		figura.bloqueActivo.transform.position = posNueva;
		figura.bloqueActivo.transform.localScale -= new Vector3(0.2f,0.2f,0);
		figura.bloqueActivo = null;
		figura.Cuadricular();
		figura.mostrarCuadricula();
		figura.actualizarPosicionClon();
	}
}
