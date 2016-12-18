using UnityEngine;
using System.Collections;

public enum EstadosParciales{
	EnEspera,
	Pendiente,
	Realizado
}

[System.Serializable]
public class Parcial {

	public float porncentaje;
	public float nota;
	public int tiempo_dias;
	public string materiaName;
	public EstadosParciales estado;

	public Parcial(float porcentaje, int tiempo_dias, string materia){
		this.porncentaje = porcentaje;
		this.tiempo_dias = tiempo_dias;
		nota = 0;
		materiaName = materia;
		estado = EstadosParciales.EnEspera;
	}
}

