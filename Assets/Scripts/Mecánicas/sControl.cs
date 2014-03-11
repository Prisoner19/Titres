using UnityEngine;
using System.Collections;

public class sControl : MonoBehaviour {

	private static sControl instancia; 
	public sFX efectos;
	public bool activo;
	public GameObject[] lineas;
	public bool finalSentado;
	public int numBloques;

	public int nivel;

	public int score;
	public float timer;
	private float tiempoInicial;

	public int scoreMinimo;

	public int scoreMeta;
	public float tiempoMeta;

	public bool win;
	public int combos;
	// Use this for initialization

	void Awake(){

		instancia = this;
		lineas = new GameObject[12];
		for(int i=0; i<12;i++){
			lineas[i] = new GameObject("Linea"+i);
			lineas[i].transform.parent = GameObject.Find("Base").transform;
		}
	}

	void Start () {
		win = false;
		activo = true;
		crearFigura();
		score = 0;
		tiempoInicial = timer;
	}
	
	// Update is called once per frame
	void Update () {
		if(activo){
			if(finalSentado){
				combos=0;
				for(int i=11; i>=0; i--){
					verificarLinea(i);
				}
				crearFigura();
				switch(combos){
					case 2: score+=50; break;
					case 3: score+=100; break;
					case 4: score+=200; break;
				}
				if(sControlSupremo.getInstancia.playFX == true){
					efectos.efectoCombo(combos);
				}
				//Debug.Log(combos);
			}

			if(timer>=0){
				timer -= Time.deltaTime;
			} 
			else{
				terminarEscena();
			}
			if(score>=scoreMinimo){
				sControlSupremo.getInstancia.niveles[sControl.getInstancia.nivel] = true;
				win = true;
				sControlSupremo.getInstancia.metas[nivel-1,0] = true;

				if(score>=scoreMeta){
					sControlSupremo.getInstancia.metas[nivel-1,2] = true;
				}

				if(tiempoMeta>=tiempoInicial-timer){
					sControlSupremo.getInstancia.metas[nivel-1,1] = true;
				}

				terminarEscena();
			}
		}

	}

	public void terminarEscena(){
		activo = false;
		destruirFigura();
		StartCoroutine("mostrarResultado");
	}
	
	public static sControl getInstancia{
		get{
			return instancia;	
		}
	}

	public IEnumerator mostrarResultado(){
		yield return new WaitForSeconds(1);
		DontDestroyOnLoad(gameObject);
		Application.LoadLevel("Resultado");
	}

	public void destruirFigura(){

		GameObject figura = GameObject.FindGameObjectWithTag("Figura");
		Destroy(figura);

		Destroy(GameObject.Find("Cuadricula"));

	}	

	public void crearFigura(){
		
		Vector3 posInicio = new Vector3(0.25f,6.25f,0);
		
		int rand_fig = UnityEngine.Random.Range (1,6);
		GameObject figura = Instantiate(Resources.Load("Prefabs/Figura"+rand_fig), posInicio, transform.rotation) as GameObject;
		figura.name = "Figura"+rand_fig;
	}

	public void verificarLinea(int linea){
		Transform row = GameObject.Find("Linea"+linea).transform;

		Debug.Log("Linea "+linea+" / "+row.childCount);

		if(row.childCount == 10){
			destruirLinea(row);
			actualizarLineas(linea);
			score += 100;
			combos++;
		}
	}

	public void destruirLinea(Transform row){
		for(int i=row.childCount-1; i>=0; i--){
			Destroy(row.GetChild(i).gameObject);
		}
	}

	public void actualizarLineas(int linea){

		Vector3 posNueva;
		Transform lineaArriba;
		Transform bloqueArriba;
		if(linea!=11){
			for(int posLinea = linea; posLinea < 11; posLinea++){
				lineaArriba = GameObject.Find("Linea"+(posLinea+1)).transform;
				for(int i=lineaArriba.childCount-1; i>=0; i--){
					bloqueArriba = lineaArriba.GetChild(i);
					posNueva = bloqueArriba.position;
					posNueva.y -= 0.5f;
					bloqueArriba.position = posNueva;
					bloqueArriba.parent = GameObject.Find("Linea"+(posLinea)).transform;
				}
			}
		}
	}

}
