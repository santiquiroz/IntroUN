using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneManagger {

	//Escenas
	public const string campus = "Campus";
	public const string interiores = "Edificio de un salón";
	public const string tienda = "Tienda";
	public const string jefe = "Jefe";

	public bool cambio;


	//Campus
	public Vector3 posPlayer_Campus;
	public Vector3 porCamara_Campus;

	//Interiores
	public Vector3 posPlayer_Interiores;
	public Vector3 porCamara_Interiores;
	public Materia materia;
	public Categoria categoria;
	public bool parcial;
	public int indexEnemigo;
	public bool fromMinigame;

	//Tienda


	//Minijuegos
	public float saludEnemigo;
	public float tiempo;
	public float dificultad;
	public float poderAtaque;


	//==================================================================================================
	/*
	 * Verifica si la escena desde la que se intenta hacer el paso de Escena corresponda con el metodo
	 * 		parametros:		-scena:	Escena origen del metodo
	*/
	private bool verificarCarga(string scena){
		if (scena != SceneManager.GetActiveScene ().name) {
			Debug.Log ("Error, la escan actual no corresponde");
			return true;
		}
		return false;
	}


	//==================================================================================================
	/*
	 * Carga de escena
	 * 		parametros:		-scena:	Escena destino del metodo
	*/
	private void cargarEscena(string scene){
		try{
			cambio = true;
			SceneManager.LoadScene (scene);
		}catch(Exception e){
			cambio = false;
			Debug.Log ("Error cargando la escena " + scene);	
		}
	}

	//==================================================================================================
	/*
	 * Retorna la escenas del juego
	*/
	public string currentScene{
		get { return SceneManager.GetActiveScene ().name; }
	}

	public string campusScene{
		get{ return campus;}
	}

	public string interioresScene{
		get{ return interiores;}
	}

	public string tiendaScene{
		get{ return tienda;}
	}

	//==================================================================================================
	/*
	 * Campus -> Interiores
	 * 		parametros:		-posPlayer: Ultima posicion del jugador
	 * 						-porCamara: Ultima posicion de la camara
	 * 						-materia: 	Materia en contexto
	 * 						-par: 		True si es hora del parcial
	*/
	public void CampusToInteriores(Vector3 posPlayer, Vector3 posCamara, Materia mat, bool par){
		if (verificarCarga (campus))
			return;

		posPlayer_Campus = posPlayer;
		porCamara_Campus = posCamara;

		materia = mat;
		categoria = GameControl.control.categoriasDic [materia.categoria];
		parcial = par;
		indexEnemigo = -1;
		fromMinigame = false;

		cargarEscena(interiores);
	}

	//==================================================================================================
	/*
	 * Interiores -> Campus
	 * 		parametros:		-
	*/
	public void InterioresToCampus(){
		if (verificarCarga (interiores))
			return;

		//--

		cargarEscena(campus);
	}


	//==================================================================================================
	/*
	 * Interiores -> Minijuegos
	 * 		parametros:		-posPlayer: Ultima posicion del jugador
	 * 						-porCamara: Ultima posicion de la camara
	 * 						-indexEnem:	Indece del enemgio al que accedio
	 * 						-saludEnem: Salud del enemigo
	 * 						-time:		Tiempo del minijuego
	 * 						-dif:		Dificultad del minijuego
	 * 						-poderAtaq:	Poder de ataque del jugador
	*/
	public void InterioresToMinijuegos(Vector3 posPlayer, Vector3 posCamara, int indexEnem, float saludEnem, float time, float dif, float poderAtaq){
		if (verificarCarga (interiores))
			return;

		posPlayer_Interiores = posPlayer;
		porCamara_Interiores = posCamara;
		indexEnemigo = indexEnem;

		saludEnemigo = saludEnem;
		tiempo = time;
		dificultad = dif;
		poderAtaque = poderAtaq;


		if (parcial)
			cargarEscena (jefe);
		else {

			int l = GameControl.control.categoriasDic [materia.categoria].minijuegos.Length;

			if (l > 0) {
				int i = UnityEngine.Random.Range (0, l);
				cargarEscena (GameControl.control.categoriasDic [materia.categoria].minijuegos [i]);
			} else {
				Debug.Log ("No hay minijuegos para la categoria " + GameControl.control.categoriasDic [materia.name].name);
			}
		}

	}

	//==================================================================================================
	/*
	 * Minijuegos -> Interiores
	 * 		parametros:		-
	*/
	public void MinijuegoToInteriores(){
		fromMinigame = true;
		cargarEscena (interiores);
	}

	//==================================================================================================
	/*
	 * Minijuegos -> Campus
	 * 		parametros:		-
	*/
	public void MinijuegoToCampus(){
		fromMinigame = true;
		cargarEscena (campus);
	}

	//==================================================================================================
	/*
	 * Campus -> Tienda
	 * 		parametros:		-posPlayer: Ultima posicion del jugador
	 * 						-porCamara: Ultima posicion de la camara
	*/
	public void CampusToTienda(Vector3 posPlayer, Vector3 posCamara){
		if (verificarCarga (campus))
			return;
	
		posPlayer_Campus = posPlayer;
		porCamara_Campus = posCamara;

		cargarEscena(tienda);
	}

	//==================================================================================================
	/*
	 * Tienda -> Campus
	 * 		parametros:		-
	*/
	public void TiendaToCampus(){
		if(verificarCarga (tienda))
			return;

		cargarEscena (campus);

	}
}
