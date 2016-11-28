using UnityEngine;
using System.Collections;

/*
 * emlopez
 * Script para el Zoom de la Camara
 * Basado en: https://unity3d.com/es/learn/tutorials/topics/mobile-touch/pinch-zoom
 */


public class Zoom : MonoBehaviour {
    
    public float perspectiveZoomSpeed;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed;        // The rate of change of the orthographic size in orthographic mode.
    public float minimo, maximo;

    

	//==================================================================================================
    void Start()
    {

    }

	//==================================================================================================
    void Update()
    {

        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (GetComponent<Camera>().orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
               // GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, 25.0f);
                GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, minimo, maximo);

            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, minimo, maximo);
            }

			gameObject.GetComponent<TraslacionCamara> ().setOffset (transform.position);
        }
    }
}