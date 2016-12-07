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

	public float energiaActual;
	public float energiaTotal;
	public float estresActual;
	public float estresTotal;

	public float dinero;


}
