using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * juaalvarezme
 * 
 * Script para controlar el Minijuego de Operaciones marematicas
 */


public class GeneradorOperaciones : MonoBehaviour {

	//Variables de score
	public int aciertos;
	public int errores;
	public int victoriasNecesarias;

	//Variables genericas
	[SerializeField]
	private int tamañoMaximo;
	[SerializeField]
	private int cantidadOperadores;

	private int[] operadores;
	private int[] numeros;
	private int parentesis;
	private int ladoDerecho;

	//Variables de la interfaz
	public Text operacionLD;
	public Text operacionLI;


	public PersonajesScripts controladorPersonajes;

	//==================================================================================================
	/*
	 *	Inicialización de las variables
	*/
	void Start () {

		//controladorPersonajes = GameObject.Find ("MiniJuego").GetComponent<MinijuegoGeneral> ().controladorPersonajes;

		aciertos = 0;
		errores = 0;
		tamañoMaximo = 20;
		parentesis = 0;
		victoriasNecesarias = 15;
		GenerarOperacion ();
	}

	//==================================================================================================
	/*
	 *
	*/
	void Update () {	
	}

	//==================================================================================================
	/*
	 *	Metodo para generar una operación
	*/
	public void GenerarOperacion(){
		calcularCantidad ();
		generarOperadores ();
		calcularTamañoMaximo ();
		generarNumeros ();
		imprimir ();
		//aciertos++;
	}

	//==================================================================================================
	/*
	 *	Método para calcular la cantidad de operadores
	*/
	void calcularCantidad(){
		var proporcion = (float) aciertos / victoriasNecesarias;

		if (proporcion < 0.5)
			cantidadOperadores = 1;
		else {
			cantidadOperadores = 2;
			parentesis = Random.Range (0, 2); 		// 0: lado iz, 1: lado der
		}

		operadores = new int[cantidadOperadores];
		numeros = new int[cantidadOperadores + 1];
	}

	//==================================================================================================
	/*
	 *	Método para generar los operadores
	*/
	void generarOperadores(){
		operadores [0] = Random.Range (0, 4);

		if (cantidadOperadores == 2) {
			operadores [1] = Random.Range (0, 4);
		}
	}

	//==================================================================================================
	/*
	 *	Metodo para calcular el tamaño maximo de los operandos
	*/
	void calcularTamañoMaximo(){
		var proporcion = aciertos / victoriasNecesarias;

		if (proporcion < 0.25)
			tamañoMaximo = 20;
		else if (proporcion < 0.6)
			tamañoMaximo = 50;
		else if (proporcion < 1)
			tamañoMaximo = 100;
		else
			tamañoMaximo = 500;
	}

	//==================================================================================================
	/*
	 *	Metodo para generar/calcular el valor de los operandos
	*/
	void generarNumeros(){
		var a = 1;
		var b = 1;
		var c = 1;

		if (cantidadOperadores == 2) {

			//Parentesis lado Izq
			if (parentesis == 0) {
				if (operadores [0] == 3 && operadores [1] == 3) {
					b = -1;
					c = -2;
				} else if (operadores [0] == 3) {
					b = -1;
				} else if (operadores [0] == 2 && operadores [1] == 3) {
					b = -2;
				} else if (operadores [0] != 3 && operadores [1] == 3) {
					a = -2;
					b = -2;
				}
			
				//Parentesis lado Der
			} else if (parentesis == 1) {
				if (operadores [0] == 3 && operadores [1] == 3) {
					a = -2;
					b = -1;
				} else if (operadores [0] == 3) {
					b = -5;
				} else if (operadores [0] == 2 && operadores [1] == 3) {
					b = -2;
				} else if (operadores [0] != 3 && operadores [1] == 3) {
					a = -2;
				}
			}

			numeros [2] = Random.Range (1, tamañoMaximo);
			numeros [1] = Random.Range (1, tamañoMaximo);
			numeros [0] = Random.Range (1, tamañoMaximo);

			if (a != 1)
				numeros [1] = numeros [2] * numeros [1];

			if (b == -1)
				numeros [0] = numeros [1] * numeros [0];
			if (b == -2 || c != 1)
				numeros [0] = numeros [2] * numeros [0];

			if (parentesis == 0) {
				ladoDerecho = calcularResultado (numeros [0], numeros [1], operadores [0]);
				ladoDerecho = calcularResultado (ladoDerecho, numeros [2], operadores [1]);
			} else {
				ladoDerecho = calcularResultado (numeros [1], numeros [2], operadores [1]);

				if (b == -5)
					numeros [0] = ladoDerecho * numeros [0];
				
				ladoDerecho = calcularResultado (numeros [0], ladoDerecho, operadores [0]);
			}

		} else if (cantidadOperadores == 1) {
			numeros [1] = Random.Range (1, tamañoMaximo);
			numeros [0] = Random.Range (1, tamañoMaximo);

			if (operadores [0] == 3)
				numeros [0] = numeros [0] * numeros [1];
			
			ladoDerecho = calcularResultado (numeros [0], numeros [1], operadores [0]);
		}

	}

