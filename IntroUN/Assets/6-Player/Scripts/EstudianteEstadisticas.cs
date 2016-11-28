using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EstudianteEstadisticas : MonoBehaviour {

	//Datos estudiante
	private float exp;
	public float semestre;
	public float papa;
	public string nivel;
	public float poderAtaque;

	public int cantidadMaterias;
	public string[] materias;
	public Dictionary<string, int> coloresMaterias = new Dictionary<string, int>();
	public Dictionary<string, float> notas = new Dictionary<string, float>();

	public float beta = 0.2f;

	//Variables Interfaz
	private bool panelEstaActivo;
	public GameObject panelEstadisticas;
	private bool panelMateActivo;
	public GameObject panelMaterias;


	public Text textNivel;
	public Text textExp;
	public Text textPapa;
	public Text textProm;

	public Text textMateria1;
	public Text textMateria1Nota;
	public Text textMateria2;
	public Text textMateria2Nota;
	public Text textMateria3;
	public Text textMateria3Nota;

	public Image energiaContent;
	public Image estresContent;

	//Variables de control
	private float time = 0f;
	private float timeDelta;
	private float energiaPorRespiro;
	private float energiaPorCorrer;
	private float energiaPorBatalla;
	private float energiaPorJefe;
	private float estresPorBatalla;
	private float estresPorJefe;

	void Start () {

		panelEstaActivo = false;
		panelMateActivo = false;

		panelEstadisticas.SetActive(panelEstaActivo);
		panelMaterias.SetActive(panelMateActivo);

		exp = GameControl.control.experiencia;
		semestre = GameControl.control.semestre;
		papa = GameControl.control.papa;
		nivel = GameControl.control.nivel;
		poderAtaque = beta	*GameControl.control.expEsperada*exp/100;
		GameControl.control.poderAtaque = poderAtaque;

		timeDelta = time;
		energiaPorRespiro = 0.1f;
		energiaPorBatalla = 15f;
		energiaPorJefe = 28f;
		estresPorBatalla = 22f;
		estresPorJefe = 28f;

		drawEstadisticas ();
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
	 * Prueba para activar/desactivar el panel estadisticas
	*/
	public void setPanelEstadisticas(){
		if (panelMateActivo)
			setPanelMaterias ();

		panelEstaActivo = !panelEstaActivo;
		panelEstadisticas.SetActive(panelEstaActivo);
	}

	//==================================================================================================
	/*
	 * Prueba para activar/desactivar el panel de materias
	*/
	public void setPanelMaterias(){
		if (panelEstaActivo)
			setPanelEstadisticas ();

		panelMateActivo = !panelMateActivo;
		panelMaterias.SetActive(panelMateActivo);
	}


	//==================================================================================================
	/*
	 * Metodo para actualizar los textos de la intefaz
	*/
	public void drawEstadisticas(){
		textExp.text = exp.ToString ();
		textPapa.text = papa.ToString ();

		string materia = GameControl.control.materias [0];
		textMateria1.text = materia;
		textMateria1Nota.text = GameControl.control.notas [materia].ToString();

		materia = GameControl.control.materias [1];
		textMateria2.text = materia;
		textMateria2Nota.text = GameControl.control.notas [materia].ToString();

		materia = GameControl.control.materias [2];
		textMateria3.text = materia;
		textMateria3Nota.text = GameControl.control.notas [materia].ToString();

		setEnergia (0);
		setEstres (0);

	}


	//==================================================================================================
	/*
	 * Metodo para reducir la energia
	 * 		parámetros: -dif: Cantida que disminuye la energia
	 */
	public bool enregiaActividad(int actividad){
		float energia = GameControl.control.energiaActual;
		float estres = GameControl.control.estresActual;

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
		float energia = GameControl.control.energiaActual - dif;
		float energiaT = GameControl.control.energiaTotal;
	
		if (energia < 0) {
			energia = 0;
		}
		
		energiaContent.fillAmount = Map (energia, 0, energiaT, 0, 1);
		GameControl.control.energiaActual = energia;
	}

	//==================================================================================================
	/*
	 * Metodo para reiniciar la energia
	 */
	public void restaurarEnergia(float dif){
		float energia = GameControl.control.energiaActual + dif;
		float energiaT = GameControl.control.energiaTotal;

		if (energia > energiaT) {
			energia = energiaT;
		}

		energiaContent.fillAmount = Map (energia, 0, energiaT, 0, 1);
		GameControl.control.energiaActual = energia;
	}

	//==================================================================================================
	/*
	 * Metodo para reducir el estres
	 * 		parámetros: -dif: Cantida que disminuye la energia
	 */
	public void setEstres(float dif){
		float estres = GameControl.control.estresActual - dif;
		float estresT = GameControl.control.estresTotal;

		if (estres< 0)
			estres = 0;

		estresContent.fillAmount = Map (estres, 0, estresT, 0, 1);
		GameControl.control.estresActual = estres;
	}

	//==================================================================================================
	/*
	 * Metodo para reiniciar la energia
	 */
	public void restaurarEestres(){
		GameControl.control.estresActual = GameControl.control.estresTotal;
		estresContent.fillAmount = 0;
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
