using UnityEngine;
using System.Collections;

[System.Serializable]
public class DataConsistence {

	//SceneManagger
	public int ultimaEscena;
	public string ultimaEscenaName;
	public bool fromMinigame;

	//Player
	public float[] ultiPosPlayerCampus;
	public float[] ultiPosPlayerInteriores;

	//Camara
	public float[] ultiPosCamaraCampus;
	public float[] ultiPosCamaraInteriores;

	//TiempoSemestre
	public int dia;
	public int semana;
	public int dias_trancurridos;

	//Interiores
	public Materia materia;
	public bool parcial;

	public DataConsistence(){
		ultiPosPlayerCampus = new float[3];
		ultiPosCamaraCampus = new float[3];
		ultiPosPlayerInteriores = new float[3];
		ultiPosCamaraInteriores = new float[3];
	}

	//==================================================================================================
	/*
	 * Get & Set Posision Jugador en Cmapus
	*/
	public void FillPosPlayerCampus(Vector3 v3){
		ultiPosPlayerCampus = new float[3];

		ultiPosPlayerCampus[0] = v3.x;
		ultiPosPlayerCampus[1] = v3.y;
		ultiPosPlayerCampus[2] = v3.z;
	}

	public Vector3 V3PosPlayerCampus{ 
		get { 
			return new Vector3(ultiPosPlayerCampus[0], ultiPosPlayerCampus[1], ultiPosPlayerCampus[2]); 
		} 
	}


	//==================================================================================================
	/*
	 * Get & Set Posision Camara en Cmapus
	*/
	public void FillPosCamaraCampus(Vector3 v3){
		ultiPosCamaraCampus = new float[3];

		ultiPosCamaraCampus[0] = v3.x;
		ultiPosCamaraCampus[1] = v3.y;
		ultiPosCamaraCampus[2] = v3.z;
	}

	public Vector3 V3PosCamaraCampus{ 
		get { 
			return new Vector3(ultiPosCamaraCampus[0], ultiPosCamaraCampus[1], ultiPosCamaraCampus[2]); 
		} 
	}


	//==================================================================================================
	/*
	 * Get & Set Posision Jugador en Interiores
	*/
	public void FillPosPlayerInteriores(Vector3 v3){
		ultiPosPlayerInteriores = new float[3];

		ultiPosPlayerInteriores[0] = v3.x;
		ultiPosPlayerInteriores[1] = v3.y;
		ultiPosPlayerInteriores[2] = v3.z;
	}

	public Vector3 V3PosPlayerInteriores{ 
		get { 
			return new Vector3(ultiPosPlayerInteriores[0], ultiPosPlayerInteriores[1], ultiPosPlayerInteriores[2]); 
		} 
	}


	//==================================================================================================
	/*
	 * Get & Set Posision Camara en Cmapus
	*/
	public void FillPosCamaraInteriores(Vector3 v3){
		ultiPosCamaraInteriores = new float[3];

		ultiPosCamaraInteriores[0] = v3.x;
		ultiPosCamaraInteriores[1] = v3.y;
		ultiPosCamaraInteriores[2] = v3.z;
	}

	public Vector3 V3PosCamaraInteriores{ 
		get { 
			return new Vector3(ultiPosCamaraInteriores[0], ultiPosCamaraInteriores[1], ultiPosCamaraInteriores[2]); 
		} 
	}
}
