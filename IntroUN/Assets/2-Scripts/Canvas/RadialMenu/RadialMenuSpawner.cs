using UnityEngine;
using System.Collections;

public class RadialMenuSpawner : MonoBehaviour {

	public static RadialMenuSpawner ins;
	public RadialMenu menuPrefab;

	private bool active;
	private Vector3 originalPos;
	public float speed = 8f;
	public float spaceBetween = 65f;

	/*
	void Awake() {
		ins = this;
	}
	
	public void SpawnMenu(Interactable obj){
		RadialMenu newMenu = Instantiate (menuPrefab) as RadialMenu;
		newMenu.transform.SetParent (transform, false);
		newMenu.transform.position = Input.mousePosition;
		newMenu.label.text = obj.title.ToUpper ();
		newMenu.SpawnButtons (obj);
	}
	*/

	void Start(){
		active = false;
		originalPos = transform.localPosition;
	}

	public void RadialMenuSpawn(Interactable obj){
		StartCoroutine (MoveMenu ());
		if (!active) {
			gameObject.GetComponent<RadialMenu> ().SpawnButtons (obj);
		} else {
			gameObject.GetComponent<RadialMenu> ().DeleteButtons ();
		}

	}

	IEnumerator MoveMenu(){
		Vector3 pos = transform.position;
		float timer = 0f;

		while (timer < (1 / speed)) {
			pos.y += spaceBetween/speed * ( active ? -1 : 1);
			pos.x += spaceBetween/speed * ( active ? 1 : -1);

			transform.position = pos;
			timer += Time.deltaTime;
			yield return null;
		}

		if(active)
			transform.localPosition = originalPos;

		active = !active;
	}

}
