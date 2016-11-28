using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PersonajesScript2 : MonoBehaviour {
	//Variables generales
	public GameObject estudiante;
	private Animator estudianteAnim;

	public GameObject enemigo;
	private Animator enemigoAnim;

	[SerializeField]
	public Image barraEnemigo;

	public GameObject mensaje;

	public Canvas canvasJuego;

	//Variables leidas
	private float puntosDaño;
	public float saludEnimigo;

	private bool jefe;
	private bool gano;

	//Variables para Animaciones
	private string[] estudianteAtaques;
	private string[] jefeAtaques;
	private string[] fantasmaAtaques;
	private string recibirDano;
	private string muerte;
	private string victoria;

	private string tempAnimEstudiante;
	private string tempAnimEnemigo;


	//==================================================================================================
	/*
	 *	Inicialización de variables
	*/
	void Start () {

		estudianteAnim = estudiante.GetComponent<Animator> ();
		enemigoAnim = 	 enemigo.GetComponent<Animator> ();

		saludEnimigo = 120;
		puntosDaño = 20;

		estudianteAtaques = new string[4]{"Attack 01", "Attack 02", "Double Attack", "Jump Attack"};
		jefeAtaques = 		new string[4]{"Slash Attack 01", "Slash Attack 02", "Double Attack", "Jump Attack"};
		fantasmaAtaques = 	new string[2]{"Bite Attack", "Cast Attack"};
		recibirDano = 		"Take Damage";
		muerte = 			"Die";
		victoria = 			"Victory";

		jefe = false;
		gano = false;

	}

	//==================================================================================================
	/*
	 *	Metodo para reducir salud del enemigo y generar animación
	*/
	public void Atacar(){

		saludEnimigo -= puntosDaño;

		tempAnimEstudiante = estudianteAtaques [Random.Range (0, 4)];
		estudinteAnimacion ();

		tempAnimEnemigo = recibirDano;
		Invoke("enemigoAnimacion",0.5f);

		if(tempAnimEstudiante.Equals("Double Attack"))
			Invoke("enemigoAnimacion",1f);

		if (saludEnimigo <= 0) {
			Invoke ("Ganar", 1.2f);
			//canvasJuego.GetComponent<CanvasJuegoScript> ().JuegoGanado ();
		}
	}

	//==================================================================================================
	/*
	 *	Metodo para generar animación al fallar un intento
	*/
	public void Fallar(){

		tempAnimEnemigo = fantasmaAtaques [Random.Range (0, 2)];
		enemigoAnimacion ();

		tempAnimEstudiante = recibirDano;
		Invoke ("estudinteAnimacion", 0.5f);;
	}

	//==================================================================================================
	/*
	 * Ejecuta animacion del estudiante
	 * 		parametro:	-animación: Valor del Trigger a ejecutar
	*/
	void estudinteAnimacion(){
		estudianteAnim.SetTrigger(tempAnimEstudiante);
	}

	//==================================================================================================
	/*
	 * Ejecuta animacion del enemigo
	 * 		parametro:	-animación: Valor del Trigger a ejecutar
	*/
	void enemigoAnimacion(){
		enemigoAnim.SetTrigger(tempAnimEnemigo);
		barraEnemigo.fillAmount = Map (saludEnimigo, 0f, 120f, 0f, 1f);
	}

	//==================================================================================================
	/*
	 *	Metodo cuando se derrota al enemigo: ejecuta animación y termina el minijuego
	*/

	void Ganar(){
		gano = true;
		tempAnimEstudiante = victoria;
		estudinteAnimacion ();

		tempAnimEnemigo = muerte;
		Invoke("enemigoAnimacion",0.5f);
	}

	//==================================================================================================
	/*
	 *	Metodo cuando se pierde el minijuego: ejecuta animación y termina el minijuego
	*/

	public void Perder(){

		if (jefe) {
			tempAnimEnemigo = victoria;
			enemigoAnimacion ();
		} else {
			tempAnimEnemigo = "Surround Attack";
			StartCoroutine (FantasmaWin ());
		}


		tempAnimEstudiante = muerte;
		Invoke("estudinteAnimacion",0.5f);
		mensaje.SetActive (true);
		mensaje.GetComponent<Text>().text = "TIEMPO!";
	}


	//==================================================================================================
	/*
	 *	Animación de victoria de fantasma en un loop
	*/
	IEnumerator FantasmaWin ()
	{
		while (true)
		{
			enemigoAnimacion ();
			yield return new WaitForSeconds (0.5f);
		}
	}

	//==================================================================================================
	/*
	 * Map para normalizar un valor
	 * 		parametros:	-value: Valor a normalizar
	 * 					-inMin: Minimo valor que puede tomar el parametro value
	 * 					-inMax: Maximo valor que puede tomar el parametro value
	 * 					-outMin: Minimo valor que puede tomar la salida
	 * 					-outMax: Maximo valor que puede tomar la salida
	 * 
	 * 		retorno:	-Valor normalizado
	 */
	private float Map(float value, float inMin, float inMax, float outMin, float outMax)
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	//==================================================================================================
	/*
	 *	Getter de la variable gano
	*/
	public bool getGano(){
		return gano;
	}

	public void RellenarVida(){
		saludEnimigo = 120;
		barraEnemigo.fillAmount = Map (saludEnimigo, 0f, 120f, 0f, 1f);
	}
}
