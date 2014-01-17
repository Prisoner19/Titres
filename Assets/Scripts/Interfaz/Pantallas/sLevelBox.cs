using UnityEngine;
using System.Collections;

public class sLevelBox : MonoBehaviour {

	public int nivel;
	public bool activo;
	public Sprite spriteBueno;
	public Sprite spriteMalo;
	private SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent("SpriteRenderer") as SpriteRenderer;

		if(sControlSupremo.getInstancia.niveles[nivel-1] == true){
			rend.sprite = spriteBueno;
			activo = true;
		}
		else{
			rend.sprite = spriteMalo;
			activo = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if(activo)
			Application.LoadLevel("Nivel "+nivel);
	}
}
