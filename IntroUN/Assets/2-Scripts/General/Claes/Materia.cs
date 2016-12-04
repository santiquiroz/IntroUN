using UnityEngine;
using System.Collections;

[System.Serializable]
public class Materia {

	public string name;
	public Categoria categoria;
	public float creditos;
	public float nota;
	public int cantidadParciales;
	public Parcial[] parciales;

}
