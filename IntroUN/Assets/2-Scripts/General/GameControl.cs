using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


/*
 * juaalvarezme
 * Script para el control general del juego (meta-data) y el paso de info entre Escenas
 */


public class GameControl : MonoBehaviour {

	public static GameControl control;

	//Datos estudiante
	public float experiencia;
	public float energiaTotal;
	public float energiaActual;
	public float estresTotal;
	public float estresActual;

	public float semestre;
	public float papa;
	public string nivel;
	public int cantidadMaterias;
	public string[] materias;
	public Dictionary<string, int> coloresMaterias = new Dictionary<string, int>();
	public Dictionary<string, float> notas = new Dictionary<string, float>();
	public Dictionary<string, int> parciales = new Dictionary<string, int>();
	public Dictionary<string, int> enemigosMateria = new Dictionary<string, int>();
	public Vector3 ultimaPosicion;
	public Vector3 ultimaPosicionCamara;
	public float expEsperada;
	public float poderAtaque;



	//Variables info entre Escenas
		//campus:
	public int dias = 6;
	public int semanas = 16;
	public int categoriMinijuego;
			/*
			 * 0: Calculo
			 * 1: Física
			 * 2: Ingles
			 * */
		//edificio
	public int saludEnemigo;
	public Vector3 ultimaPosEdificio;
	public Vector3 ultimaPosEdificioCam;
	public bool desdeMinijuego;
	public int indexUtimoEnemigo;
		//minijuegos
	public bool parcial;
	public float tiempo;


	//temp
	public int diaAlerta = 5;

	//Variables generales
	public string ruta;
	private Vector3 posicionInicial;
	private Vector3 posicionInicialCamara;
	public bool load;
	public Text texto;
	GameObject player;
	GameObject camara;


	private bool resetar;

