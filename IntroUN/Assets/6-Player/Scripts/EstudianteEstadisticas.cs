using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EstudianteEstadisticas : MonoBehaviour {

	public float beta = 0.2f;
	public float poderAtaque;

	//Variables de control
	private float time = 0f;
	private float timeDelta;

	//Energias - Estres
	private float energiaPorRespiro;
	private float energiaPorCorrer;
	private float energiaPorBatalla;
	private float energiaPorJefe;
	private float estresPorBatalla;
	private float estresPorJefe;

	void Start () {

		timeDelta = time;
		energiaPorRespiro = 0.1f;
		energiaPorBatalla = 15f;
		energiaPorJefe = 28f;
		estresPorBatalla = 22f;
		estresPorJefe = 28f;
	}
	

	void Update () {
		time += Time.deltaTime;
		if (time - timeDelta >= 1) {
			setEnergia (energiaPorRespiro);	
			timeDelta = time;
		}
	}




	//==================================================================================================
	/*
	 * Metodo para reducir la energia
	 * 		parámetros: -dif: Cantida que disminuye la energia
	 */
	public bool enregiaActividad(int actividad){
		float energia = GameControl.control.playerData.energiaActual;
		float estres = GameControl.control.playerData.estresActual;

		if (actividad == 0)
			setEnergia (energiaPorRespiro);
		else if (actividad == 1)
			setEnergia (energiaPorCorrer);
		else if (actividad == 2 && energia >= energiaPorBatalla && estres >= estresPorBatalla) {
			setEnergia (energiaPorBatalla);
			setEstres (estresPorBatalla);
		} else if (actividad == 3 && energia >= energiaPorJefe && estres >= estresPorJefe) {
			setEnergia (energiaPorJefe);
			setEstres (estresPorJefe);
		} else
			return false;

		return true;
	}

	//==================================================================================================
	/*
	 * Metodo para reducir la energia
	 * 		parámetros: -dif: Cantida que disminuye la energia
	 */
	public void setEnergia(float dif){
		float energia = GameControl.control.playerData.energiaActual - dif;
		float energiaT = GameControl.control.playerData.energiaTotal;
	
		if (energia < 0) {
			energia = 0;
		}
		
		GameControl.control.playerData.energiaActual = energia;
	}

	//==================================================================================================
	/*
	 * Metodo para reiniciar la energia
	 */
	public void restaurarEnergia(float dif){
		float energia = GameControl.control.playerData.energiaActual + dif;
		float energiaT = GameControl.control.playerData.energiaTotal;

		if (energia > energiaT) {
			energia = energiaT;
		}

		GameControl.control.playerData.energiaActual = energia;
	}

	//==================================================================================================
	/*
	 * Metodo para reducir el estres
	 * 		parámetros: -dif: Cantida que disminuye la energia
	 */
	public void setEstres(float dif){
		float estres = GameControl.control.playerData.estresActual - dif;
		float estresT = GameControl.control.playerData.estresTotal;

		if (estres< 0)
			estres = 0;

		GameControl.control.playerData.estresActual = estres;
	}

	//==================================================================================================
	/*
	 * Metodo para reiniciar la energia
	 */
	public void restaurarEestres(){
		GameControl.control.playerData.estresActual = GameControl.control.playerData.estresTotal;
	}


	//==================================================================================================
	/*
	 * Metodo para calcular el poder de ataque
	 */
	public float poderDeAtaque(){
		poderAtaque = beta	*GameControl.control.playerData.expEsperada*GameControl.control.playerData.experiencia/100;
		return poderAtaque;
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
}
