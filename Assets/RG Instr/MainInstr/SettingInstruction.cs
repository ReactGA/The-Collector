using UnityEngine;

[ExecuteInEditMode]
public class SettingInstruction : MonoBehaviour
{
    [SerializeField] Transform PanelToSetPos;

	[SerializeField]float OffsetFromCamera = 0.7f;


	void SetPos(){
        if(PanelToSetPos.GetComponent<InstructionClass>()){
            var script = PanelToSetPos.GetComponent<InstructionClass>();
            script.offsetFromCamera= OffsetFromCamera;
        }else if(PanelToSetPos.GetComponent<OptionsDisplay>()){
            var script = PanelToSetPos.GetComponent<OptionsDisplay>();
            script.offsetFromCamera = OffsetFromCamera;
        }
    }

    public void Preview(){
        SetPos();
	    PanelToSetPos.position = Camera.main.transform.position + Camera.main.transform.forward * OffsetFromCamera;
	    PanelToSetPos.LookAt(Camera.main.transform);
    }

    void OnValidate()
	{
		if(PanelToSetPos == null){
			return;
		}
        Preview();
    }
}
