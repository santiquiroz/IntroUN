using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;  

public class ArmarPalabras : MonoBehaviour {


	private MinijuegoGeneral conexionControlador;
    public TextAsset archivoPalabras;
    public Text palabraIngresada;
    public Transform iconoCargar;
    private Dictionary<string, int> palabras;
    private Dictionary<string, int> palabrasIngresadas;
    private Dictionary<string, int> letrasPresentadas;
    private string[] letras;
    private int cantidadCasillas = 8; //Cambiando este valor se puede graduar la dificultad
    private int cantidadLetras = 12; //Cambiando este valor se puede graduar la dificultad (cantCasillas-27)
    private bool esperar;
    


	// Use this for initialization
	void Start () {

		conexionControlador = gameObject.GetComponent<MinijuegoGeneral> ();

        esperar = false;
        palabras = new Dictionary<string,int>();
        palabrasIngresadas = new Dictionary<string, int>();
        letrasPresentadas = new Dictionary<string, int>();
        letras = new string[] { "E", "A", "O", "S", "R", "N", "I", "D", "L", "C", "T", 
                                "U", "M", "P", "B", "G", "V", "Y", "Q", "H", "F", "Z", "J", "Ñ", "X", "W", "K"};
        string[] lineas = archivoPalabras.text.Split('\n');
        foreach (string linea in lineas)
        {
            palabras[linea.Trim()] = 1;

        }

        generarLetras();
        
     }
	
	
	// Update is called once per frame
	void Update () {
	    if (esperar) iconoCargar.Rotate(-Vector3.forward * Time.deltaTime * 350); 
	}

    public void agregarLetra()
    {
        if (!esperar)
        {
            string letraSeleccionada = EventSystem.current.currentSelectedGameObject.GetComponent<Text>().text;
            palabraIngresada.text = palabraIngresada.text + letraSeleccionada;
        }


    }

    public void verificarPalabra()
    {
        string palabra = palabraIngresada.text.ToLower();

        if (palabrasIngresadas.ContainsKey(palabra)) palabraIngresada.text = "";

        else if (palabras.ContainsKey(palabra))
        {
			
			conexionControlador.controladorPersonajes.Atacar();
            palabrasIngresadas[palabra] = 1;
            palabraIngresada.text = "";
        }
        else
        {
			conexionControlador.controladorPersonajes.Fallar();
            palabraIngresada.text = "";
        }
        
    }

    public void recargar()
    {
        if (!esperar)
        {
            palabraIngresada.text = "";
            StartCoroutine(esperar5segundos());
        }
        
    }

    public void borrar()
    {
        palabraIngresada.text = "";
    }

    void generarLetras()
    {
        string letra = "";
        letrasPresentadas.Clear();
        for (int i = 0; i < cantidadCasillas; i++)
        {
            do
            {
                letra = letras[UnityEngine.Random.Range(0, cantidadLetras)];
            } while (letrasPresentadas.ContainsKey(letra));

            this.transform.GetChild(i).transform.GetComponent<Text>().text = letra;
            letrasPresentadas[letra] = 1;

        }
    }

    IEnumerator esperar5segundos()
    {
        esperar = true;
        iconoCargar.transform.gameObject.SetActive(true);
        for (int i = 0; i < cantidadCasillas; i++)
        {
            this.transform.GetChild(i).transform.gameObject.SetActive(false);

        }
        yield return new WaitForSeconds(5);
        iconoCargar.transform.gameObject.SetActive(false);
        esperar = false;
        for (int i = 0; i < cantidadCasillas; i++)
        {
            this.transform.GetChild(i).transform.gameObject.SetActive(true);

        }
        generarLetras();
    }
    
}
