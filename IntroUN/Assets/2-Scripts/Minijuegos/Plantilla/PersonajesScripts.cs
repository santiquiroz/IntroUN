using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * juaalvarezme
 * 
 * Script para controlar la Scene de los MInijuegos
 */

public class PersonajesScripts : MonoBehaviour {

	//Variables generales
	public GameObject estudiante;
	private Animator estudianteAnim;

	public GameObject enemigo;
	private Animator enemigoAnim;

	public GameObject textDañoPrefab;
	public Text textoSalud;
	private float ataque;

	public GameObject minijuego;

	[SerializeField]
	private Image barraEnemigo;
	public GameObject mensaje;

	//Variables leidas
	private float puntosDaño;
	public float saludEnimigo;
	public float saludEnimigoTotal;

	//Variables de control
	private bool jefe;
	private bool gano;
	private int ataques;

	//Variables para Animaciones
	private string[] estudianteAtaques;
	private string[] jefeAtaques;
	private string[] fantasmaAtaques;
	private string recibirDano;
	private string muerte;
	private string victoria;

	private string tempAnimEstudiante;
	private string tempAnimEnemigo;

	//Sistemas de particulas
	public CartoonPS enemigoPS;
	public CartoonPS estudiantePS;

	//==================================================================================================
	/*
	 *	Inicialización de variables
	*/
	void Start () {

		estudianteAnim = estudiante.GetComponent<Animator> ();
		enemigoAnim = 	 enemigo.GetComponent<Animator> ();

		try{
			saludEnimigoTotal = GameControl.control.sceneManager.saludEnemigo;
			saludEnimigo = saludEnimigoTotal;
			puntosDaño = GameControl.control.sceneManager.poderAtaque;
			jefe = GameControl.control.sceneManager.parcial;
		}catch(System.Exception ex){
			Debug.Log ("Error en PersonajesScripts, producido por GameControl.control.sceneManager.");
			saludEnimigo = 400;
			puntosDaño = 80;
			jefe = false;
		}

		ataques = 0;
		mensaje.SetActive (false);
		textoSalud.text = saludEnimigo.ToString () + " hp";

		estudianteAtaques = new string[4]{"Attack 01", "Attack 02", "Double Attack", "Jump Attack"};
		jefeAtaques = 		new string[4]{"Slash Attack 01", "Slash Attack 02", "Double Attack", "Jump Attack"};
		fantasmaAtaques = 	new string[2]{"Bite Attack", "Cast Spell"};
		recibirDano = 		"Take Damage";
		muerte = 			"Die";
		victoria = 			"Victory";

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

		ataques++;

		tempAnimEnemigo = recibirDano;
		Invoke("enemigoAnimacion",0.5f);

		if (tempAnimEstudiante.Equals ("Double Attack")) {
			Invoke ("enemigoAnimacion", 1f);
		}

		if (saludEnimigo <= 0)
			Invoke("Ganar",1.2f);
		
		if (jefe && ataques%5 == 0) {
			gameObject.GetComponent<ControladorInicio> ().cambiarMinijuego ();
		}
	}

	//==================================================================================================
	/*
	 *	Metodo para generar animación al fallar un intento
	*/
	public void Fallar(){
		if(jefe)
			tempAnimEnemigo = jefeAtaques [Random.Range (0, jefeAtaques.Length)];
		else
			tempAnimEnemigo = fantasmaAtaques [Random.Range (0, fantasmaAtaques.Length)];
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
		if (tempAnimEstudiante.Equals (recibirDano)) {
			estudiantePS.Play();
		}
	}

	//==================================================================================================
	/*
	 * Ejecuta animacion del enemigo
	 * 		parametro:	-animación: Valor del Trigger a ejecutar
	*/
	void enemigoAnimacion(){
		enemigoAnim.SetTrigger(tempAnimEnemigo);
		barraEnemigo.fillAmount = Map (saludEnimigo, 0f, saludEnimigoTotal, 0f, 1f);

		if (tempAnimEnemigo.Equals (recibirDano)) {
			enemigoPS.Play();
			initTextoDaño (puntosDaño.ToString());
		}
	}

	//==================================================================================================
	/*
	 *	Metodo cuando se derrota al enemigo: ejecuta animación y termina el minijuego
	*/

