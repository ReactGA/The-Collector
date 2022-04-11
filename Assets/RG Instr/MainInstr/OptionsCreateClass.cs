using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class OptionsCreateClass : MonoBehaviour
{
	public bool CreateInEditMode = true,AutoCapitalize = true;
	[SerializeField]GameObject AllPanelsHolder,ButtonOptionsPrefab;
	
	[SerializeField]OptionsObject[] optObjs;
	void Start()
	{
		if(!transform.parent.gameObject.GetComponent<OptionsDisplay>()){
			transform.parent.gameObject.AddComponent<OptionsDisplay>();
		}
	}
	public void CreateButtonOptionsPanel(){
		if(optObjs.Length == 0 || optObjs[0] == null){
			Debug.LogError("No OptionsObject Set");
			return;
		}
			
		if(AllPanelsHolder == null){ AllPanelsHolder = transform.gameObject; }
			
		DestroyAllPanels();
		for (int i = 0; i < optObjs.Length; i++) {
			GeneratePanel(optObjs[i]);
		}
	}
	
	void GeneratePanel(OptionsObject optObj){
		var p = Instantiate(ButtonOptionsPrefab,transform.position,Quaternion.Euler(0,180,0));
		p.transform.SetParent(AllPanelsHolder.transform);
		p.transform.localScale = Vector3.one;
		p.transform.localPosition = Vector3.zero;
		if(optObj.PanelName.Length>0){
			p.name = optObj.PanelName;
		}
		SetUpButtonOptions(optObj,p);
		AllPanelsHolder.transform.localPosition = new Vector3(0,10,0);
		AllPanelsHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(335,230);
		
		for (int i = 0; i < AllPanelsHolder.transform.childCount; i++) {
			AllPanelsHolder.transform.GetChild(i).gameObject.SetActive(false);
		}
	}
	
	void SetUpButtonOptions(OptionsObject optObj,GameObject currentPanel){
		currentPanel.transform.GetChild(0).gameObject.SetActive(optObj.QuestionOrTitle.Length>0);
		currentPanel.transform.GetChild(0).GetComponent<Text>().text = optObj.QuestionOrTitle;
		if(AutoCapitalize){ 
			currentPanel.transform.GetChild(0).GetComponent<Text>().text = optObj.QuestionOrTitle.ToUpper();
		}	
		var ButtonParent = currentPanel.transform.GetChild(1);
		
		GameObject[] buttons =  new GameObject[]{
			ButtonParent.GetChild(0).gameObject,
			ButtonParent.GetChild(1).gameObject,
			ButtonParent.GetChild(2).gameObject,
			ButtonParent.GetChild(3).gameObject,
		};
		
		if (optObj.ButtonsOptions.Length > 1)
		{
			if(optObj.QuestionOrTitle.Length > 0 ){
				AllPanelsHolder.GetComponent<RectTransform>().offsetMax = new Vector2(0,-20);
			}else if(optObj.QuestionOrTitle.Length > 0)
			{
				AllPanelsHolder.GetComponent<RectTransform>().offsetMax = Vector2.zero;
			}
			
			foreach (var btn in buttons)
			{
				btn.SetActive(false);
			}
			
			for (int i = 0; i < optObj.ButtonsOptions.Length; i++) {
				buttons[i].SetActive(true);
				buttons[i].name = optObj.ButtonsOptions[i];
				buttons[i].GetComponentInChildren<Text>().text = optObj.ButtonsOptions[i];
				if(AutoCapitalize){ 
					buttons[i].GetComponentInChildren<Text>().text = optObj.ButtonsOptions[i].ToUpper();
				}
			}
			
			switch (optObj.ButtonsOptions.Length)
			{
			case 2:
				for (int i = 0; i == 1; i++) {
					var rect = buttons[i].GetComponent<RectTransform>();
					rect.sizeDelta = new Vector2(250,45);
					rect.position = new Vector3(0,-80,0);
				}
				buttons[1].GetComponent<RectTransform>().localPosition = new Vector3(0,-150+100,0);
				
				break;
				case 3:
					for (int i = 0; i == 2; i++) {
						var rect = buttons[i].GetComponent<RectTransform>();
						rect.sizeDelta = new Vector2(250,45);
						rect.position = new Vector3(0,-67,0);
					}
					buttons[1].GetComponent<RectTransform>().localPosition = new Vector3(0,-121+100,0);
					buttons[2].GetComponent<RectTransform>().localPosition = new Vector3(0,-175+100,0);
				break;
				case 4:
					for (int i = 0; i == 3; i++) {
						var rect = buttons[i].GetComponent<RectTransform>();
						rect.sizeDelta = new Vector2(140,45);
						rect.position = new Vector3(-78,-78,0);
					}
					buttons[1].GetComponent<RectTransform>().localPosition = new Vector3(78,-78,0);
					buttons[2].GetComponent<RectTransform>().localPosition = new Vector3(-78,-152,0);
					buttons[3].GetComponent<RectTransform>().localPosition = new Vector3(78,-152,0);
				break;
				default:
				break;
			}
		}
		
	}
	
	void DestroyAllPanels(){
		if(AllPanelsHolder.transform.childCount>0){
			foreach (Transform t in AllPanelsHolder.transform)
			{
				DestroyImmediate(t.gameObject);
			}
			foreach (Transform t in AllPanelsHolder.transform)
			{
				DestroyImmediate(t.gameObject);
			}
			foreach (Transform t in AllPanelsHolder.transform)
			{
				DestroyImmediate(t.gameObject);
			}
			foreach (Transform t in AllPanelsHolder.transform)
			{
				DestroyImmediate(t.gameObject);
			}
			foreach (Transform t in AllPanelsHolder.transform)
			{
				DestroyImmediate(t.gameObject);
			}
		}
		
		
		
	}
}
