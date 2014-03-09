using UnityEngine;
using System.Collections;

public class sMovimiento : MonoBehaviour {
	
	public GameObject pfbBase;
	
	private bool puedeMover;
	private bool puedeCaer;
	private bool yaMovio;
	private Vector3 posInicioCuad;

	public int fase;
	private const int ARMADO = 0;
	private const int ACOPLADO = 1;


	public int estado;

	private const int CONGELADO = 0;
	private const int CAYENDO = 1;
	private const int MOVIENDOSE = 2;
	private const int ASENTADO = 3;

	private GameObject cuadricula;
	public sBloque bloqueActivo;
	
	// Use this for initialization
	void Start () {

		fase = ARMADO;
		estado = CAYENDO;

		puedeMover = true;
		yaMovio = false;
		puedeCaer = true;
		sControl.getInstancia.finalSentado = false;

		posInicioCuad = new Vector3(0.25f,6.25f,-1);
		cuadricula = null;
		Cuadricular();
	}
	
	// Update is called once per frame
	void Update () {

		cuadricula.transform.position = transform.position - posInicioCuad;
		
		if(fase == ARMADO && Input.GetButtonDown("Jump")){
			if(estado != CONGELADO){
				detenerLados();
				detenerCaida();
				mostrarCuadricula();
			}
			else{
				puedeCaer = true;
				puedeMover = true;
				estado = CAYENDO;
				deseleccionarBloque();
				ocultarCuadricula();
			}
		}

		if(fase == ACOPLADO && estado == CONGELADO){
			fase = ARMADO;
		}
		else if(transform.position.y < GameObject.Find("Limite").transform.position.y && estado != CONGELADO){
			fase = ACOPLADO;
		}

		if(estado != ASENTADO && estado != CONGELADO){
			if(puedeCaer == true){
				caer();
				StartCoroutine("esperarCaer", 0.25f);
			}
			
			if(Input.GetKey("a")){
				if(puedeMover == true && yaMovio == false){
					moverIzquierda();
					StartCoroutine("esperarMover",0.1f);
				}
			}
			else if(Input.GetKey("d") ){
				if(puedeMover == true && yaMovio == false){
					moverDerecha();
					StartCoroutine("esperarMover",0.1f);
				}
			}
		}
	}

	void detenerCaida(){
		StopCoroutine("esperarCaer");
		puedeCaer = false;
		estado = CONGELADO;
	}

	void detenerLados(){
		StopCoroutine("esperarMover");
		puedeMover = false;
	}

	bool verificarOcupado(string direccion){
		int numBloques = transform.childCount;
		Vector3 hijoPos;
		RaycastHit2D objeto;
		
		float valorX = 0;
		float valorY = 0;
		
		if(direccion == "Izquierda"){
			valorX = -0.5f;
			valorY = 0;
		}
		else if(direccion == "Derecha"){
			valorX = 0.5f;
			valorY = 0;
		}
		else if(direccion == "Abajo"){
			valorX = 0;
			valorY = -0.5f;
		}
		
		for(int i=0; i<numBloques; i++){
			hijoPos = transform.GetChild(i).transform.position;
			objeto = Physics2D.Linecast(hijoPos, new Vector2(hijoPos.x + valorX, hijoPos.y + valorY), 1 << LayerMask.NameToLayer("Obstaculos"));
			
			if(objeto.collider != null){
				if(objeto.collider.gameObject.tag != "Bloque"){
					Debug.Log("detectado " + objeto.collider.gameObject.name);
					return true;
				}
			}
		}
		return false;
	}
	
	IEnumerator esperarMover(float secs){
		puedeMover = false;
		yield return new WaitForSeconds(secs);
		puedeMover = true;
		yaMovio = false;
	}
	
	IEnumerator esperarCaer(float secs){
		puedeCaer = false;
		yield return new WaitForSeconds(secs);
		puedeCaer = true;
	}
	
	
	void moverIzquierda ()
	{
		if(verificarOcupado("Izquierda") == false){
			Vector3 pos = transform.position;
			pos.x -= 0.5f;
			transform.position = pos;
			yaMovio = true;
		}
	}
	
	void moverDerecha ()
	{
		if(verificarOcupado("Derecha") == false){
			Vector3 pos = transform.position;
			pos.x += 0.5f;
			transform.position = pos;
			yaMovio = true;
		}
	}
	
	void caer ()
	{
		if(verificarOcupado("Abajo") == false){
			Vector3 pos = transform.position;
			pos.y -= 0.5f;
			transform.position = pos;
		}
		else{
			StartCoroutine("esperarAsentar");
		}
	}

	IEnumerator esperarAsentar(){
		yield return new WaitForSeconds(0.5f);
		if(verificarOcupado("Abajo") == true)
			asentar();
	}
	
	void asentar ()
	{
		estado = ASENTADO;
		removerHijos();
		Destroy(this.gameObject);
		sControl.getInstancia.finalSentado = true;
	}
	
	public void removerHijos(){
		Transform hijo;
		BoxCollider2D col;
		
		for(int i = transform.childCount-1; i >= 0; i--){
			hijo = transform.GetChild(i);
			
			Instantiate(pfbBase, hijo.position, Quaternion.identity);
			Destroy(hijo.gameObject);
		}
	}

	public void eliminarCuadricula(){
		if(cuadricula!=null){
			Destroy(cuadricula);
		}
	}

	public void Cuadricular(){
		
		Vector3 posNueva;
		bool mismaPos;
		
		eliminarCuadricula();
		cuadricula = new GameObject("Cuadricula");
		//Debug.Log(cuadricula.transform.position.ToString("F2")+" / "+ transform.position.ToString("F2"));
		posInicioCuad = new Vector3(transform.position.x, transform.position.y, -1);
		
		for(int i=0; i<transform.childCount; i++){
			for(int j=0; j<4; j++){
				mismaPos = false;
				posNueva = transform.GetChild(i).transform.position;
				switch(j){
				case 0: posNueva.x += 0.5f; break;
				case 1: posNueva.x -= 0.5f; break;
				case 2: posNueva.y += 0.5f; break;
				case 3: posNueva.y -= 0.5f; break;
				}
				
				for(int k=0; k<transform.childCount; k++){
					if(posNueva == transform.GetChild(k).transform.position){
						mismaPos = true;
						break;
					}
				}
				
				if(!mismaPos){
					GameObject cuad = Instantiate(Resources.Load("Prefabs/BCuad"), posNueva, transform.rotation) as GameObject;
					cuad.name = "BCuad";
					cuad.transform.parent = cuadricula.transform;
					cuad.renderer.enabled = false;
				}
			}
		}
	}

	
	public void ocultarCuadricula(){
		Transform t = cuadricula.transform;
		
		for(int i=0; i<t.childCount; i++){
			t.GetChild(i).renderer.enabled = false;
		}
	}

	public void mostrarCuadricula(){
		
		Transform t = cuadricula.transform;
		
		for(int i=0; i<t.childCount; i++){
			t.GetChild(i).renderer.enabled = true;
		}
		
		cuadricula.transform.position = transform.position - posInicioCuad;
	}

	void deseleccionarBloque(){
		if(bloqueActivo != null){
			bloqueActivo.transform.localScale -= new Vector3(0.2f,0.2f,0);
			bloqueActivo = null;
		}
	}
}
