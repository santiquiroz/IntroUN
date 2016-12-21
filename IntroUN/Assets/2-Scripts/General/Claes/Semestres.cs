using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Semestres {
	public MateriasArray[] semestres;
	public List<EdificioControl> edificios;
}

[System.Serializable]
public class MateriasArray{
	public Materia[] materias;
}
	