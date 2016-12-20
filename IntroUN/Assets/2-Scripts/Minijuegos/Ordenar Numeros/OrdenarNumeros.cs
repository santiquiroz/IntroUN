using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrdenarNumeros : MonoBehaviour {

    public PersonajesScripts controladorPersonajes;
    public GameObject objetos;
    private int totalObjetos;
    [Range(2, 9)] // El maximo de este rango no debe sobrepasar la cantidad de objetos disponibles!
    public int cantidadObjetos;
    public int minimoEntero, maximoEntero;
    private HashSet<Transform> objetosMostrados = new HashSet<Transform>();
    private HashSet<int> numerosMostrados = new HashSet<int>();
    private List<int> secuenciaElegida = new List<int>();

	// Use this for initialization
	void Start () {
        objetosMostrados.Clear();
        numerosMostrados.Clear();
        secuenciaElegida.Clear();
        totalObjetos = objetos.transform.childCount;
        elegirObjetos();
        mostrarObjetos();
	}




	
	// Update is called once per frame
	void Update () {
        if (objetosMostrados.Count == 0)
        {
            verificarSecuencia();
            Start();
        }
	}

    private void elegirObjetos()
    {
        if (cantidadObjetos < totalObjetos)
        {
            
            int limite = objetos.transform.childCount - 1;
            int indice;

            while (objetosMostrados.Count < cantidadObjetos)
            {
                indice = Random.Range(0, limite);
                objetosMostrados.Add(objetos.transform.GetChild(indice));
            }
        }
        else
        {
            for (int i = 0; i < totalObjetos; i++) objetosMostrados.Add(objetos.transform.GetChild(i));
        }
       
    }


    private int elegirNumero()
    {
        int numero;
        do
        {
            numero = Random.Range(minimoEntero, maximoEntero);
        } while (numerosMostrados.Contains(numero));
        numerosMostrados.Add(numero);
        return numero;
    }


    private void mostrarObjetos()
    {
        
        foreach (Transform objeto in objetosMostrados)
        {
            objeto.gameObject.SetActive(true);
            objeto.GetChild(1).GetComponent<Text>().text = elegirNumero().ToString();
        }

    }

    public void elegirObjeto()
    {
        GameObject objetoElegido = EventSystem.current.currentSelectedGameObject;
        int numero = int.Parse(objetoElegido.transform.GetChild(1).GetComponent<Text>().text);
        secuenciaElegida.Add(numero);
        objetoElegido.SetActive(false);
        objetosMostrados.Remove(objetoElegido.transform);
        
    }

    private void verificarSecuencia()
    {
        bool correcta = true;
        for (int i = 1; i < secuenciaElegida.Count; i++)
        {
            if (secuenciaElegida[i] < secuenciaElegida[i - 1])
            {
                correcta = false;
                break;
            }
        }

        if (correcta) controladorPersonajes.Atacar();
        else controladorPersonajes.Fallar();
        
    }


}