	//==================================================================================================
	/*
	 * Comprobación de que solo exista un objeto GameControl
	*/
	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this)
			Destroy (gameObject);
		else
			control = this;

		resetar = false;
		defaultValues ();
		Load ();


	}

	void defaultValues(){
		
		ruta = Application.persistentDataPath + Path.DirectorySeparatorChar + "playerInfo.dat";
		posicionInicial = new Vector3 (896f, 8f, 128);
		posicionInicialCamara = new Vector3 (850f, 250f, -109f);

		experiencia = 100;
		energiaTotal = 100;
		energiaActual = energiaTotal;
		estresTotal = 100;
		estresActual = estresTotal;

		semestre = 1;
		cantidadMaterias = 3;
		materias = new string[3]{ "Calculo", "Fisica", "Ingles" };

		coloresMaterias = new Dictionary<string, int>();
		notas = new Dictionary<string, float>();
		parciales = new Dictionary<string, int>();
		enemigosMateria = new Dictionary<string, int>();


		coloresMaterias.Add ("Calculo", 1);
		coloresMaterias.Add ("Fisica", 2);
		coloresMaterias.Add ("Ingles", 3);

		enemigosMateria.Add ("Calculo", 4);
		enemigosMateria.Add ("Fisica", 4);
		enemigosMateria.Add ("Ingles", 4);

		notas.Add ("Calculo", 2);
		notas.Add ("Fisica", 0);
		notas.Add ("Ingles", 0);

		parciales.Add ("Calculo", 0);
		parciales.Add ("Fisica", 0);
		parciales.Add ("Ingles", 0);

		ultimaPosicion = posicionInicial;
		ultimaPosicionCamara = posicionInicialCamara;
		expEsperada = 400;
		desdeMinijuego = false;

		load = false;
	}

	public void resetValues(){
		resetar = true;
		defaultValues ();
		Save ();
		Load ();
		GameObject.FindWithTag("Player").GetComponent<NavMeshAgent>().Warp(posicionInicial);
	}
		
	//==================================================================================================
	/*
	 * Método para guardar los datos del juego
	*/
	public int getCategoriaMinijuego(){
		return categoriMinijuego;
	}


	//==================================================================================================
	/*
	 * Método para cargar los datos actuales del estudiante 
	*/
	void datosActuales(){
		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			player = GameObject.FindGameObjectWithTag ("Player");
			ultimaPosicion = player.transform.position;

			camara = GameObject.FindGameObjectWithTag ("MainCamera");
			ultimaPosicionCamara = camara.transform.position;
		}
	}


	//==================================================================================================
	/*
	 * Método para guardar los datos del juego
	*/
	public void Save(){

		if(!resetar)
			datosActuales ();

		try{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (ruta);

			PlayerData data = new PlayerData ();
			data.experiencia = experiencia;
			data.energiaTotal = energiaTotal;
			data.energiaActual = energiaActual;
			data.estresTotal = estresTotal;
			data.estresActual = estresActual;
			data.semestre = semestre;
			data.cantidadMaterias = cantidadMaterias;
			data.materias = materias;
			data.notas = notas;
			data.parciales= parciales;
			data.coloresMaterias = coloresMaterias;

			data.enemigosMateria = enemigosMateria;
			data.ultimaPosicionX = ultimaPosicion.x;
			data.ultimaPosicionY = ultimaPosicion.y;
			data.ultimaPosicionZ = ultimaPosicion.z;
			data.ultimaPosicionCamaraX = ultimaPosicionCamara.x;
			data.ultimaPosicionCamaraY = ultimaPosicionCamara.y;
			data.ultimaPosicionCamaraZ = ultimaPosicionCamara.z;


			bf.Serialize (file, data);
			file.Close ();
		}catch(Exception e){
			texto.text = e.Message;
		}
		//texto.text = "SAVE " + ruta;
	}

	//==================================================================================================
	/*
	 * Método para cargar los datos del juego
	*/
	public void Load(){
		if (File.Exists (ruta)) {
			try{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (ruta, FileMode.Open);
				PlayerData data = (PlayerData)bf.Deserialize (file);
				file.Close ();

				inicializar (data);
			}catch(Exception e){
				texto.text = e.Message;
			}
		}

	}

	//==================================================================================================
	/*
	 * Método para asignar los datos leido
	 * 		parametros:	-data: objeto con los datos leidos tipo PLayerData
	*/
	public void inicializar(PlayerData  data){
		
		getData (data);

		player = GameObject.FindGameObjectWithTag ("Player");
		player.transform.position = ultimaPosicion;

		camara = GameObject.FindGameObjectWithTag ("MainCamera");
		camara.transform.position = ultimaPosicionCamara;

		load = true;
				
	}


	void getData(PlayerData  data){
		if (data.experiencia == null || data.experiencia == 0)
			experiencia = 200;
		else
			experiencia = data.experiencia;

		if (data.energiaTotal == null || data.energiaTotal == 0) {
			energiaTotal = 100;
			energiaActual = 100;
		}else {
			energiaTotal = data.energiaTotal;
			energiaActual = data.energiaActual;
		}

		if (data.estresTotal == null || data.estresTotal == 0) {
			estresTotal = 100;
			estresActual = 100;
		}else {
			estresTotal = data.estresTotal;
			estresActual = data.estresActual;
		}
		

		if (data.semestre == null)
			semestre = 1;
		else
			semestre= data.semestre;

		if (data.cantidadMaterias == null || data.cantidadMaterias < 0)
			cantidadMaterias = 0;
		else
			cantidadMaterias = data.cantidadMaterias;

		if (data.materias != null)
			materias = data.materias;

		if (data.coloresMaterias != null)
			coloresMaterias = data.coloresMaterias;

		if (data.enemigosMateria!= null)
			enemigosMateria = data.enemigosMateria;

		if (data.notas!= null)
			notas = data.notas;

		if (data.parciales!= null)
			parciales = data.parciales;


		if (materias.Length != cantidadMaterias || coloresMaterias.Count != cantidadMaterias) {
			print ("Error en lectura de datos: Cantidad de materias");
		}

		if (data.ultimaPosicionX != null && data.ultimaPosicionY != null && data.ultimaPosicionZ != null) {
			ultimaPosicion = new Vector3 (data.ultimaPosicionX, data.ultimaPosicionY, data.ultimaPosicionZ);
		}else
			ultimaPosicion = posicionInicial;

		if (data.ultimaPosicionCamaraX != null && data.ultimaPosicionCamaraY != null && data.ultimaPosicionCamaraZ != null)
			ultimaPosicionCamara = new Vector3 (data.ultimaPosicionCamaraX, data.ultimaPosicionCamaraY, data.ultimaPosicionCamaraZ);
		else
			ultimaPosicionCamara = posicionInicialCamara;

		expEsperada = 100 + 300 * semestre;
	}

	//==================================================================================================
	/*
	 * Guardar el juego al pausar o cerrar la App.
	*/
	void OnApplicationQuit(){
		Save ();
	}

	void OnApplicationPause(){
		Save ();
	}

}


/*
 * Clase Serializable utilizada para guardar los datos de juego en disco
 */

[Serializable]
public class PlayerData{
	public float experiencia;
	public float semestre;
	public float energiaTotal;
	public float energiaActual;
	public float estresTotal;
	public float estresActual;


	public int cantidadMaterias;
	public string[] materias;
	public Dictionary<string, int> coloresMaterias;
	public Dictionary<string, float> notas = new Dictionary<string, float>();
	public Dictionary<string, int> parciales = new Dictionary<string, int>();
	public Dictionary<string, int> enemigosMateria = new Dictionary<string, int>();

	public float ultimaPosicionX;
	public float ultimaPosicionY;
	public float ultimaPosicionZ;
	public float ultimaPosicionCamaraX;
	public float ultimaPosicionCamaraY;
	public float ultimaPosicionCamaraZ;
}