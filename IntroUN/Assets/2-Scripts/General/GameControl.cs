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

	public bool reset;

	public SceneManagger sceneManager;
	public Player playerData;
	public DataConsistence dataConsistence;

	public Categoria[] categorias;
	public Dictionary<string, Categoria> categoriasDic = new Dictionary<string, Categoria>();

	//Rutas
	public string path;
	private const string archivoInstalacion = "IntroUNGame.dat";
	private const string rutaInfoBase = "InfoBase.dat";
	private const string rutaPlayer = "Player.dat";
	private const string rutaDataConsistence = "DataConsistence.dat";
	private const string tiempoSemestre = "Semestre.dat";


	private bool resetar;
	private bool inicio;
	private bool instalacion;

	//==================================================================================================
	/*
	 * Comprobación de que solo exista un objeto GameControl
	*/
	void Awake () {

		inicio = true;

		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this)
			Destroy (gameObject);
		else
			control = this;

		path = Application.persistentDataPath + Path.DirectorySeparatorChar;
		sceneManager = new SceneManagger();
		sceneManager.cambio = false;


		for(int i = 0; i < categorias.Length; i++){
			categoriasDic.Add(categorias[i].name, categorias[i]);
		}

		instalacion = false;
		LoadAllData ();
	}

	void Start(){
		initData ();
	}

	//==================================================================================================
	/*
	 * Lectura de los archivos necesarios
	*/
	private void initData(){
		
		//Cargar ultima escena

		if (dataConsistence.ultimaEscena == 0) {
			if (instalacion) {
				GameObject player = GameObject.FindGameObjectWithTag ("Player");
				player.transform.position = dataConsistence.V3PosPlayer;

				GameObject camara = GameObject.FindGameObjectWithTag ("MainCamera");
				camara.transform.position = dataConsistence.V3PosCamara;
			}
				
		}

		inicio = false;
	}



	//==================================================================================================
	/*
	 * Guardar el juego al pausar o cerrar la App.
	*/
	void OnApplicationQuit(){
		if(!inicio)
			SaveAllData ();
	}

	void OnApplicationPause(){
		if(!inicio)
			SaveAllData ();
	}


	public void SaveAllData(){
		SavePlayerData (path + rutaPlayer);

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		GameObject camara = GameObject.FindGameObjectWithTag ("MainCamera");

		dataConsistence.ultimaEscena = SceneManager.GetActiveScene ().buildIndex;
		dataConsistence.ultimaEscenaName = SceneManager.GetActiveScene ().name;
		dataConsistence.FillPosPlayer (player.transform.position);
		dataConsistence.FillPosCamara (camara.transform.position);

		SaveDataConsistence(path + rutaDataConsistence);
	}


	//==================================================================================================
	/*
	 * Carga el juego
	*/
	public void LoadAllData(){
		
		if (!File.Exists (path + archivoInstalacion) || reset) {
			DefaultInfo dI = new DefaultInfo ();
			dI.PrimerUso ();
			dI = null;
			Debug.Log ("Installation");
		} else {
			LoadPlayerData (path + rutaPlayer);
			LoadDataConsistence (path + rutaDataConsistence);
			instalacion = true;
		}
	}

	//==================================================================================================
	/*
	 * Guarda la información actual del jugador
	*/
	public void SavePlayerData(string ruta){

		try{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (ruta);	
			bf.Serialize (file, playerData);
			file.Close ();
		}catch(Exception e){
			Debug.Log(e.Message);
		}
	}

	//==================================================================================================
	/*
	 * Carga la información actual del jugador
	*/
	public void LoadPlayerData(string ruta){
		if (File.Exists (ruta)) {
			try {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (ruta, FileMode.Open);
				playerData = (Player)bf.Deserialize (file);
				file.Close ();
				Debug.Log ("Load Player Data");
			} catch (Exception e) {
				Debug.Log (e.Message);
			}
		} else {
			Debug.Log ("ruta no existe");
		}

	}


	//==================================================================================================
	/*
	 * Guarda la información para la Consistencia entre Escenas y Sesiones
	*/
	public void SaveDataConsistence(string ruta){

		try{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (ruta);	
			bf.Serialize (file, dataConsistence);
			file.Close ();
		}catch(Exception e){
			Debug.Log(e.Message);
		}
	}

	//==================================================================================================
	/*
	 * Carga la información para la Consistencia entre Escenas y Sesiones
	*/
	public void LoadDataConsistence(string ruta){
		if (File.Exists (ruta)) {
			try{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (ruta, FileMode.Open);
				dataConsistence = (DataConsistence) bf.Deserialize (file);
				file.Close ();
				Debug.Log ("Load Data Consistent");
			}catch(Exception e){
				Debug.Log(e.Message);
			}
		}

	}

	//==================================================================================================
	/*
	 * 
	*/
	public List<Parcial> LoadTiempoSemestre(){
		string ruta = path + tiempoSemestre;
		if (File.Exists (path + instalacion) && File.Exists (ruta)) {
			try {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (ruta, FileMode.Open);
				List<Parcial> parcialesSemestre = (List<Parcial>)bf.Deserialize (file);
				file.Close ();
				Debug.Log ("Load Data Tiempo semestre");
				return parcialesSemestre;
			} catch (Exception e) {
				Debug.Log (e.Message);
			}
		}
		return TiempoSemestreDefault ();

	}

	//==================================================================================================
	/*
	 * Guarda la información para la Consistencia entre Escenas y Sesiones
	*/
	public void SaveTiempoSemestre(string ruta, List<Parcial> paricialesSemestre){

		try{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (ruta);	
			bf.Serialize (file, paricialesSemestre);
			file.Close ();
		}catch(Exception e){
			Debug.Log(e.Message);
		}
	}

	//==================================================================================================
	/*
	 * 
	*/
	public List<Parcial> TiempoSemestreDefault(){
		List<Parcial> parcialesSemestre = new List<Parcial>();
		foreach (Materia m in playerData.materias) {
			foreach (Parcial p in m.parciales) {
				parcialesSemestre.Add (p);
			}
		}
		parcialesSemestre.Sort(new Comparison<Parcial>((x, y) => y.tiempo_dias.CompareTo(x.tiempo_dias)));

		SaveTiempoSemestre (path + tiempoSemestre, parcialesSemestre);
		return parcialesSemestre;
	}

}