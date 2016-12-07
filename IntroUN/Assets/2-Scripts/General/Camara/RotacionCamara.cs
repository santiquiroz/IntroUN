using UnityEngine;
using System.Collections;


/*
 * juaalvarezme
 * Script para la Rotacióm de la Camara
 */

public class RotacionCamara : MonoBehaviour
{


	public GameObject target;
	public float orbitspeed = 100f;
	private Vector3 pivote = new Vector3(0,0,0);

	//==================================================================================================
    void Start()
    {
		transform.LookAt(target.transform);
    }

	//==================================================================================================
	void Update()
    {

		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved) {

			Touch touchZero = Input.GetTouch(0);
			//pivote = target.transform.position;
			pivote.x = target.transform.position.x;
			pivote.z = target.transform.position.z;
			pivote.y = transform.position.y;

			transform.RotateAround(pivote, new Vector3(0,(touchZero.deltaPosition.x>0) ? 1 : -1,0), Time.deltaTime * orbitspeed);
			gameObject.GetComponent<TraslacionCamara> ().setOffset (transform.position);
			//transform.LookAt(target.transform);
		}
		//print (transform.position);
    }
}

