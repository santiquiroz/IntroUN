using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GanarTextoScript2 : MonoBehaviour {

	public Text ganarTexto;

	// Este script tiene como finalidad agregarle texto a ganarTexto, se utiliza para cambiarle el color a ! ¡

	// Use this for initialization
	void Start () {
		ganarTexto.text = "<color=red>¡</color>Ganaste<color=red>!</color>";
	}

	// Update is called once per frame
	void Update () {

	}
}
