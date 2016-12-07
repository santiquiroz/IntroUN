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


	// Use this for initialization
	void Start () {
		cantidadPuntos = puntos.Count;
		string materia = GameControl.control.sceneManager.materia.name;
		totalenemigos = GameControl.control.sceneManager.materia.cantidadEnemigos;

		if (GameControl.control.sceneManager.parcial) {
			Instantiate (jefe, jefePos.transform.position, Quaternion.identity);
		} else {

			enemgoToInstantiate = GameControl.control.sceneManager.categoria.basciEnemy;
			if (GameControl.control.sceneManager.indexEnemigo > -1)
				puntos.RemoveAt (GameControl.control.sceneManager.indexEnemigo);

			crearEnemigos ();
		}
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
