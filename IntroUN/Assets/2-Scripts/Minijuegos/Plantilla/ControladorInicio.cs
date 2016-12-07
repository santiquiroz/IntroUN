using UnityEngine;
using System.Collections;

public class ControladorInicio : MonoBehaviour {

	public GameObject minijuego;
	public GameObject estudiante;
	public GameObject enemigo;
	public GameObject timer;
	public GameObject preInicio;
	public GameObject vs;


	private Animator animEstudiante;
	private bool inicio;
	private bool finPreInicio;
	private Vector3 posEstudiante;

	private bool jefe;
	public bool jefeForTest;


	//Set de minijuegos -> NO TOCAR
	public GameObject[] minijuegos;
	public GameObject referencia;
	private int indexMinijuego;

	// Use this for initialization
	void Start () {


		try{
			jefe = GameControl.control.sceneManager.parcial;
		}catch(System.Exception ex){
			jefe = jefeForTest;
		}



		if (jefe) {
			indexMinijuego = 0;
			instanciarMinijuego ();
		}


		preInicio.SetActive (true);
		minijuego.SetActive (false);
		timer.SetActive (false);
		enemigo.SetActive (false);
		vs.SetActive (false);
		inicio = false;
		finPreInicio = false;

		posEstudiante = estudiante.transform.position;
		animEstudiante = estudiante.GetComponent<Animator> ();
		estudianteAnimación ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//==================================================================================================
	/*
	 *	Iniciar minijuego al oprimir el boton Start
	*/

	public void iniciar(){
		inicio = true;
		preInicio.SetActive (false);
		minijuego.SetActive (true);
		timer.SetActive (true);

		Invoke ("activarTimer", 0.5f);

		if (!finPreInicio) {
			enemigo.SetActive (true);
			animEstudiante.SetTrigger ("Walk");
			estudiante.transform.position = posEstudiante ;
		}

	}

	//==================================================================================================
	/*
	 *	Iniciar temporizador
	*/
	void activarTimer(){
		timer.GetComponent<Timer> ().setIniciar (true);
	}

	//==================================================================================================
	/*
	 *	Activar entrada del enemigo
	*/
	void enemigoAnimación(){
		if(jefe)
			enemigo.GetComponentInChildren<Animator> ().SetTrigger ("Cast Spell");
		else
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

	//==================================================================================================
	/*
	 *	Instancia un minijuego 
	*/
	void instanciarMinijuego(){

		if (minijuego != null)
			Destroy (minijuego);

		minijuego = Instantiate (minijuegos[indexMinijuego]) as GameObject;
		RectTransform tempRect = minijuego.GetComponent<RectTransform> ();
		minijuego.transform.SetParent (GameObject.Find("Canvas").transform);
		//tempRect.transform.localPosition = minijuegos[indexMinijuego].transform.localPosition;
		tempRect.localPosition = referencia.transform.localPosition;
		tempRect.transform.localScale = minijuegos[indexMinijuego].transform.localScale;
		tempRect.transform.localRotation = minijuegos[indexMinijuego].transform.localRotation;


		if (indexMinijuego == 0)
			minijuego.GetComponent<GeneradorOperaciones> ().controladorPersonajes = gameObject.GetComponent<PersonajesScripts> ();
		else if (indexMinijuego == 1)
			minijuego.GetComponent<CanvasJuegoScript> ().controladorPersonajes = gameObject.GetComponent<PersonajesScripts> ();	

		gameObject.GetComponent<PersonajesScripts> ().minijuego = minijuego;
		
	}

	//==================================================================================================
	/*
	 *	Cambia al siguiente minijuego
	*/
	public void cambiarMinijuego(){
		if (indexMinijuego + 1 >= minijuegos.Length)
			indexMinijuego = 0;
		else
			indexMinijuego++;

		instanciarMinijuego();
	}
}
