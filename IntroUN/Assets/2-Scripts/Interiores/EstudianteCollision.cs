using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EstudianteCollision : MonoBehaviour {

	private Vector3 posCamara;
	private int index;
	private float saludEnemigo;
	private float time ;
	private float dif;
	private float poderAtaque;

	void OnTriggerEnter(Collider other) {
		


		if (other.gameObject.tag == "Enemy" && gameObject.GetComponent<EstudianteEstadisticas> ().enregiaActividad (2)) {
			
			posCamara = gameObject.GetComponent<EstudianteMovimiento> ().camara.transform.position;
			index = other.GetComponent<EnemigoIndex> ().index;
			saludEnemigo = 400;
			time = 40f;
			dif = 0.5f;
			poderAtaque = gameObject.GetComponent<EstudianteEstadisticas> ().poderDeAtaque ();

			cargarEscena ();
		}else if(other.gameObject.tag == "Jefe" && gameObject.GetComponent<EstudianteEstadisticas> ().enregiaActividad (3)){
			
			posCamara = gameObject.GetComponent<EstudianteMovimiento> ().camara.transform.position;
			index = -1;
			saludEnemigo = 1000;
			time = 40f;
			dif = 0.5f;
			poderAtaque = gameObject.GetComponent<EstudianteEstadisticas> ().poderDeAtaque ();

			cargarEscena ();
		}


		if (other.gameObject.name == "Exit")
			GameControl.control.sceneManager.InterioresToCampus ();


	}


	private void cargarEscena(){
		GameControl.control.sceneManager.InterioresToMinijuegos (
			transform.position, 
			posCamara,
			index,
			saludEnemigo,
			time,
			dif,
			poderAtaque);
	}
}
