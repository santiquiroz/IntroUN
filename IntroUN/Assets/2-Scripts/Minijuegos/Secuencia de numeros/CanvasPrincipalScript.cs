using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasPrincipalScript : MonoBehaviour {

	public bool iniciarTemp;

	public GameObject canvasJuego;
	public Text textoInstrucciones;
	public Text textoTiempo;
	public Text textoInicio;

	public GameObject jugador;
	public GameObject enemigo;
	public GameObject vs;

	private bool inicio;
	private bool finPreInicio;

	private Animator animEstudiante;
	private Vector3 posEstudiante;

	public Timer timerControl;

	// Start(): Pantalla inicial. Se muestra el texto de instrucciones y el boton de inicio, y se desactivan los demás textos. 

	// Use this for initialization
	void Start () {
		canvasJuego.SetActive (false);
		textoInstrucciones.enabled = true;
		textoTiempo.enabled = false;
		textoTiempo.GetComponent<TemporizadorScript> ().temporizador = 0;
		textoInicio.enabled = true;
		iniciarTemp = false;
		enemigo.SetActive (false);
		finPreInicio = false;
		posEstudiante = jugador.transform.position;
		animEstudiante = jugador.GetComponent<Animator> ();
		estudianteAnimación ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/* inicioPresionado(): funcion aplicada al boton de inicio, el cual activa el canvas de juego, 
	y desactiva los textos de inicio e instrucciones*/ 

	public void inicioPresionado () {
		inicio = true;
		if (!finPreInicio) {
			enemigo.SetActive (true);
			animEstudiante.SetTrigger ("Walk");
			jugador.transform.position = posEstudiante ;
		}
		textoInstrucciones.enabled = false;
		textoInicio.enabled = false;
		canvasJuego.SetActive(true);
		textoTiempo.enabled = true;
		textoTiempo.GetComponent<TemporizadorScript> ().temporizador = 30;
		textoTiempo.GetComponent<TemporizadorScript> ().puntoDeControl = 30;
		textoTiempo.GetComponent<TemporizadorScript> ().primerControl = false;
		//iniciarTemp = true;
		timerControl.setIniciar (30);
		timerControl.setIniciar (true);
		canvasJuego.GetComponent<CanvasJuegoScript> ().imagenVolteada = false;
		canvasJuego.GetComponent<CanvasJuegoScript> ().iniciar ();
		//canvasJuego.GetComponent<CanvasJuegoScript> ().Posicionar ();
	}

	void enemigoAnimación(){
		enemigo.GetComponentInChildren<Animator> ().SetTrigger ("Surround Attack");
		animEstudiante.SetTrigger ("Cast Spell");
		finPreInicio = true;
	}

	void estudianteAnimación(){
		animEstudiante.SetTrigger ("Walk");

		Vector3 to = jugador.transform.position;
		Vector3 from = jugador.transform.position;
		from.x -= 8;

		jugador.transform.position = to;

		StartCoroutine(Move_Routine(jugador.transform, from, to	));
	}

	private IEnumerator Move_Routine(Transform transform, Vector3 from, Vector3 to)
	{
		float t = 0f;
		while(t < 1f && !inicio)
		{
			t += Time.deltaTime/3;
			transform.position = Vector3.Lerp(from, to, Mathf.SmoothStep(0f, 1f, t));
			//transform.position = Vector3.MoveTowards(from, to, Time.deltaTime * 50f);
			yield return null;
		}

		vs.SetActive (true);
		animEstudiante.SetTrigger ("Walk");

		Invoke ("animacionesBatalla", 1f);
	}

	//==================================================================================================
	/*
	 *	Activar animaciones de inicio
	*/
	void animacionesBatalla(){
		enemigo.SetActive (true);
		if(!inicio)
			Invoke ("enemigoAnimación", 1f);
	}

}
