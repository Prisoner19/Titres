using UnityEngine;
using System.Collections;

public class sControlSupremo : MonoBehaviour {

	public bool[] niveles;
	public bool[,] metas;
	public static sControlSupremo instancia;

	// Use this for initialization
	void Start () {
		instancia = this;
	
		metas = new bool[10,3];
		niveles = new bool[10];
		for(int i=0; i<=9; i++){
			niveles[i] = false;
			for(int j=0; j<=2; j++){
				metas[i,j] = false;
			}
		}

		niveles[0] = true;
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static sControlSupremo getInstancia{
		get{
			return instancia;	
		}
	}
}