	void Ganar(){
		gano = true;
		minijuego.SetActive (false);
		tempAnimEstudiante = victoria;
		estudinteAnimacion ();

		tempAnimEnemigo = muerte;
		Invoke("enemigoAnimacion",0.5f);

		mensaje.SetActive (true);
		actualizarGameControl(true);

		Invoke ("regresar", 6f);
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
		minijuego.SetActive (false);

		tempAnimEstudiante = muerte;
		Invoke("estudinteAnimacion",0.5f);

		mensaje.SetActive (true);
		actualizarGameControl(false);

		Invoke ("regresar", 6f);
	}


	//==================================================================================================
	/*
	 *	Metodo para actualizar la meta-data
	*/
	void actualizarGameControl(bool fin){
	
		string txt = "";
		if(fin)
			txt = "VICTORIA!\nExp: ";
		else
			txt = "TIEMPO!";

		//try{
			



		txt += GameControl.control.playerData.experiencia.ToString ();
		txt += " + ";
		txt += ataques.ToString ();

		if (jefe && GameControl.control.sceneManager.materia.cantidadParciales > 0) {
			float nota = calcularNota (fin);
			txt += "\nNota: " + nota.ToString ();


			int p = GameControl.control.sceneManager.materia.parciales.Length - GameControl.control.sceneManager.materia.cantidadParciales;
			GameControl.control.sceneManager.materia.parciales[p].nota = nota;
			GameControl.control.sceneManager.materia.parciales[p].estado = EstadosParciales.Realizado;
			GameControl.control.sceneManager.materia.porcentaje += GameControl.control.sceneManager.materia.parciales[p].porncentaje;
			GameControl.control.sceneManager.materia.nota += nota * GameControl.control.sceneManager.materia.parciales[p].porncentaje / GameControl.control.sceneManager.materia.porcentaje;
			GameControl.control.sceneManager.materia.cantidadParciales--;
		}else{
			GameControl.control.sceneManager.materia.cantidadEnemigos--;
		}

		GameControl.control.playerData.experiencia += ataques;
		/*
		}catch(System.Exception ex){
			txt += "?";
			txt += " + ";
			txt += ataques.ToString ();

			if (jefe) {
				float nota = calcularNota (true);
				txt += "\nNota: " + nota.ToString ();
			}
		}
		*/

		mensaje.GetComponent<Text>().text = txt;
	}


	//==================================================================================================
	/*
	 *	Metodo cuando se derrota al enemigo: ejecuta animación y termina el minijuego
	*/
	float calcularNota(bool gano){
		
		float nota = 0;
		float tiempo = GameObject.Find ("Timer").GetComponent<Timer> ().getTiempo ();
		float tiempoTotal = 0f;
		float saludTotal = 0f;
		try{
			tiempoTotal = GameControl.control.sceneManager.tiempo;
			saludTotal = GameControl.control.sceneManager.saludEnemigo;
		}catch(System.Exception ex){
			tiempoTotal = 45f;
			saludTotal = 400f;
		}

		if (gano && tiempo / tiempoTotal >= 0.5)
			nota = 5f;
		else if (gano) {
			nota = 4 * tiempo / tiempoTotal + 3;
		} else if (!gano && saludEnimigo < saludTotal ) {
			nota = -3 * saludEnimigo / saludTotal + 3;
		}else{
			nota = 0f;
		}

		nota = (float)System.Math.Round ((double)nota, 1);

		return nota;

	}

	//==================================================================================================
	/*
	 *	Cargar Escena Campus o Edificio
	*/
	void regresar(){
		if (jefe)
			GameControl.control.sceneManager.MinijuegoToCampus ();
		else
			GameControl.control.sceneManager.MinijuegoToInteriores ();
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


	//==================================================================================================
	/*
	 *	
	*/
	public void initTextoDaño(string text){
		GameObject temp = Instantiate (textDañoPrefab) as GameObject;
		RectTransform tempRect = temp.GetComponent<RectTransform> ();
		temp.transform.SetParent (enemigo.transform);
		tempRect.transform.localPosition = textDañoPrefab.transform.localPosition;
		tempRect.transform.localScale = textDañoPrefab.transform.localScale;
		tempRect.transform.localRotation = textDañoPrefab.transform.localRotation;

		temp.GetComponent<Text> ().text = text;
		temp.GetComponent<Animator>().SetTrigger("Hit");
		Destroy (temp.gameObject, 2);

		textoSalud.text = saludEnimigo.ToString () + " hp";
	}
}
