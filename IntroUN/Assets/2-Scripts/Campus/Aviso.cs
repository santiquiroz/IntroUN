// Por: Eliana Lopez

using UnityEngine;
using System.Collections;

/*
 * emlopez
 * Script para la manipulación de los avisos
 */

public class Aviso : MonoBehaviour {

   	public GameObject icono;
	public bool activo = false;

	//==================================================================================================

    void Start()
    {
		icono.SetActive (activo);
    }

	//==================================================================================================

	void Update () {
        transform.Rotate(0, 0, 5);          //Rota el icono sobre un eje
	}

	//==================================================================================================

	/*
	 * Metodo para habilitar un icono en cierto edificio
	 */
    public  void activarAviso(){
		activo = true;
		icono.SetActive (activo);
    }

	//==================================================================================================

	/*
	 * Metodo para deshabilitar un icono en cierto edificio
	 */
    public void desactivarAviso(){  
		activo = false;
		icono.SetActive (activo);
    }
		
  
}
