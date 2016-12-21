using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CanvasMannager : MonoBehaviour {

	//Seccion - Materias
	private bool panelMateActivo;
	public GameObject panelMaterias;
    public GameObject panelParciales;
	/*public Text textMateria1;
	public Text textMateria1Nota;
	public Text textMateria2;
	public Text textMateria2Nota;
	public Text textMateria3;
	public Text textMateria3Nota;
     */

	//Seccion - Info Estudiante
    public GameObject panelInfoEstudiante;

	//Seccion - Carrera

	//Seccion - Estado Fisico & Psicologico
	private bool panelEstaActivo;

    public GameObject panelSalud;
	public Image energiaContent;
	public Image estresContent;

	//Seccion - Opciones

    public GameObject panelItems;


	//Aviso Parcial
	public GameObject avisoParcial;
	public Text textAvisoParcial;
	bool activeAvisoParcial;

	void Awake(){
		activeAvisoParcial = false;
		avisoParcial.SetActive (activeAvisoParcial);
	}

	void Start () {
		panelEstaActivo = false;
		panelMateActivo = false;

		panelInfoEstudiante.SetActive(panelEstaActivo);
		panelMaterias.SetActive(panelMateActivo);

        //mostrarPanelInfoEstudiante();////////////////
        //mostrarPanelMaterias();/////////////////////
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
		panelInfoEstudiante.SetActive(panelEstaActivo);
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

		//textExperiencia.text = GameControl.control.playerData.experiencia.ToString ();
		//textPapa.text = GameControl.control.playerData.papa.ToString ();
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
		if (!activeAvisoParcial) {
			textAvisoParcial.text = "Es momento de realizar el parcial #";
			textAvisoParcial.text += GameControl.control.playerData.numeroParcial (materia);
			textAvisoParcial.text += " de ";
			textAvisoParcial.text += materia;
		}
		activeAvisoParcial = !activeAvisoParcial;
		avisoParcial.SetActive (activeAvisoParcial);
	}

	public void drawAvisoParcial(){
		activeAvisoParcial = !activeAvisoParcial;
		avisoParcial.SetActive (activeAvisoParcial);
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

    //========================= Agregado por Eli a partir de aqui ==================================


    public void mostrarPanelMaterias()
    {
        panelMaterias.SetActive(true);
        Materia[] materias = GameControl.control.playerData.materias;
        int i = 0;
        Transform seccion;
        foreach(Materia materia in materias){
            seccion = panelMaterias.transform.GetChild(i);
            i++;
            seccion.GetChild(0).GetComponent<Text>().text = materia.name;
            seccion.GetChild(1).GetComponent<Text>().text = materia.nota.ToString();
            seccion.gameObject.SetActive(true);
        }
        
    }
public void verNotas()
    {
        GameObject materia = EventSystem.current.currentSelectedGameObject;        
        Materia materiaElegida = obtenerMateria(materia.transform.GetChild(0).GetComponent<Text>().text);
        Parcial[] parciales = materiaElegida.parciales;
        panelMaterias.SetActive(false);
        panelParciales.SetActive(true);
        panelParciales.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = materiaElegida.name;
        panelParciales.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = materiaElegida.nota.ToString();
        int i = 1;
        Transform seccion;
        foreach(Parcial parcial in parciales){
            seccion = panelParciales.transform.GetChild(i);
            seccion.GetChild(1).GetComponent<Text>().text = parcial.nota.ToString();
            seccion.gameObject.SetActive(true);
            i++;
        }

    }

private Materia obtenerMateria(string n)
{
    Materia[] materias = GameControl.control.playerData.materias;
    foreach (Materia m in materias) if (m.name.Equals(n)) return m;
    return materias[0];

}

public void panelParcialesToPanelMaterias()
{
    panelParciales.SetActive(false);
    panelMaterias.SetActive(true);
}


public void mostrarPanelInfoEstudiante()
{
    panelInfoEstudiante.transform.GetChild(1).GetComponent<Text>().text = GameControl.control.playerData.nivel.ToString();
    panelInfoEstudiante.transform.GetChild(3).GetComponent<Text>().text = GameControl.control.playerData.experiencia.ToString();
    panelInfoEstudiante.transform.GetChild(3).GetComponent<Text>().text = GameControl.control.playerData.papa.ToString();
    panelInfoEstudiante.SetActive(true);
}

public void mostrarPanelSalud()
{
    panelSalud.SetActive(true);
}

public void mostrarPanelItems()
{
    panelItems.SetActive(true);
}

public void cerrarPanel()
{
    EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
}
}
