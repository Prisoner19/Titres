using UnityEngine;
using System.Collections;

public class sMovimiento : MonoBehaviour {
	
	public GameObject pfbBase;
	
	private bool puedeMover;
	private bool puedeCaer;
	private bool yaMovio;
	private Vector3 posInicioCuad;
	private Vector3 posCuadricula;

	private float intervaloCaida;
	private bool acelerandoCaida;

	public int estado;

	private const int CONGELADO = 0;
	private const int CAYENDO = 1;
	private const int MOVIENDOSE = 2;
	private const int ASENTADO = 3;

	private GameObject cuadricula;
	private GameObject guia;

	public sBloque bloqueActivo;
	private int[,] bloquesGuia;
	private float[,] bloquesLocalGuia;


	// Use this for initialization
	void Start () {

		intervaloCaida = 0.25f;
		acelerandoCaida = false;

		estado = CAYENDO;

		puedeMover = true;
		yaMovio = false;
		puedeCaer = true;
		sControl.getInstancia.finalSentado = false;

		posInicioCuad = new Vector3(0.25f,6.25f,-1);
		posCuadricula = transform.position;

		cuadricula = null;
		Cuadricular();

		guia = null;
		crearGuia();
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log(puedeMover + " / " + estado + " / "+ yaMovio);
		posCuadricula = transform.position - posInicioCuad;
		posCuadricula.z = 0;
		cuadricula.transform.position = posCuadricula;

		Debug.Log(acelerandoCaida);

		if(Input.GetButtonDown("Jump")){
			if(acelerandoCaida == true)
			{
				chantar();
				puedeCaer = true;
				puedeMover = true;
				yaMovio = false;
				estado = CAYENDO;
				deseleccionarBloque();
				ocultarCuadricula();
			}
			else
			{
				if(estado != CONGELADO)
				{
					detenerLados();
					detenerCaida();
					mostrarCuadricula();
				}
				else
				{
					puedeCaer = true;
					puedeMover = true;
					yaMovio = false;
					desacelerarCaida();
					estado = CAYENDO;
					deseleccionarBloque();
					ocultarCuadricula();
				}
			}
		}

		if(Input.GetKey("s"))
		{
			acelerandoCaida = true;
			if(estado != CONGELADO)
			{
				acelerarCaida();
			}
		}
		else if(Input.GetKeyUp("s"))
		{
			acelerandoCaida = false;
			if(estado != CONGELADO)
			{
				desacelerarCaida();
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

	void chantar()
	{
		Vector3 posBloque;
		Vector3 posLocalBloque;
		int linea;
		int columna; 
	
		for(int i=0; i<transform.childCount; i++)
		{
			posBloque = transform.GetChild(i).position;
			posLocalBloque = transform.GetChild(i).localPosition;
			linea = Mathf.FloorToInt((posBloque.y + 4.75f)*2);
			columna = Mathf.FloorToInt((posBloque.x + 2.25f)*2); 

			for(int j=0; j<transform.childCount; j++)
			{
				if(bloquesLocalGuia[j,0] == posLocalBloque.y && bloquesLocalGuia[j,1] == posLocalBloque.x)
				{
					posBloque = new Vector3((bloquesGuia[j,1] * 1.0f)/2 - 2.25f, (bloquesGuia[j,0] * 1.0f)/2 - 4.75f, posBloque.z);
					transform.GetChild(i).position = posBloque;
					break;
				}
			}
		}
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

		sBloque scrHijo;
		sBase scrBloqueBase;
		GameObject bloqueBase;
		
		for(int i = transform.childCount-1; i >= 0; i--){
			hijo = transform.GetChild(i);
			scrHijo = hijo.gameObject.GetComponent("sBloque") as sBloque;

			bloqueBase = Instantiate(pfbBase, hijo.position, Quaternion.identity) as GameObject;

			if(scrHijo.tipo == 2)
			{
				scrBloqueBase = bloqueBase.GetComponent("sBase") as sBase;
				scrBloqueBase.metalear();
			}

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
		posInicioCuad = new Vector3(transform.position.x, transform.position.y, 0);
		
		for(int i=0; i<transform.childCount; i++){
			for(int j=0; j<4; j++){
				mismaPos = false;
				posNueva = transform.GetChild(i).transform.position;
				posNueva.z = -1;
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

//	bool verificarBloquesUnidos()
//	{
//		Vector3 hijoPos;
//		RaycastHit2D objeto;
//
//		for(int i=0; i<transform.childCount; i++)
//		{
//			hijoPos = transform.GetChild(i).transform.position;
//			for(int j=0; j<4; j++)
//			{
//				//objeto = Physics2D.Linecast(hijoPos, new Vector2(hijoPos.x + valorX, hijoPos.y + valorY), 1 << LayerMask.NameToLayer("BloquesFigura"));
//			}
//		}
//		return true;
//	}

	void crearGuia()
	{
		guia = new GameObject("Guia");
		bloquesGuia = new int[4,2];
		bloquesLocalGuia = new float[4,2];

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
		inicializarBloquesGuia();
		posicionarBloquesGuia();
	}

	void inicializarBloquesGuia()
	{
		int linea;
		int columna;
		int posyMin ;
		Vector3 posBloque;
		Vector3 posNueva;

		for(int i=0; i<transform.childCount; i++)
		{
			posBloque = transform.GetChild(i).position;
			linea = Mathf.FloorToInt((posBloque.y + 4.75f)*2);
			columna = Mathf.FloorToInt((posBloque.x + 2.25f)*2); 

			bloquesGuia[i,0] = linea;
			bloquesGuia[i,1] = columna;
		}

		posyMin = calcularMaxMinColumnaGuias(false);

		for(int i=0; i<transform.childCount; i++)
		{
			bloquesGuia[i,0] = bloquesGuia[i,0] - posyMin;
		}
	}

	void posicionarBloquesGuia()
	{
		int indice = 0;
		int posY;
		int posX;
		int max;
		bool ocupado;
		bool ocupadoAbajo;
		Vector3 posNueva;
		int linea = Mathf.FloorToInt((transform.position.y + 4.75f)*2);

		if(linea >  20 - calcularMaxMinColumnaGuias(true))
		{
			max =  20 - calcularMaxMinColumnaGuias(true);;
		}
		else
		{
			max = linea;
		}

		for(int i=0; i<max; i++)
		{
			ocupado = false;
			ocupadoAbajo = false;
			for(int j=0; j<transform.childCount; j++)
			{
				posY = bloquesGuia[j,0];
				posX = bloquesGuia[j,1];
				if(sControl.getInstancia.matrizOcupados[posY+i,posX] == true)
				{
					ocupado = true;
					break;
				}
				else
				{
					if(posY+i-1 < 0 || sControl.getInstancia.matrizOcupados[posY+i-1,posX] == true)
					{
						ocupadoAbajo = true;
					}
				}
			}
			if(ocupado == false && ocupadoAbajo == true)
			{
				indice = i;
			}
		}

		for(int i=0; i<transform.childCount; i++)
		{
			bloquesGuia[i,0] = bloquesGuia[i,0] + indice;
			bloquesLocalGuia[i,0] = transform.GetChild(i).localPosition.y;
			bloquesLocalGuia[i,1] = transform.GetChild(i).localPosition.x;

			posNueva = new Vector3((bloquesGuia[i,1] * 1.0f)/2 - 2.25f, (bloquesGuia[i,0] * 1.0f)/2 - 4.75f, transform.position.z+1);
			guia.transform.GetChild(i).position = posNueva;
		}
	}

	
	int calcularMaxMinColumnaGuias(bool max)
	{
		int valor = bloquesGuia[0,0];
		
		for(int i=0; i<4; i++)
		{
			if(max == true)
			{
				if(bloquesGuia[i,0] > valor)
				{
					valor = bloquesGuia[i,0];
				}
			}
			else
			{
				if(bloquesGuia[i,0] < valor)
				{
					valor = bloquesGuia[i,0];
				}
			}

		}
		return valor;
	}
}