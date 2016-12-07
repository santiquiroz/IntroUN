using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TiempoSemestre : MonoBehaviour {


	private float equivalenciaDia;
	[SerializeField]
	private float tiempo;
	private float tiempoAnterior;
	private bool iniciar;
	public int dias;
	public int semanas;

	public Text textoSemanas;

	public GameObject alert1;
	public GameObject[] diasObj;
	private int diaAlerta;
	private bool activo;

	// Use this for initialization
	void Start () {

		iniciar = true;
		//Actualizar con GameControl
		//dias = GameControl.control.dias;
		//semanas = GameControl.control.semanas;
		textoSemanas.text = semanas.ToString () + "s. : " + dias.ToString () + "d.";

		tiempoAnterior = tiempo;
		equivalenciaDia = tiempo / (dias * semanas);

		diaAlerta = 6;

		if (diaAlerta > 6)
			alert1.SetActive (false);
		else {
			Vector3 pos = diasObj [diaAlerta].transform.position;
			pos.y = alert1.transform.position.y;
			alert1.transform.position = pos;
			activo = false;
		}
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
					//ajustarAlerta ();
					if (dias <= 0) {
						semanas--;
						GameControl.control.dataConsistence.semana = semanas;
						dias = 6;
					}

					GameControl.control.dataConsistence.dia = dias;

					textoSemanas.text = semanas.ToString () + "s. : " + dias.ToString () + "d.";
				}
			}
		}
	}

	void ajustarAlerta(){
		/*
		if (diaAlerta >= 0 && GameControl.control.parciales["Calculo"] < 4) {
			diaAlerta--;

			if (diaAlerta == 0) {
				//GameControl.control.parcial = true;
				alert1.GetComponent<Animator> ().SetTrigger ("Alerta");
				alert1.GetComponent<Button> ().onClick.Invoke ();
			} else if (diaAlerta < 0) {
				alert1.SetActive (false);

				if(GameControl.control.parciales["Calculo"] <4)
					diaAlerta = GameControl.control.diaAlerta;
				
				return;
			} else if (diaAlerta > 6) {
				alert1.SetActive (false);
				return;
			}


			alert1.SetActive (true);
			//GameControl.control.diaAlerta = diaAlerta;
			Vector3 pos = diasObj [diaAlerta].transform.position;
			pos.y = alert1.transform.position.y;
			alert1.transform.position = pos;
		}
		*/
	}

	public void activar(GameObject apoyo){
		activo = !activo;
		apoyo.SetActive (activo);
	}
}
