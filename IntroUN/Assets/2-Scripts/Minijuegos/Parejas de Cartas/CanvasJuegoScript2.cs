using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasJuegoScript2 : MonoBehaviour
{

    public bool imagenVolteada;
    GameObject tagBotonPresionado;
    int cont;

	public PersonajesScripts controladorPersonajes;
    //public GameObject estudiante;
    //public GameObject enemigo;

    //public Text ganarTexto;
    //public Text perderTexto;
    //public Text textoTiempo;
    //public Text textoReset;
    //public Text textoSalir;

    public Image copo;
    public Image copoPareja;
    public Image corazon;
    public Image corazonPareja;
    public Image diamante;
    public Image diamantePareja;
    public Image pica;
    public Image picaPareja;
    public Image trebol;
    public Image trebolPareja;

    public Sprite fondo;
    public Sprite copoImagen;
    public Sprite corazonImagen;
    public Sprite diamanteImagen;
    public Sprite picaImagen;
    public Sprite trebolImagen;

    GameObject primerPresionado;
    GameObject segundoPresionado;

    public Transform[] cartas = new Transform[10];
    Vector3[] posCartas = new Vector3[10];


    // Use this for initialization
    void Start()
    {
        //ganarTexto.enabled = false;
        //perderTexto.enabled = false;
        //textoReset.enabled = false;
        //textoSalir.enabled = false;
        imagenVolteada = false;
        primerPresionado = null;
        segundoPresionado = null;
        cont = 0;
        GenerarPosiciones(posCartas);
        Posicionar();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            tagBotonPresionado = EventSystem.current.currentSelectedGameObject.gameObject;
        }
        catch (NullReferenceException ex)
        {
        }

        if (cont == 5)
        {
			controladorPersonajes.Atacar();
            cont = 0;
			if (controladorPersonajes.saludEnimigo <= 0)
            {
                Invoke("JuegoGanado", 5f);
            }
            else
            {
                ReiniciarPresionado();
            }
        }
    }

    public void GenerarPosiciones(Vector3[] posC)
    {
        for (int i = 0; i < posC.Length; i++)
        {
            posC[i] = cartas[i].position;
        }
    }

    public void Posicionar()
    {
        Barajar(posCartas);
        for (int i = 0; i < cartas.Length; i++)
        {
            cartas[i].position = posCartas[i];
        }
    }

    public void Barajar(Vector3[] pos)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            Vector3 temp = pos[i];
            int posIndex = UnityEngine.Random.Range(0, (pos.Length - 1));
            pos[i] = pos[posIndex];
            pos[posIndex] = temp;
        }
    }

    public void CartaPresionada()
    {
        if (primerPresionado == null)
        {
            primerPresionado = tagBotonPresionado;
            VoltearImagen(primerPresionado);
        }
        else if (segundoPresionado == null)
        {
            segundoPresionado = tagBotonPresionado;
            VoltearImagen(segundoPresionado);
            StartCoroutine(Esperar());
        }
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(0.5f);
        CompararCartas();
    }

    public void CompararCartas()
    {
		if (primerPresionado.tag == segundoPresionado.tag && primerPresionado.gameObject != segundoPresionado.gameObject)
        {
            primerPresionado.GetComponent<Image>().enabled = false;
            segundoPresionado.GetComponent<Image>().enabled = false;
            cont++;
            primerPresionado = null;
            segundoPresionado = null;
        }
        else
        {
            VoltearImagen2(primerPresionado, segundoPresionado);
			controladorPersonajes.Fallar ();
            primerPresionado = null;
            segundoPresionado = null;
        }
    }

    public void VoltearImagen(GameObject img)
    {
        switch (img.tag)
        {
            case "Copo":
                img.GetComponent<Image>().sprite = copoImagen;
                break;
            case "Corazon":
                img.GetComponent<Image>().sprite = corazonImagen;
                break;
            case "Diamante":
                img.GetComponent<Image>().sprite = diamanteImagen;
                break;
            case "Pica":
                img.GetComponent<Image>().sprite = picaImagen;
                break;
            case "Trebol":
                img.GetComponent<Image>().sprite = trebolImagen;
                break;
        }
    }

    public void VoltearImagen2(GameObject img1, GameObject img2)
    {
        img1.GetComponent<Image>().sprite = fondo;
        img2.GetComponent<Image>().sprite = fondo;
    }

    public void VoltearImagenes()
    {
        if (!imagenVolteada)
        {
            imagenVolteada = true;
            copo.sprite = fondo;
            copoPareja.sprite = fondo;
            corazon.sprite = fondo;
            corazonPareja.sprite = fondo;
            diamante.sprite = fondo;
            diamantePareja.sprite = fondo;
            pica.sprite = fondo;
            picaPareja.sprite = fondo;
            trebol.sprite = fondo;
            trebolPareja.sprite = fondo;
        }
        else
        {
            imagenVolteada = false;
            copo.sprite = copoImagen;
            copoPareja.sprite = copoImagen;
            corazon.sprite = corazonImagen;
            corazonPareja.sprite = corazonImagen;
            diamante.sprite = diamanteImagen;
            diamantePareja.sprite = diamanteImagen;
            pica.sprite = picaImagen;
            picaPareja.sprite = picaImagen;
            trebol.sprite = trebolImagen;
            trebolPareja.sprite = trebolImagen;
        }
    }

    public void JuegoGanado()
    {
        //estudiante.gameObject.SetActive(false);
        //estudiante.gameObject.SetActive(false);
        //textoTiempo.gameObject.SetActive(false);
        //ganarTexto.enabled = true;

        copo.enabled = false;
        copoPareja.enabled = false;
        corazon.enabled = false;
        corazonPareja.enabled = false;
        diamante.enabled = false;
        diamantePareja.enabled = false;
        pica.enabled = false;
        picaPareja.enabled = false;
        trebol.enabled = false;
        trebolPareja.enabled = false;
    }

    public void JuegoPerdido()
    {
        //estudiante.gameObject.SetActive(false);
        //enemigo.gameObject.SetActive(false);
        //perderTexto.enabled = true;
        //textoReset.enabled = true;
        //textoSalir.enabled = true;
        //textoTiempo.gameObject.SetActive(false);

        copo.enabled = false;
        copoPareja.enabled = false;
        corazon.enabled = false;
        corazonPareja.enabled = false;
        diamante.enabled = false;
        diamantePareja.enabled = false;
        pica.enabled = false;
        picaPareja.enabled = false;
        trebol.enabled = false;
        trebolPareja.enabled = false;
    }

    public void ReiniciarPresionado()
    {
        //estudiante.gameObject.SetActive(true);
        //enemigo.gameObject.SetActive(true);
        //textoTiempo.gameObject.SetActive(true);
        //textoTiempo.GetComponent<Temporizador2Script>().temporizador = 45;
        //textoTiempo.GetComponent<Temporizador2Script>().puntoDeControl = 45;
        //textoTiempo.GetComponent<Temporizador2Script>().primerControl = false;
        //perderTexto.enabled = false;
        //textoReset.enabled = false;
        //textoSalir.enabled = false;

        VoltearImagenes();
        copo.enabled = true;
        copoPareja.enabled = true;
        corazon.enabled = true;
        corazonPareja.enabled = true;
        diamante.enabled = true;
        diamantePareja.enabled = true;
        pica.enabled = true;
        picaPareja.enabled = true;
        trebol.enabled = true;
        trebolPareja.enabled = true;
		DesactivarBotones ();
        Posicionar();
    }

	public void DesactivarBotones(){
		copo.GetComponent<Button> ().enabled = false;
		copoPareja.GetComponent<Button> ().enabled = false;
		corazon.GetComponent<Button> ().enabled = false;
		corazonPareja.GetComponent<Button> ().enabled = false;
		diamante.GetComponent<Button> ().enabled = false;
		diamantePareja.GetComponent<Button> ().enabled = false;
		pica.GetComponent<Button> ().enabled = false;
		picaPareja.GetComponent<Button> ().enabled = false;
		trebol.GetComponent<Button> ().enabled = false;
		trebolPareja.GetComponent<Button> ().enabled = false;
	}

	public void ActivarBotones(){
		copo.GetComponent<Button> ().enabled = true;
		copoPareja.GetComponent<Button> ().enabled = true;
		corazon.GetComponent<Button> ().enabled = true;
		corazonPareja.GetComponent<Button> ().enabled = true;
		diamante.GetComponent<Button> ().enabled = true;
		diamantePareja.GetComponent<Button> ().enabled = true;
		pica.GetComponent<Button> ().enabled = true;
		picaPareja.GetComponent<Button> ().enabled = true;
		trebol.GetComponent<Button> ().enabled = true;
		trebolPareja.GetComponent<Button> ().enabled = true;
	}

}
