using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TemporizadorScript : MonoBehaviour {

	public float temporizador;
	public float puntoDeControl;
	//public float puntoDeControl2;
	float puntoDeControlCadaSeg= 25;
	//float cambiarPosCadaSeg = 8;
	public bool primerControl;

	public Text textoTiempo;
	public Canvas canvasJuego;
	public Canvas canvasPrincipal;



	// Use this for initialization
	void Start () {
		textoTiempo.text = "";
		temporizador = 30;
		puntoDeControl = temporizador;
		//puntoDeControl2 = temporizador;
		primerControl = false;
	}

	/* Se verifica que el temporizador sea >= 0 y que iniciar temportizador sea verdadero, cumpliendose esto, 
	   se le resta al temporizador el tiempo (y se muestra en pantalla de a un segundo). Si tiempo < 0, 
	   se pierde el juego  */ 
	 
	/* Se verifica cada vez que han transcurrido 5 segundos en el update, se utiliza una variable bool para evtar llamar a la 
	funcion () cada vez que pasan 5 segundos, sino unicamente la primera vez */ 

	// Update is called once per frame
	void Update () {
		if (temporizador >= 0 && canvasPrincipal.GetComponent<CanvasPrincipalScript>().iniciarTemp) {
			temporizador -= Time.deltaTime;
			textoTiempo.text = "Tiempo: " + temporizador.ToString ("<color=red> 0 </color>");
		} 
		else {
			if (canvasPrincipal.GetComponent<CanvasPrincipalScript> ().iniciarTemp) {
				TiempoTerminado ();
			}
		}

		if (puntoDeControl - temporizador >= puntoDeControlCadaSeg) {
			puntoDeControl = temporizador;
			if(primerControl == false){
				primerControl = true;
				//canvasJuego.GetComponent<CanvasJuegoScript> ().VoltearImagen();
			}
		}

		/*if (puntoDeControl2 - temporizador >= cambiarPosCadaSeg) {
			puntoDeControl2 = temporizador;
			canvasJuego.GetComponent<CanvasJuegoScript> ().Posicionar ();
		}*/
	}

	/* TiempoTerminado(): Cuando el tiempo es < 0 se pierde el juego */

	public void TiempoTerminado(){
		Debug.Log ("Tiempo terminado juego perdido");
		canvasJuego.GetComponent<CanvasJuegoScript> ().JuegoPerdido ();
	}
}
