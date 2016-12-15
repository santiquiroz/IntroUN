using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class RadialButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


	public Image circle;
	public Image icon;
	public string title;
	public RadialMenu myMenu;
	public float speed = 8f;

	Color defaultColor;

	//Create Option
	public void Anim(){
		StartCoroutine (AnimatedButton ());
	}

	IEnumerator AnimatedButton(){
		transform.localScale = Vector3.zero;
		float timer = 0f;
		while (timer < (1 / speed)) {
			transform.localScale = Vector3.one * timer * speed;
			timer += Time.deltaTime;
			yield return null;
		}
		transform.localScale = Vector3.one;
	}

	//Destroy Option
	public void AnimDestroy(){
		StartCoroutine (AnimatedDestroyButton ());
	}

	IEnumerator AnimatedDestroyButton(){
		transform.localScale = Vector3.zero;
		float timer = (1 / speed);
		while (timer > 0) {
			transform.localScale = Vector3.one * timer * speed;
			timer -= Time.deltaTime;
			yield return null;
		}
		transform.localScale = Vector3.zero;
		Destroy (gameObject);
	}

	//Move Option
	/*
	public void AnimMove(){
		StartCoroutine (AnimatedMoveButton ());
	}

	IEnumerator AnimatedMoveButton(float posInit, float posFin){
		Vector3 pos = transform.position;
		float timer = 0f;

		while (timer < (1 / speed)) {
			pos.y += spaceBetween/speed * ( active ? -1 : 1);
			pos.x += spaceBetween/speed * ( active ? 1 : -1);

			transform.position = pos;
			timer += Time.deltaTime;
			yield return null;
		}
	}
	*/



	#region IPointerEnterHandler implementation
	public void OnPointerEnter (PointerEventData eventData)
	{
		myMenu.selected = this;
		defaultColor = circle.color;
		circle.color = Color.white;
	}
	#endregion


	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		myMenu.selected = null;
		circle.color = defaultColor;
	}

	#endregion


}

