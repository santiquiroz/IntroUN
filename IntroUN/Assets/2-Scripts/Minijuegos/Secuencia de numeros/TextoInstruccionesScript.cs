using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextoInstruccionesScript : MonoBehaviour {

	public Text textoIns;

	//Este script tiene como finalidad agregarle texto a textoIns, se hace este script para cambiarle el color a 15 :P 

	// Use this for initialization
	void Start () {
		//textoIns.text = "Pulsa los numeros en orden ascendente, tienes <color=red>30</color> segundos para lograrlo, los ultimos <color=red>5</color> segundos " +
		//	"no podras ver los numeros. Debes ganar <color=red>3</color> veces";
		textoIns.text = "Pulsa los numeros en orden ascendente para ganar.";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
