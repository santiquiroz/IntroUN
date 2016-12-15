using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

	[System.Serializable]
	public class Action{
		public Color color;
		public Sprite srpite;
		public string title;
	}

	public Action[] options;
	public string title;



	void Start(){
		if (title == "" || title == null) {
			title = gameObject.name;
		}
	}

	/*
	void OnMouseDown(){
		RadialMenuSpawner.ins.SpawnMenu (this);
	}
	*/

}
