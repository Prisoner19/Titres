using UnityEngine;
using System.Collections;

public class sBloque : MonoBehaviour {

	public sFigura figuraPadre;
	public Sprite spriteMadera;
	public Sprite spriteMetal;
	public int tipo;
	private SpriteRenderer rend;

	private const int MADERA = 1;
	private const int METAL = 2;

	// Use this for initialization
	void Start () {
		tipo = MADERA;
		if(sControl.getInstancia.nivel>5){
			rend = GetComponent("SpriteRenderer") as SpriteRenderer;
			int random = Random.Range(1,11);
			if(random<9){
				tipo = MADERA;
				rend.sprite = spriteMadera;
			}
			else{
				tipo = METAL;
				rend.sprite = spriteMetal;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//sControl.getInstancia.finalSentado = false;
	}

	void OnMouseDown(){
		if(tipo == MADERA){
			seleccionarBloque();
		}
	}

	void seleccionarBloque(){
		if(figuraPadre.estado == 0){
			if(figuraPadre.bloqueActivo == null){
				transform.localScale += new Vector3(0.2f,0.2f,0);
				figuraPadre.bloqueActivo = this;
			}
			else{
				figuraPadre.bloqueActivo.transform.localScale -= new Vector3(0.2f,0.2f,0);
				if(figuraPadre.bloqueActivo != this){
					figuraPadre.bloqueActivo = this;
					transform.localScale += new Vector3(0.2f,0.2f,0);
				}
				else{
					figuraPadre.bloqueActivo = null;
				}
			}
		}
	}

}
