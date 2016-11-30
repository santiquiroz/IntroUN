using UnityEngine;
using System.Collections;

/*
 * emlopez
 * Script para el movimiento de la Camara
 */


public class TraslacionCamara : MonoBehaviour {


	public float velocidadTraslacion;
	private float movimientoX;
	private float movimientoY;
	private float minX = 635, maxX = 981 , minY = 190, maxY = 352 , minZ = -255, maxZ = -84; //Para restringir la posición de la camara

	Vector3 offset;
	public GameObject player;
	public bool active;

	//==================================================================================================
	void Start () {
		offset = transform.position - player.transform.position;    
	}

	//==================================================================================================
	void Update () {

		if (active && Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
			movimientoX = -touchDeltaPosition.x * velocidadTraslacion;
			movimientoY = -touchDeltaPosition.y * velocidadTraslacion;

			transform.Translate (movimientoX, movimientoY, 0);

			transform.position = new Vector3(
				Mathf.Clamp(transform.position.x, minX, maxX),
				Mathf.Clamp(transform.position.y, minY, maxY),
				Mathf.Clamp(transform.position.z, minZ, maxZ));

		//} else if(Input.GetTouch (0).phase == TouchPhase.Ended){
			//player.GetComponent<EstudianteMovimiento> ().setTraslacion (false);
		}//else if(Input.touchCount == 0){
			transform.position = player.transform.position + offset;
		//}

	}

	//==================================================================================================
	/*
     * Actualiza offest
     *      parametros:
     *                  -nuevoOffset: Vector3 con la nueva posición de la camara
    */
	public void setOffset(Vector3 nuevoOffset){
		offset = nuevoOffset - player.transform.position;
	}

}
