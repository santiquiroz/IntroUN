using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EstudianteCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
		if (GameControl.control.energiaActual > 0) {

			if (other.gameObject.tag == "Enemy") {

				//--------------------------------------------------------


				if (!gameObject.GetComponent<EstudianteEstadisticas> ().enregiaActividad (2))
					return;

				GameControl.control.ultimaPosEdificio = transform.position;
				GameControl.control.ultimaPosEdificioCam = gameObject.GetComponent<EstudianteMovimiento>().camara.transform.position;

				GameControl.control.indexUtimoEnemigo = other.GetComponent<EnemigoIndex> ().index;
				GameControl.control.saludEnemigo = 400;
				GameControl.control.tiempo = 40f;

				if (GameControl.control.categoriMinijuego == 0)
					SceneManager.LoadScene (2);
				else if (GameControl.control.categoriMinijuego == 1)
					SceneManager.LoadScene (3);
				else if (GameControl.control.categoriMinijuego == 2)
					SceneManager.LoadScene (4);

			}else if(other.gameObject.tag == "Jefe" && !gameObject.GetComponent<EstudianteEstadisticas> ().enregiaActividad (3)){
				GameControl.control.saludEnemigo = 1000;
				GameControl.control.tiempo = 90f;

				SceneManager.LoadScene (5);

			}
		}

		if (other.gameObject.name == "Exit")
			SceneManager.LoadScene (0);

		//--------------------------------------------------------
	}
}
