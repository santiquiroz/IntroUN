using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

    public GameObject textos;
    private int indiceTexto;
	// Use this for initialization
	void Start () {
        indiceTexto = 0;
        
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

    public void siguienteTexto()
    {
        if (indiceTexto < textos.transform.childCount - 1)
        {
            textos.transform.GetChild(indiceTexto).transform.gameObject.SetActive(false);
            indiceTexto++;
            textos.transform.GetChild(indiceTexto).transform.gameObject.SetActive(true);

        }

    }
}
