using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
	
	private bool activo=true;
	private int bloquesLinea;
	public bool stop=false;
	public float timer;
	
	public GUIText txt_freeze;
	public GUIText txt_timer;
	
	private static Control instancia; 
	public static BloqueScript bloqueActivo; 

	public GameObject[] lineas;
	
	private void Awake(){
		instancia = this;	
		lineas = new GameObject[10];
		for(int i=0; i<10;i++){
			lineas[i] = new GameObject("Linea"+i);
		}
	}
	
	// Use this for initialization
	void Start() {
		txt_freeze.guiText.text = "";
		crearFigura();
		bloqueActivo = null;
		bloquesLinea = 12;
	}
	
	// Update is called once per frame
	void Update() {
		
		if(timer>=0){
			timer -= Time.deltaTime;
			txt_timer.guiText.text = timer.ToString("F1");
		} 
	}
	
	public static Control getInstancia{
		get{
			return instancia;	
		}
	}

	public static BloqueScript getBloque{
		get{
			return bloqueActivo;	
		}
	}

	
	public bool estaActivo{
		get{
			return activo;	
		}
		set{
			activo=value;	
		}
	}
	
	public void crearFigura(){
		
		Vector3 posInicio = new Vector3(0.25f,6.10f,0);
		
		int rand_fig = UnityEngine.Random.Range (1,6);
		
		//Debug.Log("Prefabs/Figura"+ rand_fig);
		Instantiate(Resources.Load("Prefabs/Figura"+ rand_fig), posInicio, transform.rotation);
		Instantiate(Resources.Load("Prefabs/CuadF"+ rand_fig), posInicio, transform.rotation);
	}

	public void deseleccionarBloque(){
		bloqueActivo.transform.localScale -= new Vector3(0.2f,0.2f,0);
		bloqueActivo = null;
	}

	public void eliminarCuad(){
		GameObject basurero = new GameObject();
		GameObject[] array;
		array = GameObject.FindGameObjectsWithTag("BCuad");
		
		foreach (GameObject basura in array){
			basura.transform.parent = basurero.transform;
		}
		GameObject.Destroy(basurero);
	}

	public void verificarLinea(int linea){
		Transform row = GameObject.Find("Linea"+linea).transform;

		if(row.childCount == bloquesLinea){
			Debug.Log("Linea "+linea+" llena");
			/*
			for(int i=row.childCount-1; i>=0; i--){
				Destroy(row.GetChild(i).gameObject);
			}
			actualizarLineas(linea);
			*/
		}

	}

	public void actualizarLineas(int row){
		Vector3 pos;
		for(int i=row; i<=9; i++){
			Transform lineaArriba = GameObject.Find("Linea"+(row+1)).transform;
			Transform lineaActual = GameObject.Find("Linea"+row).transform;
			pos = lineaActual.position;
			pos.y = pos.y - 1;
			Debug.Log(lineaActual.transform.position.ToString("F2"));
			for(int j=lineaArriba.childCount-1; j>=0; j--){
				lineaArriba.GetChild(i).parent = lineaActual;
			}
			lineaArriba.position = pos;
		}
	}
}

