using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Personalizar : MonoBehaviour {

    public GameObject panelGenero;
    public GameObject panelEstilos;
    public GameObject panelHombres;
    public GameObject panelMujeres;
    private string generoElegido;
    

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	//void Update () {
        
	//}

    public void elegirGenero()
    {
        generoElegido = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(generoElegido);
        elegirEstilo();
        
        
    }

    public void elegirEstilo()
    {
        panelGenero.SetActive(false);
        panelEstilos.SetActive(true);
        if (generoElegido.Equals("Hombre")) panelHombres.SetActive(true);
        else if (generoElegido.Equals("Mujer")) panelMujeres.SetActive(true);
    }

    public void elegirAvatar()
    {
        Debug.Log("Has elegido: " + EventSystem.current.currentSelectedGameObject.name);
    }
}
