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

		float cons = (2 * Mathf.PI / obj.options.Length);

		for (int i = 0; i < obj.options.Length; i++) {
			RadialButton newButton = Instantiate (buttonPrefab) as RadialButton;
			newButton.transform.SetParent (transform, false);

			newButton.id = i;

			float theta = cons * i;
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




	void Update(){
		if (Input.GetMouseButtonUp (0)) {
			if (selected) {
				MoverBotones();
			}
			//Destroy (gameObject);
		}
	}

	void MoverBotones(){

		if (selected.id == buttons.Count - 1)
			return;


		int p = 0;
		int r = 0;
		bool rigth = true;
			
		if (selected.id >= buttons.Count / 2)
			p = buttons.Count - selected.id - 1;
		else {
			p = selected.id + 1;
			rigth = false;
		}


		float cons = (2 * Mathf.PI / buttons.Count);

		for (int i = buttons.Count - 1; i >= 0; i--) {

			if(rigth){
				r = buttons [i].id + p;
				if (r > buttons.Count - 1)
					r -= buttons.Count;
				
			}else{
				r = buttons [i].id - p;
				if (r < 0)
					r = buttons.Count + r;
			}
				
			buttons [i].MoveSelected (r, rigth, buttons.Count, cons, spaceBetween);
			//buttons[i].transform.localPosition = new Vector3 (xPos, yPos, 0f) * spaceBetween;
			//buttons [i].id = r;
		}
	}
}
