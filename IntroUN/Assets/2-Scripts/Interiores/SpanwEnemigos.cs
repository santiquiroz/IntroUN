using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpanwEnemigos : MonoBehaviour {

	public List<GameObject> puntos;
	public GameObject[] enemigos;
	private GameObject enemgoToInstantiate;

	public GameObject jefe;
	public GameObject jefePos;

	private int cantidadPuntos;
	private int totalenemigos;

	private int categoria;

	// Use this for initialization
	void Start () {
		cantidadPuntos = puntos.Count;
		categoria = GameControl.control.categoriMinijuego;
		string materia = GameControl.control.materias [categoria];
		totalenemigos = GameControl.control.enemigosMateria[materia];;

		if (GameControl.control.parcial) {
			Instantiate (jefe, jefePos.transform.position, Quaternion.identity);
		} else {

			if (categoria < enemigos.Length)
				enemgoToInstantiate = enemigos [categoria];
			else
				enemgoToInstantiate = enemigos [0];


			if (GameControl.control.indexUtimoEnemigo > -1)
				puntos.RemoveAt (GameControl.control.indexUtimoEnemigo);

			crearEnemigos ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void crearEnemigos(){
		for (int i = 0; i < totalenemigos; i++) {
			int pos = Random.Range(0, (puntos.Count- 1));
			GameObject temp = (GameObject) Instantiate (enemgoToInstantiate, puntos[pos].transform.position, Quaternion.identity);
			puntos.RemoveAt (pos);
			temp.GetComponent<EnemigoIndex> ().index = pos;
		}
	}
}
