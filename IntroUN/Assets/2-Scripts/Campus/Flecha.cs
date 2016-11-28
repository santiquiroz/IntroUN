using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flecha : MonoBehaviour {


   /* Por: Eliana Lopez
    * 
    * Script para las flechas que sugieren entrar a un edificio
    * 
    */

   	private  float y0, y1;
   	public float amplitud;
   	public float rapidez;
	public Color[] coloresBase;
   	public GameObject flecha; 

	private Dictionary<string, Color> colores;
    

	// Use this for initialization
	void Start () {
        
        
        flecha.SetActive(false);

		colores = new Dictionary<string,Color> ();
		colores.Add ("Calculo", coloresBase [0]);
		colores.Add ("Fisica", coloresBase [1]);
		colores.Add ("Ingles", coloresBase [2]);
 		
	}
	
	// Update is called once per frame
	void Update () {

        if (flecha.active == true)
        {
            y0 = transform.position.y;
            y1 = y0 + amplitud * Mathf.Sin(rapidez * Time.time);
            transform.position = new Vector3(transform.position.x, y1, transform.position.z);
            transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up); 
        }

        
	}

	public void activarFlecha(Vector3 posicion, string materia)
    {
        //flecha.transform.position = Camera.main.ScreenToWorldPoint(posicion);
		flecha.SetActive (true);
		flecha.transform.position = posicion;
		flecha.GetComponent<SpriteRenderer> ().color = colores [materia];
        
    }
}
