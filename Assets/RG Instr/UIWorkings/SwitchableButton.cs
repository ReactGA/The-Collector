using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class SwitchableButton : MonoBehaviour,IPointerExitHandler,IPointerEnterHandler,IPointerClickHandler
{
	GameObject fill,TextOrIconObj;
	//Text text;
	Color originalTextIconcol,buttonColor;
	[SerializeField]bool EqualizeTextOrIconColor = true;
	
	void Start()
	{
		fill = transform.GetChild(0).gameObject;
		if(TextOrIconObj == null){
			TextOrIconObj = transform.GetChild(1).gameObject;
		}
		originalTextIconcol = GetChildColor(TextOrIconObj);
		buttonColor = transform.GetComponent<Image>().color;
		GetComponent<Button>().transition = Selectable.Transition.None;
		if(fill.GetComponent<Image>().color == Color.white)
			fill.GetComponent<Image>().color = buttonColor;
	}
	public void OnPointerEnter(PointerEventData eventData){
		//Copy these lines of code for whatever input module is used
		fill.SetActive(true);
		if(fill.GetComponent<Image>().color == Color.white)
			SetChildColor(TextOrIconObj,new Color(1-buttonColor.r,1-buttonColor.g,1-buttonColor.b));
	}
	public void OnPointerExit(PointerEventData eventData){
		fill.SetActive(false);
		if(fill.GetComponent<Image>().color == Color.white)
			SetChildColor(TextOrIconObj,originalTextIconcol);
	}
	
	public void OnPointerClick(PointerEventData eventData){
		fill.SetActive(false);
		SetChildColor(TextOrIconObj,originalTextIconcol);
	}
	
	void Update()
	{
		if(TextOrIconObj != null && EqualizeTextOrIconColor == true && !(Application.isPlaying)){

			if(GetChildColor(TextOrIconObj) != transform.GetComponent<Image>().color){
				SetChildColor(TextOrIconObj,transform.GetComponent<Image>().color);
			}
		}
		
	}
	
	Color GetChildColor(GameObject obj){
		if(obj.GetComponent<Text>())
			return obj.GetComponent<Text>().color;
		
		if(obj.GetComponent<Image>())
			return obj.GetComponent<Image>().color;
			
		return Color.grey;
	}
	
	void SetChildColor(GameObject obj,Color ColorToset){
		if(obj.GetComponent<Text>()){
			obj.GetComponent<Text>().color = ColorToset;
		}else if(obj.GetComponent<Image>())
		{
			obj.GetComponent<Image>().color = ColorToset;
		}
	}
	
}
