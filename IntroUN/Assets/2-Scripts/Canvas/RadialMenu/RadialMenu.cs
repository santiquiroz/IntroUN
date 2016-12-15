using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RadialMenu : MonoBehaviour {
		
	public Text label;
	public RadialButton buttonPrefab;
	public RadialButton selected;

	private float spaceBetween = 65f;
	public float timeSpawn = 0.03f;
	private List<RadialButton> buttons;



	public void SpawnButtons (Interactable obj) {
		StartCoroutine (AnimatedButtons (obj));
	}

	IEnumerator AnimatedButtons (Interactable obj) {

		buttons = new List<RadialButton> ();

		for (int i = 0; i < obj.options.Length; i++) {
			RadialButton newButton = Instantiate (buttonPrefab) as RadialButton;
			newButton.transform.SetParent (transform, false);

			float theta = (2 * Mathf.PI / obj.options.Length) * i;
			float xPos = Mathf.Sin (theta);
			float yPos = Mathf.Cos (theta);

			newButton.transform.localPosition = new Vector3 (xPos, yPos, 0f) * spaceBetween;
			newButton.circle.color = obj.options [i].color;
			newButton.icon.sprite = obj.options [i].srpite;
			newButton.title = obj.options [i].title;
			newButton.myMenu = this;
			newButton.Anim ();

			buttons.Add (newButton);

			yield return new WaitForSeconds (timeSpawn);
		}

	}


	public void DeleteButtons () {
		StartCoroutine (DeleteAnimButtons());
	}

	IEnumerator DeleteAnimButtons () {

		for (int i = buttons.Count - 1; i >= 0; i--) {
			buttons[i].AnimDestroy ();

			yield return new WaitForSeconds (timeSpawn);
		}
	}


	IEnumerator MoveButtons () {

		float t = (2 * Mathf.PI / buttons.Count);
		for (int i = buttons.Count - 1; i > 0; i--) {

			float theta = t * i - 1;
			float xPos = Mathf.Sin (theta);
			float yPos = Mathf.Cos (theta);

			buttons[i].transform.localPosition = new Vector3 (xPos, yPos, 0f) * spaceBetween;

			yield return new WaitForSeconds (timeSpawn);
		}

		float thet = (2 * Mathf.PI / buttons.Count) * buttons.Count - 1;
		float xP = Mathf.Sin (thet);
		float yP = Mathf.Cos (thet);

		buttons[0].transform.localPosition = new Vector3 (xP, yP, 0f) * spaceBetween;

	}

	void Update(){
		if (Input.GetMouseButtonUp (0)) {
			if (selected) {
				Debug.Log (selected.title + " was selected");
				StartCoroutine (MoveButtons ());
			}
			//Destroy (gameObject);
		}
	}
}
