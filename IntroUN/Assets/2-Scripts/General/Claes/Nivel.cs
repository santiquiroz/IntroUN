using UnityEngine;
using System.Collections;

[System.Serializable]
public class Nivel {

	public string name;
	public float experiancia;
	//Public Image icono;

	public Nivel(string name, float exp){
		this.name = name;
		this.experiancia = exp;
	}
}
