using UnityEngine;
using System.Collections;

[System.Serializable]
public class DataConsistence {

	public int ultimaEscena;
	public string ultimaEscenaName;

	public float ultiPosPlayer_x;
	public float ultiPosPlayer_y;
	public float ultiPosPlayer_z;

	public float ultiPosCamara_x;
	public float ultiPosCamara_y;
	public float ultiPosCamara_z;

	public int dia;
	public int semana;

	public void FillPosPlayer(Vector3 v3){
		ultiPosPlayer_x = v3.x;
		ultiPosPlayer_y = v3.y;
		ultiPosPlayer_z = v3.z;
	}

	public Vector3 V3PosPlayer{ 
		get { 
			return new Vector3(ultiPosPlayer_x, ultiPosPlayer_y, ultiPosPlayer_z); 
		} 
	}


	public void FillPosCamara(Vector3 v3){
		ultiPosCamara_x = v3.x;
		ultiPosCamara_y = v3.y;
		ultiPosCamara_z = v3.z;
	}

	public Vector3 V3PosCamara{ 
		get { 
			return new Vector3(ultiPosCamara_x, ultiPosCamara_y, ultiPosCamara_z); 
		} 
	}
}
