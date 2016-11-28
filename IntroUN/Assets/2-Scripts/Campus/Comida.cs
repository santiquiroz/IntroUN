using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class Comida : MonoBehaviour {

    
    public Text dinero;
    public  GameObject canvasCampus;
    public  GameObject canvasComida;
    private int precioItem;
    private int dineroInicial;
    private int dineroActual;
    private Transform precio;
    private Transform check;
    private float energiaObtenida;
    private string alimentoElegido;
	private Dictionary<string, float> alimentos;
    private List<string> alimentosComprados;


	// Use this for initialization
	void Start () {
        dineroActual = int.Parse(dinero.text);
        alimentos = new Dictionary<string, float>();
        alimentosComprados = new List<string>();

        //Agregar los alimentos disponibles y su valor energetico al diccionario

	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

    public void comprarItem(){

        precio = EventSystem.current.currentSelectedGameObject.transform.GetChild(0);
        check = EventSystem.current.currentSelectedGameObject.transform.GetChild(1);
        alimentoElegido = EventSystem.current.currentSelectedGameObject.name; 
        precioItem = 3;
        if (!check.gameObject.activeSelf )
        {
            if (dineroActual - precioItem >= 0)
            {
                precio.gameObject.SetActive(false);
                check.gameObject.SetActive(true);
                dineroActual -= precioItem;
                dinero.text = dineroActual + "";
                alimentosComprados.Add(alimentoElegido);
            }
            
            
        }
        else
        {
            check.gameObject.SetActive(false);
            precio.gameObject.SetActive(true);
            dineroActual += precioItem;
            dinero.text = dineroActual + "";
            alimentosComprados.Remove(alimentoElegido);

        }
        
        

    }

    public void calcularEnergiaObtenida()
    {
        energiaObtenida = 0;
        foreach (var alimento in alimentosComprados)
        {
            //Sacar del diccionario  el valor energetico del alimento
            //Sumarlo a la energia obtenida
			energiaObtenida += 10;
        }

        canvasComida.SetActive(false);
        canvasCampus.SetActive(true);
		GameObject.FindWithTag("Player").GetComponent<EstudianteEstadisticas>().restaurarEnergia(energiaObtenida);
    }

    public void comprarAlimentos()
    {
        canvasCampus.SetActive(false);
        canvasComida.SetActive(true);
		gameObject.GetComponent<EdificioControl>().activarPointer (false);
    }
}
