using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasMannager : MonoBehaviour {

	//Seccion - Materias
	private bool panelMateActivo;
	public GameObject panelMaterias;

	public Text textMateria1;
	public Text textMateria1Nota;
	public Text textMateria2;
	public Text textMateria2Nota;
	public Text textMateria3;
	public Text textMateria3Nota;

	//Seccion - Info Estudiante
	public Text textNivel;
	public Text textExperiencia;

	//Seccion - Carrear
	public Text textPapa;

	//Seccion - Estado Fisico & Psicologico
	private bool panelEstaActivo;
	public GameObject panelEstadisticas;

	public Image energiaContent;
	public Image estresContent;

	//Seccion - Opciones


	//Aviso Parcial
	public GameObject avisoParcial;
	public Text textAvisoParcial;

	void Awake(){
		avisoParcial.SetActive (false);
	}

	void Start () {
		panelEstaActivo = false;
		panelMateActivo = false;

		panelEstadisticas.SetActive(panelEstaActivo);
		panelMaterias.SetActive(panelMateActivo);
	}
	

	void Update () {
		setEnergiaEstres ();
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
		


	}

	//==================================================================================================
	/*
	 * Metodo para mostrar la Seccion de materias
	*/
	void drawSeccionMateria(){
		/*
		string materia = GameControl.control.materias [0];
		textMateria1.text = materia;
		textMateria1Nota.text = GameControl.control.notas [materia].ToString();

		materia = GameControl.control.materias [1];
		textMateria2.text = materia;																					!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		textMateria2Nota.text = GameControl.control.notas [materia].ToString();

		materia = GameControl.control.materias [2];
		textMateria3.text = materia;
		textMateria3Nota.text = GameControl.control.notas [materia].ToString();
		*/
	}

	//==================================================================================================
	/*
	 * Metodo para mostrar la Seccion de Carrera
	*/
	void drawSeccionCarrera(){

		textExperiencia.text = GameControl.control.playerData.experiencia.ToString ();
		textPapa.text = GameControl.control.playerData.papa.ToString ();
	}

	//==================================================================================================
	/*
	 * Metodo para mostrar la Seccion Info Estudiante
	*/
	void drawSeccionInfoEstudiante(){
	}


	//==================================================================================================
	/*
	 * Metodo para mostrar la Seccion de Estado Fisico & Psicologico
	*/
	void drawSeccionEstado(){
	}

	void setEnergiaEstres(){
		energiaContent.fillAmount = Map ( GameControl.control.playerData.energiaActual , 0,  GameControl.control.playerData.energiaTotal, 0, 1);
		estresContent.fillAmount = Map ( GameControl.control.playerData.estresActual , 0,  GameControl.control.playerData.estresTotal, 0, 1);
	}

	//==================================================================================================
	/*
	 * Metodo para mostrar la Seccion de Opciones
	*/
	void drawSeccionOpciones(){
	}


	//==================================================================================================
	/*
	 * Metodo para mostrar el Aviso del parcial que debe realizar
	*/
	public void drawAvisoParcial(string materia){
		avisoParcial.SetActive (true);
		textAvisoParcial.text = "Es momento de realizar el parcial #";
		textAvisoParcial.text += GameControl.control.playerData.numeroParcial (materia);
		textAvisoParcial.text += " de ";
		textAvisoParcial.text += materia;
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
