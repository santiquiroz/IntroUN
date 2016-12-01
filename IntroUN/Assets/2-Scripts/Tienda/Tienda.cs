using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Tienda : MonoBehaviour {

    public Text dinero;
    public int cantidadPaginas;
    private int indicePaginaActual;
    private int indiceSiguientePagina;
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
        indicePaginaActual = 0;
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

    public void comprarItem()
    {
        precio = EventSystem.current.currentSelectedGameObject.transform.GetChild(0);
        check = EventSystem.current.currentSelectedGameObject.transform.GetChild(1);
        alimentoElegido = EventSystem.current.currentSelectedGameObject.name;
        precioItem = 3;
        if (!check.gameObject.activeSelf)
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

        //Recupera energia
		restaurarEnergia(energiaObtenida);
    }

    public void pasarPagina()
    {

        Transform paginaActual = this.transform.GetChild(indicePaginaActual);
        indiceSiguientePagina = (indicePaginaActual == cantidadPaginas - 1) ? 0 : indicePaginaActual+1;  
        Transform siguientePagina = this.transform.GetChild(indiceSiguientePagina);

        paginaActual.gameObject.SetActive(false);
        siguientePagina.gameObject.SetActive(true);
        indicePaginaActual = indiceSiguientePagina;
    }


	//Recordar quitar cuando se Arregle Script EstudianteEstaditicas
	public void restaurarEnergia(float dif){
		float energia = GameControl.control.energiaActual + dif;
		float energiaT = GameControl.control.energiaTotal;

		if (energia > energiaT) {
			energia = energiaT;
		}

		GameControl.control.energiaActual = energia;

		// Acá debe retornar a la escena campus
		SceneManager.LoadScene (0);
	}
		

}
