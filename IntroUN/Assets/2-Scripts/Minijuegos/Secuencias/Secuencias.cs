using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Secuencias : MonoBehaviour {

	private MinijuegoGeneral conexionControlador;
    public Transform secuencia;
    public Transform opciones;
    public Sprite[] sprites;
    public Sprite incognita;
    private Transform child;
    private List<int> indicesOpciones = new List<int>(); //indices de los sprites de las opciones que se van a mostrar
    private int indice; //Indice aleatorio del sprite que se va a mostrar
    private int imagenActual =0;
    private List<Sprite> secuenciaCorrecta = new List<Sprite>(), secuenciaElegida = new List<Sprite>();
    public int cuantas2, cuantas3, cuantas4, cuantas5;



     




	void Start () {
		conexionControlador = gameObject.GetComponent<MinijuegoGeneral> ();

        if (cuantas2 > 0) { StartCoroutine(mostrarSecuencia(0, 2)); cuantas2--; }
        else if (cuantas3 > 0) { StartCoroutine(mostrarSecuencia(0, 3)); cuantas3--; }
        else if (cuantas4 > 0) { StartCoroutine(mostrarSecuencia(0, 4)); cuantas4--; }
        else if (cuantas5 > 0) { StartCoroutine(mostrarSecuencia(0, 5)); cuantas5--; }
        else StartCoroutine(mostrarSecuencia(0, 5));
        
	}
	
	// Update is called once per frame
	void Update () {

        if (secuenciaElegida.Count > 0 && secuenciaElegida.Count == secuenciaCorrecta.Count)
        {
            
            verificarSecuencia();
            reiniciar();
        }
	}


    
    IEnumerator mostrarSecuencia(int itemActual, int cuantas)
    {
        if (itemActual < cuantas)
        {
            indice = Random.Range(0, sprites.Length-1);
            child = secuencia.GetChild(itemActual);
            child.GetComponent<Image>().sprite= sprites[indice];
            if (!indicesOpciones.Contains(indice)) indicesOpciones.Add(indice);
            secuenciaCorrecta.Add(sprites[indice]);
            child.transform.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.2f);
            itemActual++;
            StartCoroutine(mostrarSecuencia(itemActual, cuantas));
        }

        else
        {
            desaparecerSecuencia();
            mostrarOpciones();
        }
        
    }

    private void desaparecerSecuencia()
    {
        foreach (Transform child in secuencia) child.GetComponent<Image>().sprite = incognita;
    }


    void mostrarOpciones()
    {

        int random;
        while (indicesOpciones.Count < opciones.transform.childCount)
        {
            random = Random.Range(0, sprites.Length - 1);
            if (!indicesOpciones.Contains(random)) indicesOpciones.Add(random);
        }
        Shuffle(indicesOpciones);
        int indiceLista =0;
        foreach (Transform child in opciones)
        {
            child.GetComponent<Image>().sprite = sprites[indicesOpciones[indiceLista]];
            indiceLista++;
        }
 
            opciones.gameObject.SetActive(true);
    }


    public  void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void elegirImagen()
    {
        Sprite spriteElegido = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
        secuenciaElegida.Add(spriteElegido);
        secuencia.transform.GetChild(imagenActual).GetComponent<Image>().sprite = spriteElegido;
        imagenActual++;
    }

    public void verificarSecuencia()
    {
        bool correcta = true;
        for (int i = 0; i < secuenciaCorrecta.Count; i++)
        {
            if (!secuenciaCorrecta[i].Equals(secuenciaElegida[i])) { correcta = false; break; }
        }

		if (correcta) conexionControlador.controladorPersonajes.Atacar();
		else conexionControlador.controladorPersonajes.Fallar();
      
    }

    void reiniciar()
    {
        foreach (Transform child in secuencia) child.transform.gameObject.SetActive(false);
        opciones.transform.gameObject.SetActive(false);
        secuenciaElegida.Clear();
        secuenciaCorrecta.Clear();
        indicesOpciones.Clear();
        imagenActual = 0;
        Start();
    }
}
