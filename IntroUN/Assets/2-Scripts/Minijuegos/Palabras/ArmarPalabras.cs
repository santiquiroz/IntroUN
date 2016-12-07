using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;  

public class ArmarPalabras : MonoBehaviour {


    public PersonajesScripts controladorPersonajes;
    public TextAsset archivoPalabras;
    public Text palabraIngresada;
    private Dictionary<string, int> palabras;
    private Dictionary<string, int> palabrasIngresadas;
    private Dictionary<string, int> letrasPresentadas;
    private string[] letras;
    private int cantidadCasillas = 12; //Cambiando este valor se puede graduar la dificultad
    private int cantidadLetras = 15; //Cambiando este valor se puede graduar la dificultad


	// Use this for initialization
	void Start () {

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
	//void Update () {
	
	//}

    public void agregarLetra()
    {
        string letraSeleccionada = EventSystem.current.currentSelectedGameObject.GetComponent<Text>().text;
        palabraIngresada.text = palabraIngresada.text + letraSeleccionada;


    }

    public void verificarPalabra()
    {
        string palabra = palabraIngresada.text.ToLower();

        if (palabrasIngresadas.ContainsKey(palabra)) palabraIngresada.text = "";

        else if (palabras.ContainsKey(palabra))
        {
            controladorPersonajes.Atacar();
            palabrasIngresadas[palabra] = 1;
            palabraIngresada.text = "";
        }
        else
        {
            controladorPersonajes.Fallar();
            palabraIngresada.text = "";
        }
        
    }

    public void recargar()
    {
        palabraIngresada.text = "";
        generarLetras();
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

    
}
