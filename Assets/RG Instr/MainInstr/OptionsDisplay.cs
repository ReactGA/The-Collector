using UnityEngine;

public class OptionsDisplay : MonoBehaviour
{
    public static OptionsDisplay Instance;
    bool showingpanel;
    public float offsetFromCamera = 0.7f;
    GameObject PanelsParent;
    GameObject ActiveShownPanel;
    void Awake()
    {
	    Instance = this;
	    PanelsParent = transform.GetChild(0).gameObject;
    }
    void Start()
    {
        CloseButtonOptionPanel();
    }
    void Update()
    {
        /* var tT = Camera.main.transform;
        tT.transform.localEulerAngles = new Vector3(0,tT.transform.localEulerAngles.y,0);
        Debug.DrawRay(tT.position,tT.transform.forward*5,Color.blue);
	    if(ActiveShownPanel != null){
            var Tt =  ActiveShownPanel.transform.position - Camera.main.transform.position;
            Debug.DrawRay(Camera.main.transform.position,Tt*5,Color.red);

            Debug.Log(Vector3.Dot(tT.transform.forward,Tt));
        } */

        if(showingpanel)
        {
            transform.LookAt(Camera.main.transform.position);
            if(Vector3.Distance(transform.position, Camera.main.transform.position) < offsetFromCamera - 0.2f){
                setPosForPanel(true);
            }
        }

        if(Input.GetKey(KeyCode.M)){
	        Debug.Log(PlayerPrefs.GetInt("Controller"));
        }else if(Input.GetKey(KeyCode.N)){
            ShowButtonOptionPanel(1);
        }else if(Input.GetKey(KeyCode.B)){
            ShowButtonOptionPanel(2);
        }
    }

	void setPosForPanel(bool rePosition = false){
		if(!ActiveShownPanel.activeInHierarchy || rePosition == true){
            transform.position = Camera.main.transform.position + Camera.main.transform.forward * offsetFromCamera;
        }
		ActiveShownPanel.SetActive(true);
    }
    public void ShowButtonOptionPanel(int PanelIndex){
        CloseButtonOptionPanel();
        var panel = PanelsParent.transform.transform.GetChild(PanelIndex).gameObject;
        panel.SetActive(true);
        ActiveShownPanel = panel;
        setPosForPanel(true);
        showingpanel = true;
    }

    public void CloseButtonOptionPanel(){
        foreach (Transform child in PanelsParent.transform)
        {
            child.gameObject.SetActive(false);
        }
        showingpanel = false;
        //OptionsPanelsHolder.SetActive(false); 
    }
    
	public void Button1(){
		PlayerPrefs.SetInt("Controller",0);
		CloseButtonOptionPanel();
		InstructionClass.options.SetUpInstructions(null,false,false);
		InstructionClass.options.ShowInstruction(0,2);
	}
	public void Button2(){
		PlayerPrefs.SetInt("Controller",1);
		CloseButtonOptionPanel();
		InstructionClass.options.SetUpInstructions(null,false,false);
		InstructionClass.options.ShowInstruction(0,2);
	}
	public void Button3(){
		PlayerPrefs.SetInt("Controller",2);
		CloseButtonOptionPanel();
		InstructionClass.options.SetUpInstructions(null,false,false);
		InstructionClass.options.ShowInstruction(0,2);
	}
}
