using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PieMenu : MonoBehaviour {

	public List<MenuButton> buttons = new List<MenuButton> ();
	private Vector2 mousePosition;
	private Vector2 fromVector2M = new Vector2(0.3f, 1.0f);
	private Vector2 centerCircle = new Vector2 (0.5f, 0.5f);
	private Vector2 toVector2M;

	public int menuItem;
	public int CurMenuItem;
	private int OldMenuItem;

	void Start () {
		menuItem = buttons.Count;
		foreach (MenuButton button in buttons) {
			button.sceneImage.color = button.normalColor;
		}
	}


	void Update () {
		GetCurrentMenuItem ();
		if (Input.GetButtonDown ("Fire1"))
			ButtonAction ();
	}

	public void GetCurrentMenuItem(){
		mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

		toVector2M = new Vector2 (mousePosition.x / Screen.width, mousePosition.y / Screen.height);

		float angle = (Mathf.Atan2 (fromVector2M.y - centerCircle.y, fromVector2M.x - centerCircle.x) - Mathf.Atan2 (toVector2M.y - centerCircle.y, toVector2M.x - centerCircle.x)) * Mathf.Rad2Deg;

		if (angle < 0)
			angle += 360;

		CurMenuItem = (int) (angle / (360 / menuItem));

		if (CurMenuItem != OldMenuItem) {
			buttons [OldMenuItem].sceneImage.color = buttons [OldMenuItem].normalColor;
			OldMenuItem = CurMenuItem;
			buttons [CurMenuItem].sceneImage.color = buttons [CurMenuItem].HigligthedColor;
		}
	}

	public void ButtonAction(){
		buttons [CurMenuItem].sceneImage.color = buttons [CurMenuItem].PressedColor;

	}
}

[System.Serializable]
public class MenuButton{
	public string name;
	public Image sceneImage;
	public Color normalColor = Color.white;
	public Color HigligthedColor = Color.gray;
	public Color PressedColor = Color.gray;
}
