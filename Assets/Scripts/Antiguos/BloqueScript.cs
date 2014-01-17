using UnityEngine;
using System.Collections;

public class BloqueScript : MonoBehaviour {

	public FiguraScript figPadre;
	private bool fijo;
	private bool inicioCongelado;
	private int linea;
	private Vector3 posAux;

	// Use this for initialization
	void Start () {
		fijo = false;
		inicioCongelado = false;
		linea = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(figPadre.estado == 4 && !fijo){
			transform.parent = null;
			linea = Mathf.FloorToInt((transform.position.y + 4.75f)*2);
			transform.parent = Control.getInstancia.lineas[linea].transform;
			Control.getInstancia.verificarLinea(linea);
			fijo = true;
			figPadre.num_bloques--;
			if(figPadre.num_bloques==0)
				GameObject.Destroy(figPadre.gameObject);
			Destroy(gameObject.GetComponent("BloqueScript"));
		}

		if(figPadre.estado == 1 && !inicioCongelado){
			transform.parent = null;
			transform.parent = GameObject.Find("Temporal").transform;
			inicioCongelado = true;
		}

		if(figPadre.estado == 2 && inicioCongelado){
			transform.parent = null;
			transform.parent = figPadre.transform;
			inicioCongelado = false;
		}

	}


	private void OnMouseDown(){
		if(Control.getBloque == null){
			transform.localScale += new Vector3(0.2f,0.2f,0);
			figPadre.Cuadricular();
			Control.bloqueActivo = this;
		}
		else{
			Control.getBloque.transform.localScale -= new Vector3(0.2f,0.2f,0);
			if(Control.getBloque != this){
				Control.bloqueActivo = this;
				transform.localScale += new Vector3(0.2f,0.2f,0);
			}
			else{
				Control.bloqueActivo = null;
			}
		}
	}


	private void OnTriggerEnter2D(Collider2D other){
		if(other.name=="Trigger"){
			if(figPadre.estado!=4 || figPadre.estado!=3){
				figPadre.estado = 3;
				StartCoroutine("Asentar");
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		if(other.name=="Trigger"){
			if(figPadre.estado!=4)
				figPadre.estado = 2;
		}
	}

	private void OnTriggerStay2D(Collider2D other){
		if(other.name=="Trigger"){
			if(figPadre.estado==2){
				figPadre.estado = 3;
				StartCoroutine("Asentar");
			}
		}
	}

	private IEnumerator Asentar(){

		Vector3 pos; 

		yield return new WaitForSeconds(0.3f);

		if(figPadre.estado == 3){
			figPadre.estado = 4;
			Vector2 aux = figPadre.rigidbody2D.velocity;
			aux.x = 0;
			figPadre.rigidbody2D.velocity = aux;
			Control.getInstancia.crearFigura();
			pos = figPadre.transform.position;

			if(pos.y<0){
				pos.y = Mathf.Floor(Mathf.Abs(pos.y*4))/4;
				pos.y *= -1;
			}
			else{
				pos.y = Mathf.Round(Mathf.Abs(pos.y*4))/4;
			}
			

			pos.x = Mathf.Round((pos.x - 0.25f)*2) /2 + 0.25f;

			figPadre.transform.position = pos;
			figPadre.rigidbody2D.isKinematic = true;
			Control.getInstancia.eliminarCuad();
			Object.Destroy(GameObject.FindGameObjectWithTag("Cuadricula").gameObject);
		}
	}
}

