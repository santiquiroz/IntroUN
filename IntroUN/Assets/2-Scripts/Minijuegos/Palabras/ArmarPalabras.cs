﻿using UnityEngine;
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
    private string[] letras;


	// Use this for initialization
	void Start () {

        palabras = new Dictionary<string,int>();
        letras = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", 
                                "L", "M", "N", "Ñ", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
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
        if (palabras.ContainsKey(palabra)) Debug.Log("Si");
        else Debug.Log("No");

        palabraIngresada.text = "";
        
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
        for (int i = 0; i < 10; i++)
        {
            this.transform.GetChild(i).transform.GetComponent<Text>().text = letras[UnityEngine.Random.Range(0, 27)];

        }
    }

    
}
