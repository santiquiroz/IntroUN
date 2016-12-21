using UnityEngine;
using System.Collections;

public class EstudianteParcialAuto : MonoBehaviour {

	NavMeshAgent agent;
	Animator anim;

	private Vector3 destino;
	private bool run;
	private bool automatic;
	private bool inCampus;

	public TiempoSemestre tiempoSemestre;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator> ();

		automatic = false;
		run = false;
		inCampus = true;

		if (GameControl.control.sceneManager.currentScene == GameControl.control.sceneManager.interioresScene) {
			inCampus = false;
			Vector3 punto = GameObject.FindWithTag ("Jefe").transform.position;	//Objeto JefePos
			Mover (punto);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (automatic && enDestino ()) {
			anim.SetTrigger ("Run");
			run = false;
			automatic = false;
			if(inCampus)
				cambiarScena ();
		}
	}

	//==================================================================================================
	/*
	 * Mover a un punto con NavMesh
	 * 		parametros:
	 * 					-punto: Vector3 que indica el objetivo
	*/
	public void Mover(Vector3 punto){
		//agent.Warp(transform.position);

		gameObject.GetComponent<EstudianteMovimiento> ().enabled = false;


		agent.destination = punto;
		destino = punto;

		if (!run) {
			anim.SetTrigger ("Run");
			run = true;
		}

		Invoke ("setAutomatic", 0.5f);
	}

	private void setAutomatic(){
		automatic = true;
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
		tiempoSemestre.callParcial ();
	}
}
