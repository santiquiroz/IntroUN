using UnityEngine;
using System.Collections;

[System.Serializable]
public class Parcial {

	public float porncentaje;
	public float nota;
	public int tiempo_dias;

	public Parcial(float porcentaje, int tiempo_dias){
		this.porncentaje = porcentaje;
		this.tiempo_dias = tiempo_dias;
		nota = 0;
	}
}
