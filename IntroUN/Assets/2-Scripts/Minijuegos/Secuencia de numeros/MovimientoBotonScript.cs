using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MovimientoBotonScript : MonoBehaviour
{

    // Use this for initialization
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Random.insideUnitCircle * 2);
        transform.Rotate(Vector3.forward * 0.2f);
    }

}
