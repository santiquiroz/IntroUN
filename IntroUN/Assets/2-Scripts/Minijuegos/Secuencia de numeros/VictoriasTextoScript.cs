using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoriasTextoScript : MonoBehaviour {

	// Use this for initialization

	public Text victoriasTexto;
	public Canvas canvasJuego;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (canvasJuego.GetComponent<CanvasJuegoScript> ().cont2 == 1) {
			victoriasTexto.text = "¡Felicidades, llevas <color=red>1</color> victoria!";
		} 
		else if (canvasJuego.GetComponent<CanvasJuegoScript> ().cont2 == 2) {
			victoriasTexto.text = "¡Felicidades, llevas <color=red>2</color> victorias. Te falta una!";
		}
	}
}
