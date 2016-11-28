using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Temporizador2Script : MonoBehaviour {

	public float temporizador;
	public float puntoDeControl;
	float puntoDeControlCadaSeg = 8;
	public bool primerControl;

	public Text textoTiempo;
	public Canvas canvasPrincipal;
	//public Canvas canvasJuego;
	public GameObject canvasJuego;

	public Timer timerControl;

	// Use this for initialization
	void Start () {
		canvasJuego.GetComponent<CanvasJuegoScript2> ().DesactivarBotones ();
		textoTiempo.text = " ";
		temporizador = 45;
		puntoDeControl = temporizador;
		primerControl = false;
		timerControl.setIniciar (40);
	}

	// Update is called once per frame
	void Update () {
		if (temporizador >= 0) {
			temporizador -= Time.deltaTime;
			//textoTiempo.text = "Tiempo: " + temporizador.ToString ("<color=red> 0 </color>");
		} 
		else {
			//TiempoTerminado ();
		}

		if(puntoDeControl - temporizador >= puntoDeControlCadaSeg){
			puntoDeControl = temporizador;
			if (!primerControl) {
				primerControl = true;
				canvasJuego.GetComponent<CanvasJuegoScript2> ().ActivarBotones ();
				canvasJuego.GetComponent<CanvasJuegoScript2> ().VoltearImagenes ();
				timerControl.setIniciar (true);
			}
		}
	}

	public void TiempoTerminado(){
		canvasJuego.GetComponent<CanvasJuegoScript2> ().JuegoPerdido();
	}
}
