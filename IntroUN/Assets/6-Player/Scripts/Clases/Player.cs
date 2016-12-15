using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class Player {

	public float experiencia;
	public int semestre;
	public float papa;
	public int nivel;
	public Nivel[] niveles;
	public float expEsperada;
	public int cantidadMaterias;
	public Materia[] materias;
	public int bolsaDeCreditos;

	public float energiaActual;
	public float energiaTotal;
	public float estresActual;
	public float estresTotal;

	public float dinero;


	public string buscarCategoria(string materia){
		foreach (Materia m in materias) {
			if (m.name == materia)
				return m.categoria;
		}
		Debug.Log ("Error buscarCategoria");
		return null;
	}

	public Materia buscarMateria(string materia){
		foreach (Materia m in materias) {
			if (m.name == materia)
				return m;
		}
		Debug.Log ("Error buscandoMateria");
		return null;
	}

	public int numeroParcial(string materia){
		Materia m = buscarMateria (materia);
		return m.parciales.Length - m.cantidadParciales + 1;
	}
}
