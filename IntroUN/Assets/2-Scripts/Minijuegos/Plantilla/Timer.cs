using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * juaalvarezme
 * 
 * Script para controlar la Timer de los Minijuegos
 */

public class Timer : MonoBehaviour {

	[SerializeField]
	private Image barraTimer;
	public Text timerText;
	private float tiempo;
	private bool iniciar;

	[SerializeField]
	private PersonajesScripts controladorPersonajes;

	//==================================================================================================
	/*
	 *	Tiempo del contador
	*/
	void Start () {
		iniciar = false;
		try{
			tiempo = GameControl.control.tiempo;
		}catch(System.Exception ex){
			tiempo = 45f;
		}
		timerText.text = tiempo.ToString ();
	}

	//==================================================================================================
	/*
	 *	Actualización del contador
	*/
	void Update () {
		if (iniciar) {
			if (!controladorPersonajes.getGano () && tiempo > 0) {
				tiempo -= Time.deltaTime;	
				timerText.text = (tiempo + 1).ToString ();
				//barraTimer.fillAmount = Map (tiempo, Mathf.Floor(tiempo), Mathf.Ceil(tiempo), 0f, 1f);
				barraTimer.fillAmount = tiempo - Mathf.Floor (tiempo);
				if (tiempo <= 0) {
					timerText.text = "0.";	
					barraTimer.fillAmount = 0;
					controladorPersonajes.Perder ();
					iniciar = false;
				}
			}
		}
	}

	//==================================================================================================
	/*
	 * Map para normalizar un valor
	 * 		parametros:	-value: Valor a normalizar
	 * 					-inMin: Minimo valor que puede tomar el parametro value
	 * 					-inMax: Maximo valor que puede tomar el parametro value
	 * 					-outMin: Minimo valor que puede tomar la salida
	 * 					-outMax: Maximo valor que puede tomar la salida
	 * 
	 * 		retorno:	-Valor normalizado
	 */
	private float Map(float value, float inMin, float inMax, float outMin, float outMax)
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	//==================================================================================================
	/*
	 *	Getters y Setters
	*/
	public void setIniciar(bool newIniciar){
		iniciar = true;
	}

	public void setIniciar(float newTiempo){
		tiempo = newTiempo;
		timerText.text = tiempo.ToString ();
	}

	public float getTiempo(){
		return tiempo;
	}
}
