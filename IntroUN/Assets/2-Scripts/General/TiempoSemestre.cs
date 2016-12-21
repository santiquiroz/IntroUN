using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TiempoSemestre : MonoBehaviour {


	private float equivalenciaDia;

	[SerializeField]
	private float tiempo;
	private float tiempoAnterior;
	private bool pause;

	public int dias;
	public int semanas;
	public int dias_trancurridos;

	[SerializeField]
	public List<Parcial> parcialesSemestre;
	public Text textoSemanas;

	public AlertaParcial alertPrefab;
	public List<AlertaParcial> alertas;
	public GameObject[] diasObj;

	private Parcial parcialEjecutar;

	// Use this for initialization
	void Start () {

		pause = false;
		parcialesSemestre = GameControl.control.LoadTiempoSemestre ();
		dias = GameControl.control.dataConsistence.dia;
		semanas = GameControl.control.dataConsistence.semana;
		dias_trancurridos = GameControl.control.dataConsistence.dias_trancurridos;

		textoSemanas.text = semanas.ToString () + "s. : " + dias.ToString () + "d.";

		tiempoAnterior = tiempo;
		equivalenciaDia = tiempo / (dias * semanas);

		ajustarTiempo ();
	}

	
	//==================================================================================================
	/*
	 * Avanza Tiempo de semestre
	*/
	void Update () {
		if (!pause) {
			if (tiempo > 0) {
				tiempo -= Time.deltaTime;	

				if (tiempo <= 0) {
					pause = true;
				} else if (tiempoAnterior - tiempo >= equivalenciaDia) {
					tiempoAnterior = tiempo;
					dias--;
					dias_trancurridos++;

					ajustarTiempo ();

					if (dias <= 0) {
						semanas--;
						GameControl.control.dataConsistence.semana = semanas;
						dias = 6;
					}

					GameControl.control.dataConsistence.dia = dias;
					GameControl.control.dataConsistence.dias_trancurridos = dias_trancurridos;

					textoSemanas.text = semanas.ToString () + "s. : " + dias.ToString () + "d.";
				}
			}
		}
	}

	//==================================================================================================
	/*
	 * Revisa los parciales proximos
	*/
	void ajustarTiempo(){
		int t = 0;
		foreach (AlertaParcial obj in alertas) {
			t = obj.parcial.tiempo_dias - dias_trancurridos;

			if (t >= 0){
				Vector3 pos = diasObj [t].transform.position;
				pos.y = obj.transform.position.y;
				obj.transform.position = pos;
			}

			if (t <= 0) {
				tiempoDeParcial (obj);
			}

		}


		for (int i = parcialesSemestre.Count - 1; i >= 0; i--){
			t = parcialesSemestre[i].tiempo_dias - dias_trancurridos;
			if (t <= 0 && parcialesSemestre [i].estado == EstadosParciales.Realizado) {
				parcialesSemestre.RemoveAt (i);
			}else if (t <= 6) {
				AlertaParcial newAlerta = Instantiate (alertPrefab) as AlertaParcial;
				newAlerta.transform.SetParent (transform, false);

				Vector3 pos = diasObj [t].transform.position;
				pos.y = newAlerta.transform.position.y;
				newAlerta.transform.position = pos;


				string cat = GameControl.control.playerData.buscarCategoria(parcialesSemestre[i].materiaName);
				newAlerta.setAlert (parcialesSemestre[i], GameControl.control.categoriasDic [cat]);

				alertas.Add (newAlerta);
				parcialesSemestre.RemoveAt (i);

				if(t == 0)
					tiempoDeParcial (newAlerta);
				
			} else {
				break;
			}
		}

	}

	//==================================================================================================
	/*
	 * Ajuste Meta-Información y muesta mensaje
	*/
	void tiempoDeParcial(AlertaParcial obj){
		pause = true;
		obj.parcial.estado = EstadosParciales.Pendiente;
		parcialEjecutar = obj.parcial;
		GameObject.FindGameObjectWithTag ("Canvas").GetComponent<CanvasMannager>().drawAvisoParcial (obj.parcial.materiaName);
	}


	//==================================================================================================
	/*
	 * Llama Escena de parcial
	*/
	public void callParcial(){
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		GameObject camara = GameObject.FindGameObjectWithTag ("MainCamera");

		GameControl.control.sceneManager.CampusToInteriores (
			player.transform.position,
			camara.transform.position,
			GameControl.control.playerData.buscarMateria (parcialEjecutar.materiaName),
			true);
			
	}

	public void automaticEstu(){
		Vector3 punto = buscarEdificio ();
		if (punto != Vector3.zero) {
			GameObject.FindGameObjectWithTag ("Canvas").GetComponent<CanvasMannager> ().drawAvisoParcial ();
			GameObject.FindWithTag ("Player").GetComponent<EstudianteParcialAuto> ().Mover (punto);
		} else
			Debug.Log ("Error en AutomaticEstu");
	}

	//==================================================================================================
	/*
	 * Llama Escena de parcial
	*/
	Vector3 buscarEdificio(){
		foreach (EdificioControl edificio in GameControl.control.semestresData.edificios) {
			if (GameControl.control.playerData.materias [edificio.materia].name == parcialEjecutar.materiaName)
				return edificio.gameObject.transform.GetChild (0).transform.position;
		}
		return Vector3.zero;
	}

}
