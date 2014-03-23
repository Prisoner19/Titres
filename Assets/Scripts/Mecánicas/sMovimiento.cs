using UnityEngine;
using System.Collections;

public class sMovimiento : MonoBehaviour {
	
	public GameObject pfbBase;
	
	private bool puedeMover;
	private bool puedeCaer;
	private bool yaMovio;
	private Vector3 posInicioCuad;

	private float intervaloCaida;

	public int estado;

	private const int CONGELADO = 0;
	private const int CAYENDO = 1;
	private const int MOVIENDOSE = 2;
	private const int ASENTADO = 3;

	private GameObject cuadricula;
	private GameObject guia;

	public sBloque bloqueActivo;
	private Vector3 posLocalBloqueGuia;
	private Vector3 posGlobalBloqueGuia;
	
	// Use this for initialization
	void Start () {

		intervaloCaida = 0.25f;

		estado = CAYENDO;

		puedeMover = true;
		yaMovio = false;
		puedeCaer = true;
		sControl.getInstancia.finalSentado = false;

		posInicioCuad = new Vector3(0.25f,6.25f,-1);
		cuadricula = null;
		Cuadricular();

		guia = null;
		crearGuia();
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log(puedeMover + " / " + estado + " / "+ yaMovio);

		cuadricula.transform.position = transform.position - posInicioCuad;
		
		if(Input.GetButtonDown("Jump")){
			if(estado != CONGELADO){
				detenerLados();
				detenerCaida();
				mostrarCuadricula();
			}
			else{
				puedeCaer = true;
				puedeMover = true;
				yaMovio = false;
				desacelerarCaida();
				estado = CAYENDO;
				deseleccionarBloque();
				ocultarCuadricula();
			}
		}


		if(estado != ASENTADO && estado != CONGELADO){
			if(puedeCaer == true){
				caer();
				StartCoroutine("esperarCaer", intervaloCaida);
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

			if(Input.GetKeyDown("s")){
				acelerarCaida();
				//transform.position = guia.transform.position;
			}
			else if(Input.GetKeyUp("s")){
				desacelerarCaida();
			}
		}
	}

	void acelerarCaida ()
	{
		intervaloCaida = 0.025f;
	}

	void desacelerarCaida ()
	{
		intervaloCaida = 0.25f;
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
			actualizarGuia();
		}
	}
	
	void moverDerecha ()
	{
		if(verificarOcupado("Derecha") == false){
			Vector3 pos = transform.position;
			pos.x += 0.5f;
			transform.position = pos;
			yaMovio = true;
			actualizarGuia();
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
		yield return new WaitForSeconds(0.2f);
		if(verificarOcupado("Abajo") == true)
			asentar();
	}
	
	void asentar ()
	{
		estado = ASENTADO;
		removerHijos();
		Destroy(cuadricula);
		Destroy(guia);
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

	void crearGuia()
	{
		guia = new GameObject("Guia");

		for(int i=0; i<transform.childCount; i++)
		{
			GameObject hijoGuia = Instantiate(Resources.Load("Prefabs/BGuia"), transform.position, transform.rotation) as GameObject;
			hijoGuia.name = "BGuia";
			hijoGuia.transform.parent = guia.transform;
		}

		actualizarGuia();
	}

	public void actualizarGuia()
	{
		Vector3 posHijo;
		Vector3 posLocalHijo;
		Vector3 posNueva;
		float posY;

		posY = calcularPosicionGuias();

		//Debug.Log(posY);
		Debug.Log(posGlobalBloqueGuia.ToString("F2"));
		if(posY != Mathf.NegativeInfinity)
		{
			for(int i=0; i<transform.childCount; i++)
			{
				posHijo = transform.GetChild(i).position;
				posLocalHijo = transform.GetChild(i).localPosition;

				posNueva = posGlobalBloqueGuia + posLocalHijo - posLocalBloqueGuia;

				guia.transform.GetChild(i).position = posNueva;
			}
		}
	}

	public float calcularPosicionGuias()
	{
		int alturaMax = 0;
		int valorAltura;
		Vector3 posHijo;

		float posYMinHijo = Mathf.Infinity;
		float posYHijo;

		for(int i=0; i<transform.childCount; i++)
		{
			posHijo = transform.GetChild(i).position;
			posYHijo = posHijo.y;
			valorAltura = calcularAlturaBloqueGuia(posHijo);

			if(valorAltura == -1)
			{
				return Mathf.NegativeInfinity;
			}

			if(valorAltura > alturaMax)
			{
				alturaMax = valorAltura;
				posLocalBloqueGuia = transform.GetChild(i).localPosition;
				posGlobalBloqueGuia = new Vector3(posHijo.x, (alturaMax * 1.0f)/2 - 4.75f, posHijo.z);
//				Debug.Log(posLocalBloqueGuia + " /// " + posGlobalBloqueGuia);
			}
			else if(valorAltura == alturaMax)
			{
				if(posYHijo < posYMinHijo)
				{
					posYMinHijo = posYHijo;
					alturaMax = valorAltura;
					posLocalBloqueGuia = transform.GetChild(i).localPosition;
					posGlobalBloqueGuia = new Vector3(posHijo.x, (alturaMax * 1.0f)/2 - 4.75f, posHijo.z);
				}
			}
		}

		//Debug.Log((alturaMax * 1.0f)/2 - 4.75f);
		return (alturaMax * 1.0f)/2 - 4.75f;
	}

	public int calcularAlturaBloqueGuia(Vector3 posBloque)
	{
		int linea = Mathf.FloorToInt((posBloque.y + 4.75f)*2);
		int columna = Mathf.FloorToInt((posBloque.x + 2.25f)*2);
		int posY = -1;
		int max;

		if(linea > 20)
		{
			max = 20;
		}
		else
		{
			max = linea;
		}

		for(int i=0; i<max; i++)
		{
			if(sControl.getInstancia.matrizOcupados[i,columna] == false)
			{
				if(i-1 < 0 || sControl.getInstancia.matrizOcupados[i-1,columna] == true)
				{
					posY = i;
				}
			}
		}

		//Debug.Log(posY+ " / " + columna);
		return posY;
	}
}
