using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/*
 * juaalvarezme
 * Script para el movimiento del Estudiante en el Campus
 * 	con NavMesh
 */

public class EstudianteMovimiento : MonoBehaviour {

	NavMeshAgent agent;
	Animator anim;
	public LayerMask terrenoLayer;
	public LayerMask edificioLayer;
	public LayerMask tiendaLayer;

	public GameObject camara;
	public GameObject pointer;

	public bool run;
	private bool traslacion;
	private bool onTouch;
	private Vector3 destino;
	private bool cambioEscena;

	private GameObject edificio;

	//==================================================================================================

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator> ();

		run = false;
		traslacion = false;
		onTouch = false;

		cambioEscena = false;
		pointer.SetActive(false);

		if (GameControl.control.desdeMinijuego && !GameControl.control.parcial) {
			GameControl.control.desdeMinijuego = false;
			transform.position = GameControl.control.ultimaPosEdificio;
			camara.transform.position = GameControl.control.ultimaPosEdificioCam;
		}

	}

	//==================================================================================================

	void Update () {

		//print (GameControl.control.ultimaPosicion);
		//Detección del Touch
		if (Input.touchCount > 0) {

			if (Input.GetMouseButtonDown (0) && EventSystem.current.IsPointerOverGameObject (0)) {
				onTouch = true;
			}

			if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved)
				traslacion = true;
			else if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Ended && !traslacion && !onTouch) {

				RaycastHit hit;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 1200, terrenoLayer | edificioLayer | tiendaLayer)) {

					pointer.SetActive (false);

					if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Terreno")) {
						Mover (hit.point, true);
						if (cambioEscena) {
							cambioEscena = false;
							edificio.GetComponent<EdificioControl>().activarPointer (false);
						}
					} else {
						edificio = hit.transform.gameObject;
						edificio.GetComponent<EdificioControl>().asignarCategoria();
						if (cambioEscena)
							cambiarScena ();	//LLamar a futuro script de cambio de escena
						cambioEscena = true;
						Mover (hit.transform.GetChild (0).transform.position, false);

					}
				}
			} 
		} else if (run && enDestino ()) {
			anim.SetTrigger ("Run");
			run = false;
			traslacion = false;
			pointer.SetActive (false);
		} else {
			traslacion = false;
			onTouch = false;
		}

	}

	//==================================================================================================
	/*
	 * Mover a un punto con NavMesh
	 * 		parametros:
	 * 					-punto: Vector3 que indica el objetivo
	*/
	public void Mover(Vector3 punto, bool marcarPointer){
		//agent.Warp(transform.position);
		agent.destination = punto;
		destino = punto;
		if (marcarPointer) {
			marcarLugar (destino);
		}
		if (!run) {
			anim.SetTrigger ("Run");
			run = true;
		}

	}

	//==================================================================================================
	/*
	 * Activar pointer en un posicion especifica
	 * 		parametros:
	 * 					-nuevaPosicion: Vector3 que indica el objetivo
	*/
	private void marcarLugar(Vector3 nuevaPosicion)
	{
		nuevaPosicion.y += 5;
		pointer.transform.position = nuevaPosicion; 
		pointer.SetActive(true);
	}


	//==================================================================================================
	/*
	 * Consulta si el NavMeshAgent llego a su destino
	 * 		retorna: True si el NaveMesh llego a su destino - False de lo contrario.
	*/
	bool enDestino(){
		float dist=agent.remainingDistance;
		if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 10)
			return true;
		return false;
	}





	void cambiarScena(){
		
		if (edificio.gameObject.layer == LayerMask.NameToLayer ("Tienda")) {
            SceneManager.LoadScene(2);
		} else {
			GameControl.control.indexUtimoEnemigo = -1;
			GameControl.control.Save ();
			SceneManager.LoadScene (1);
		}
	}

}