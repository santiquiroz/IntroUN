using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlertaParcial : MonoBehaviour {

	public Image background;
	public Image icono;
	public GameObject apoyoObj;
	public Text apoyo;
	public Parcial parcial;
	private Categoria categoria;
	private bool activo;

	void Start(){
		activo = false;
		apoyoObj.SetActive (false);
	}

	public void setAlert(Parcial parcial, Categoria categoria){
		this.parcial = parcial;
		this.categoria = categoria;
		background.color = categoria.color;
		icono.sprite = categoria.icono;
		apoyo.text = parcial.materiaName;
		apoyoObj.GetComponent<Image> ().color = categoria.color;
	}

	public void activarApoyo(){
		activo = !activo;
		apoyoObj.SetActive (activo);
	}
}