	//==================================================================================================
	/*
	 *	Metodo para calcular el resultado de una operación
	 *		parametros: -x: primer operando
	 *					-y: segundo operando
	 *					-operador: tipo de operacion
	 *							0 -> +
	 **							1 -> -
	 **							2 -> *
	 **							3 -> /
	*/
	int calcularResultado(int x, int y, int operador){
		if (operador == 0)
			return x + y;
		else if (operador == 1)
			return x - y;
		else if (operador == 2)
			return x * y;
		else if (operador == 3)
			return x / y;
		return 1;
	}

	//==================================================================================================
	/*
	 *	Metodo para mostrar en pantalla la operación
	*/
	void imprimir(){
		string text = "";
		string ladoDer = "";
		string ladoIzq = "";
		if (cantidadOperadores == 2) {
			if (parentesis == 0) {
				ladoDer += "( ";
				ladoDer += numeros[0];
				ladoDer += formatearOperador(operadores[0]);
				ladoDer += numeros[1];
				ladoDer += " ) ";
				text += formatearOperador(operadores[1]);
				ladoIzq += numeros[2];
				ladoIzq += " = ";
				ladoIzq += ladoDerecho;
			}else if (parentesis == 1) {
				ladoDer += numeros[0];
				ladoDer += formatearOperador(operadores[0]);
				ladoDer += " ( ";
				ladoDer += numeros[1];
				text += formatearOperador(operadores[1]);
				ladoIzq += numeros[2];
				ladoIzq += " ) ";
				ladoIzq += " = ";
				ladoIzq += ladoDerecho;
			}
		}else if( cantidadOperadores == 1){
			ladoDer += numeros[0];
			text += formatearOperador(operadores[0]);
			ladoIzq += numeros[1];
			ladoIzq += " = ";
			ladoIzq += ladoDerecho;
		}
		print (ladoDer + text + ladoIzq);
		operacionLD.GetComponent<Text> ().text = ladoDer;
		operacionLI.GetComponent<Text> ().text = ladoIzq;
	}

	//==================================================================================================
	/*
	 *	Metodo para formatear el operador
	 *		paramtros:	-operador: signo a formatear
	 *
	 *		retorno:	String con el signo corresponfdiente
	 **						0 -> +
	 **						1 -> -
	 **						2 -> *
	 **						3 -> /
	*/
	string formatearOperador(int operador){
		if (operador == 0)
			return " + ";
		else if (operador == 1)
			return " - ";
		else if (operador == 2)
			return " * ";
		else if (operador == 3)
			return " / ";
		return "_";
	}

	//==================================================================================================
	/*
	 *	Metodo activado cuando se selecciona una opcion de la interfaz
	 *		Verifica si la opcion elegida es correcta
	*/
	public void verificar(int respuesta){
		if (cantidadOperadores == 1 && respuesta == operadores [0]) {
			aciertos++;
			controladorPersonajes.Atacar ();
		} else if (cantidadOperadores == 2 && respuesta == operadores [1]) {
			aciertos++;
			controladorPersonajes.Atacar ();
		} else {
			errores++;
			controladorPersonajes.Fallar();
		}
		GenerarOperacion ();
	}
}
