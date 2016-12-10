using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TiempoSemestre : MonoBehaviour {


	private float equivalenciaDia;

	[SerializeField]
	private float tiempo;
	private float tiempoAnterior;
	private bool iniciar;

	public int dias;
	public int semanas;
	public int dias_trancurridos;

	[SerializeField]
	public List<Parcial> parcialesSemestre;
	public Text textoSemanas;

	public AlertaParcial alertPrefab;
	public List<AlertaParcial> alertas;
	public GameObject[] diasObj;

	// Use this for initialization
	void Start () {

		iniciar = true;
		parcialesSemestre = GameControl.control.LoadTiempoSemestre ();
		dias = GameControl.control.dataConsistence.dia;
		semanas = GameControl.control.dataConsistence.semana;
		dias_trancurridos = GameControl.control.dataConsistence.dias_trancurridos;

		textoSemanas.text = semanas.ToString () + "s. : " + dias.ToString () + "d.";

		tiempoAnterior = tiempo;
		equivalenciaDia = tiempo / (dias * semanas);

		ajustarTiempo ();
	}

	
	// Update is called once per frame
	void Update () {
		if (iniciar) {
			if (tiempo > 0) {
				tiempo -= Time.deltaTime;	

				if (tiempo <= 0) {
					iniciar = false;
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


	void ajustarTiempo(){
		int t = 0;
		foreach (AlertaParcial obj in alertas) {
			t = obj.parcial.tiempo_dias - dias_trancurridos;

			if (t < 0) {
				print ("Parcial!!!");
				//GameControl.control.playerData.buscarMateria ();
				//Ir a Parcial
			} else {
				Vector3 pos = diasObj [t].transform.position;
				pos.y = obj.transform.position.y;
				obj.transform.position = pos;
			}
		}


		for (int i = parcialesSemestre.Count - 1; i >= 0; i--){
			t = parcialesSemestre[i].tiempo_dias - dias_trancurridos;
			if (t <= 0) {
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
			} else {
				break;
			}
		}

	}


}
