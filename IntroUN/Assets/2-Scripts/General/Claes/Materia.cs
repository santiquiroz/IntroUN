using UnityEngine;
using System.Collections;

[System.Serializable]
public class Materia {

	public string name;
	public string categoria;
	public float creditos;
	public float nota;
	public int cantidadParciales;
	public int cantidadEnemigos;
	public Parcial[] parciales;
	public float porcentaje;

	public Materia(string name, string categoria, float creditos, int cantPar){
		this.name = name;
		this.categoria = categoria;
		this.creditos = creditos;
		this.cantidadParciales = cantPar;
		this.parciales = new Parcial [cantidadParciales];
		cantidadEnemigos = 4;
		nota = 0;
		porcentaje = 0;
	}
}
