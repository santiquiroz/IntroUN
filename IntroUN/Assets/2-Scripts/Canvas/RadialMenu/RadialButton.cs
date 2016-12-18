using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class RadialButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public int id;

	public Image circle;
	public Image icon;
	public string title;
	public RadialMenu myMenu;
	public float speed = 8f;
	private float timeMove = 0.035f;

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

	public void MoveSelected(int r, bool rigth, int t, float cons, float spaceBetween){

		StartCoroutine( AnimatedMoveSelected( r, rigth, t, cons, spaceBetween));
	}

	IEnumerator AnimatedMoveSelected(int r, bool rigth, int t, float cons, float spaceBetween){

		float temp = id;

		while (temp != r) {
			if (rigth) {
				temp += 0.5f;

				if (temp > t - 1)
					temp = 0;
				
			} else {
				temp -= 0.5f;

				if(temp < 0)
					temp = t - 1;
			}

			float theta = cons * temp;
			float xPos = Mathf.Sin (theta);
			float yPos = Mathf.Cos (theta);

			transform.localPosition = new Vector3 (xPos, yPos, 0f) * spaceBetween;
			yield return new WaitForSeconds (timeMove);
		}

		id = r;
	}




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

