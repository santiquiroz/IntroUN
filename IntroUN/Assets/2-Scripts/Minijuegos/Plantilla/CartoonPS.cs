using UnityEngine;
using System.Collections;

public class CartoonPS : MonoBehaviour {

	public ParticleSystem trails;
	public ParticleSystem smoke;

	public void Play(){
		trails.Play ();
		smoke.Play ();
	}
}
