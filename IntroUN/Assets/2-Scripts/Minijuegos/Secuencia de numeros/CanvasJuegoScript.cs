using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasJuegoScript : MonoBehaviour {

	bool primerPresionado = false;
	public bool imagenVolteada;
	int botonPresionado;
	int cont;
	public int cont2;
	string tagBotonPresionado;

	//public GameObject jugador;
	//public GameObject enemigo;

	//public Text textoSalud;
	//public Image vsImagen;

	public Image cero;
	public Image uno;
	public Image dos;
	public Image tres;
	public Image cuatro;
	public Image cinco;

	public Sprite ceroSprite;
	public Sprite unoSprite;
	public Sprite dosSprite;
	public Sprite tresSprite;
	public Sprite cuatroSprite;
	public Sprite cincoSprite;
	public Sprite fondoMorado;
	public Sprite fondoAmarillo;
	public Sprite fondoVerde;
	public Sprite fondoRojo;
	public Sprite fondoRosado;
	public Sprite fondoAzul;

	public PersonajesScripts controladorPersonajes;

	//public Text perderTexto;
	//public Text ganarTexto;
	//public Text textoTiempo;
	//public Text textoReset;
	//public Text textoSalir;
	//public Text victoriasTexto;

	public Transform[] botones = new Transform[6];
	Vector3[] posBotones = new Vector3[6];
	int[] numeros = new int[6];


	// Use this for initialization
	void Start () {
		//victoriasTexto.enabled = false;
		//perderTexto.enabled = false;
		//ganarTexto.enabled = false;
		//textoReset.enabled = false;
		//textoSalir.enabled = false;
		GenerarNumeros ();
		GenerarPosBotones (posBotones);
		Posicionar ();
		cont2 = 0;
	}

	/* En la funcion Update(): Se recoge informacion de los botones presionados, se utiliza un bloque try/catch 
	  para evitar una excepción de referencia nula puesto que en todo momento no se están presionando botones. 
	  Al presionarse un boton quedara guardado el "tag" de este. Al ser el tag de un objeto un String 
	  debemos hacer una conversión de tipo TryParse puesto que todo boton presionado no tiene como tag un numero 
	  (por ejemplo las bombas tienen el tag: bomba), la conversion queda guardada en otra variable. 
	 * Se verifica también si el contador es 9, si esto es cierto, se gana el juego. */

	// Update is called once per frame
	void Update () {
		try{
			tagBotonPresionado = EventSystem.current.currentSelectedGameObject.tag;
		}
		catch(NullReferenceException ex) {
			//Debug.Log ("No clicked box");
		}catch(MissingReferenceException ex){
			
		}

		int.TryParse (tagBotonPresionado, out botonPresionado);


	}

	public void iniciar(){
		GenerarNumeros ();
		GenerarPosBotones (posBotones);
		Posicionar ();
		cont2 = 0;
	}


	public void VerificarPuntos(){
		if( cont == 5){
			controladorPersonajes.Atacar ();
			if (controladorPersonajes.saludEnimigo <= 0) {
				//textoTiempo.gameObject.SetActive (false);
				Invoke ("JuegoGanado",5f);
			} 
			else {
				ReiniciarPresionado ();
			}
		}
	}

	IEnumerator Esperar(){
		yield return new WaitForSecondsRealtime (5);
		JuegoGanado ();
	}

	IEnumerator Esperar2(){
		yield return new WaitForSecondsRealtime (5);
		ReiniciarPresionado ();
	}

	public void GenerarNumeros(){

		for (int i = 0; i < numeros.Length; i++) {
			numeros [i] = UnityEngine.Random.Range (0, 99);
		}

		Array.Sort (numeros);
		cero.GetComponentInChildren<Text> ().text = numeros [0].ToString ();
		uno.GetComponentInChildren<Text> ().text = numeros [1].ToString ();
		dos.GetComponentInChildren<Text> ().text = numeros [2].ToString ();
		tres.GetComponentInChildren<Text> ().text = numeros [3].ToString ();
		cuatro.GetComponentInChildren<Text> ().text = numeros [4].ToString ();
		cinco.GetComponentInChildren<Text> ().text = numeros [5].ToString ();
	}

	/* GenerarPosBotones(): Está función guarda la posción de los botones, en un arreglo que después será "barajado" para dar el
	   efecto de botones aleatorios */

	public void GenerarPosBotones(Vector3[] posB){
		for(int i = 0; i < posB.Length; i++){
			posB [i] = botones [i].localPosition;
			print (botones [i].position);
		}
	}

	/* Posicionar(): Está función llama a la función que baraja el arreglo con las posiciones de los botones, y ya barajadas 
	   las posiciones le reasigna las posiciones a los botones */ 

	public void Posicionar(){
		Barajar (posBotones);
		for(int i = 0; i < botones.Length; i++){
			botones [i].localPosition = posBotones [i];
		}
	}

	/* Baraajar(): Está función intercambia los valores entre las posiciones de un arreglo. Se genera un valor aleatorio que va
	   a ser cambiado por la posicion en i del arreglo */ 

	public void Barajar(Vector3[] pos){
		for(int i = 0; i < pos.Length; i++){
			Vector3 temp = pos[i];
			int posIndex = UnityEngine.Random.Range (0,(pos.Length - 1));
			pos[i] = pos[posIndex];
			pos [posIndex] = temp;
		}
	}


	/* NumeroPresionado(): Funcion aplicada a los botones con numeros. Esta funcion verifica si el primer numero 
	presionado es cero, de lo contrario se perdera el juego. Si ya se presiono el primer boton, 
	el siguiente boton a presionar sera el que sea una unidad mayor al contador, de lo contrario se perdera el juego */

	public void NumeroPresionado(){
		if (primerPresionado == false) {
			primerPresionado = true;
			//buttonPressed = int.Parse (EventSystem.current.currentSelectedGameObject.tag);
			if (botonPresionado == 0) {
				cont = 0;
				GameObject.FindGameObjectWithTag (tagBotonPresionado).SetActive(false);
				//canvasPrincipal.GetComponent<PersonajesScripts> ().Atacar ();
			} else {
				//JuegoPerdido();
				ReiniciarPresionado();
			}
		} 
		else {
			if (cont == (botonPresionado - 1)) {
				cont++;
				GameObject.FindGameObjectWithTag (tagBotonPresionado).SetActive(false);
				//canvasPrincipal.GetComponent<PersonajesScripts> ().Atacar ();
				VerificarPuntos ();
			} 
			else {
				//JuegoPerdido ();
				ReiniciarPresionado();
			}
		}
	}

	/* JuegoPerdido(): Esta función desactiva las imagenes de juego (bombas, numeros), detiene el temporizador, 
	  y muestra en pantalla el texto de perdida y los botones para reiniciar o salir del juego */

	public void JuegoPerdido () {
		//canvasPrincipal.GetComponent<PersonajesScripts> ().RellenarVida ();
		cero.gameObject.SetActive(false);
		uno.gameObject.SetActive(false);
		dos.gameObject.SetActive(false);
		tres.gameObject.SetActive(false);
		cuatro.gameObject.SetActive(false);
		cinco.gameObject.SetActive(false);
		//perderTexto.enabled = true;
		//textoReset.enabled = true;
		//textoSalir.enabled = true;
		//textoTiempo.gameObject.SetActive (false);
		cont2 = 0;
		//enemigo.SetActive (false);
		//jugador.SetActive (false);
		//textoSalud.enabled = false;
		//vsImagen.enabled = false;
	}

	/*JuegoGanado(): Esta función desactiva las imagenes del juego (bombas, numeros), detiene el temporizador 
	 y activa el texto de ganar. (Se aclara que al ganarse el juego no hay boton de salir, puesto que se hará 
	 un cambio de escena automatico) */

	public void JuegoGanado(){
		//textoSalud.enabled = false;
		//vsImagen.enabled = false;
		//enemigo.SetActive (false);
		//jugador.SetActive (false);
		cero.gameObject.SetActive(false);
		uno.gameObject.SetActive(false);
		dos.gameObject.SetActive(false);
		tres.gameObject.SetActive(false);
		cuatro.gameObject.SetActive(false);
		cinco.gameObject.SetActive(false);
		//ganarTexto.enabled = true;
		//textoTiempo.gameObject.SetActive (false);
		//textoTiempo.GetComponent<TemporizadorScript> ().temporizador = 0;
	}

	/* ReiniciarPresionado(): Función aplicada al boton de reinicio. Esta función desactiva los textos activados 
	   en la funcion Juego Perdido y re-activa las imagenes del juego, hace contador = 0, primerPresionado = false 
	   y reinicia el temporizador a su cuenta regresiva. Dando así la sensación que se inicia un nuevo juego. */

	public void ReiniciarPresionado(){
		//enemigo.SetActive (true);
		//jugador.SetActive (true);
		//victoriasTexto.enabled = false;
		//perderTexto.enabled = false;
		//ganarTexto.enabled = false;
		//textoReset.enabled = false;
		//textoSalir.enabled = false;
		controladorPersonajes.Fallar ();
		GenerarNumeros ();
		cero.gameObject.SetActive(true);
		uno.gameObject.SetActive(true);
		dos.gameObject.SetActive(true);
		tres.gameObject.SetActive(true);
		cuatro.gameObject.SetActive(true);
		cinco.gameObject.SetActive(true);
		//textoSalud.enabled = true;
		//vsImagen.enabled = true;
		Posicionar ();
		cont = 0;
		primerPresionado = false;
		//textoTiempo.gameObject.SetActive (true);
		///textoTiempo.GetComponent<TemporizadorScript> ().temporizador = 30;
		//textoTiempo.GetComponent<TemporizadorScript> ().puntoDeControl = 30;
		//textoTiempo.GetComponent<TemporizadorScript> ().primerControl = false;
		imagenVolteada = true;
		VoltearImagen ();
	}

	/* VoltearImagen(): Está función cambia el sprite de la imagen */

	public void VoltearImagen() {
		if (!imagenVolteada) {
			imagenVolteada = true;
			cero.sprite = fondoRosado;
			uno.sprite = fondoRojo;
			dos.sprite = fondoVerde;
			tres.sprite = fondoAzul;
			cuatro.sprite = fondoMorado;
			cinco.sprite = fondoAmarillo;
			cero.GetComponentInChildren<Text> ().enabled = false;
			uno.GetComponentInChildren<Text> ().enabled = false;
			dos.GetComponentInChildren<Text> ().enabled = false;
			tres.GetComponentInChildren<Text> ().enabled = false;
			cuatro.GetComponentInChildren<Text> ().enabled = false;
			cinco.GetComponentInChildren<Text> ().enabled = false;
		} 
		else {
			imagenVolteada = false;
			cero.sprite = ceroSprite;
			uno.sprite = unoSprite;
			dos.sprite = dosSprite;
			tres.sprite = tresSprite;
			cuatro.sprite = cuatroSprite;
			cinco.sprite = cincoSprite;
			cero.GetComponentInChildren<Text> ().enabled = true;
			uno.GetComponentInChildren<Text> ().enabled = true;
			dos.GetComponentInChildren<Text> ().enabled = true;
			tres.GetComponentInChildren<Text> ().enabled = true;
			cuatro.GetComponentInChildren<Text> ().enabled = true;
			cinco.GetComponentInChildren<Text> ().enabled = true;
		}
	} 

}
