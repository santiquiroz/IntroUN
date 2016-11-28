using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasPpalScript : MonoBehaviour {

	public GameObject estudiante;
	public GameObject enemigo;
	public GameObject vs;
	public GameObject timer;

	private Animator animEstudiante;
	private bool inicio;
	private bool finPreInicio;
	private Vector3 posEstudiante;

	public Text textoInstrucciones;
	public Text textoTiempo;
	public Text textoInicio; 
	//public Canvas canvasJuego;
	public GameObject canvasJuego;

	public bool iniciarTemp;



	// Use this for initialization
	void Start () {
		canvasJuego.SetActive(false);
		textoInstrucciones.enabled = true;
		textoTiempo.gameObject.SetActive (false);
		timer.SetActive (false);
		textoInicio.enabled = true;
		iniciarTemp = false;

		enemigo.SetActive (false);
		vs.SetActive (false);
		inicio = false;
		finPreInicio = false;

		posEstudiante = estudiante.transform.position;
		animEstudiante = estudiante.GetComponent<Animator> ();
		estudianteAnimación ();
	}

	public void InicioPresionado(){
		inicio = true;
		if (!finPreInicio) {
			enemigo.SetActive (true);
			animEstudiante.SetTrigger ("Walk");
			estudiante.transform.position = posEstudiante ;
		}

		estudiante.gameObject.SetActive (true);
		//canvasJuego.GetComponent<CanvasJuegoScript2> ().enemigo.gameObject.SetActive (true);
		canvasJuego.SetActive(true);
		textoInstrucciones.enabled = false;
		textoInicio.enabled = false;
		textoTiempo.gameObject.SetActive(true);
		iniciarTemp = true;
		timer.SetActive (true);

		textoTiempo.GetComponent<Temporizador2Script> ().primerControl = false;

		// falta imagen volteada y posicionar...
	}

	void enemigoAnimación(){
		enemigo.GetComponentInChildren<Animator> ().SetTrigger ("Surround Attack");
		animEstudiante.SetTrigger ("Cast Spell");
		finPreInicio = true;
	}

	//==================================================================================================
	/*
	 *	Activar entrada del estudiante
	*/
	void estudianteAnimación(){
		animEstudiante.SetTrigger ("Walk");

		Vector3 to = estudiante.transform.position;
		Vector3 from = estudiante.transform.position;
		from.x -= 8;

		estudiante.transform.position = to;

		StartCoroutine(Move_Routine(estudiante.transform, from, to	));
	}

	//==================================================================================================
	/*
	 *	Corutina para el movimiento del estudiante
	*/
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
