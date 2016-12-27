using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Operaciones : MonoBehaviour {

	private MinijuegoGeneral conexionControlador;
    public int tipo1, tipo2, tipo3, min, max;
    public Text ladoIzq, ladoDer;
    private int op = 0;
    public GameObject opciones;
    public GameObject iconoEsperar;
	// Use this for initialization
	void Start () {
		conexionControlador = gameObject.GetComponent<MinijuegoGeneral> ();

        generarOperacion();
	}
	
	// Update is called once per frame
	void Update () {
        if (iconoEsperar.activeSelf) iconoEsperar.transform.Rotate(-Vector3.forward * Time.deltaTime * 350);
	}


    void generarOperacion()
    {
        if (tipo1 > 0) generarTipo1();
        else if (tipo2 > 0) generarTipo2();
        else if (tipo3 > 0) generarTipo3();
        else generarTipo3();
    }

    void generarTipo1()
    {
        int num1 = Random.Range(min, max);
        int num2 = Random.Range(min, max);
        op = Random.Range(1, 4);
        int res = calcularResultado(num1, num2, op);
        ladoDer.text = num1.ToString();
        ladoIzq.text = num2.ToString() + " = " + res.ToString();
        tipo1--;
    }

    void generarTipo2()
    {
        int num1 = Random.Range(min, max);
        int num2 = Random.Range(min, max);
        int num3 = Random.Range(min, max);
        int opFijo = Random.Range(1, 4);
        op = Random.Range(1, 4);
        int res = calcularResultado(num1, num2, opFijo);
        ladoDer.text = "("+ num1.ToString() + " " + getOperador(opFijo) + num2.ToString() +")";
        res = calcularResultado(res, num3, op);
        ladoIzq.text = num3.ToString() + " = " + res.ToString();
        tipo2--;
    }

    void generarTipo3()
    {
        int num1 = Random.Range(min, max);
        int num2 = Random.Range(min, max);
        int num3 = Random.Range(min, max);
        int opFijo = Random.Range(1, 4);
        op = Random.Range(1, 4);

        ladoDer.text = num1.ToString() + " " + getOperador(opFijo) + " (" + num2.ToString(); 
        int res = calcularResultado(num2, num3, op);
        res = calcularResultado(num1, res, opFijo);
        ladoIzq.text = num3.ToString() + ") = " + res.ToString();
        tipo3--;
    }

    int calcularResultado(int x, int y, int o)
    {
        switch (o)
        {
            case 1:
                return x + y;
            case 2:
                return x - y;
            case 3:
                return x * y;
            case 4:
                return x / y;    
        }
        return 0;
    }

    string getOperador(int o)
    {
        switch (o)
        {
            case 1:
                return "+";
            case 2:
                return "-";
            case 3:
                return "*";
            case 4:
                return "/";
        }
        return "";

    }
    public void verificarOperador(int o)
    {

        if (o == op) conexionControlador.controladorPersonajes.Atacar();
        else {
            conexionControlador.controladorPersonajes.Fallar();
            StartCoroutine(desactivarOpciones());
        }

        generarOperacion();
    }

    private IEnumerator desactivarOpciones()
    {
        opciones.SetActive(false);
        iconoEsperar.SetActive(true);
        yield return new WaitForSeconds(1);
        iconoEsperar.SetActive(false);
        opciones.SetActive(true);
    }
}
