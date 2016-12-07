﻿using UnityEngine;
using System.Collections;

public class EdificioControl : MonoBehaviour {

	public int materia;
	GameObject pointer;

	void Start () {
		pointer = transform.GetChild (1).gameObject;
		pointer.GetComponent<Animator> ().SetBool ("PointerEdificio", true);
		pointer.gameObject.SetActive (false);
	}
	

	void Update () {
		
	}

	public void asignarCategoria(){
		activarPointer (true);
	}

	public void asignarCategoria(int casoEspecial){
		activarPointer (true);
	}

	public void activarPointer(bool activar){
		pointer.gameObject.SetActive (activar);
		pointer.GetComponent<Animator> ().SetBool ("PointerEdificio", true);
	}
}
