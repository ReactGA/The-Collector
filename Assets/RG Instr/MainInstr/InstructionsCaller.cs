using UnityEngine;

[ExecuteInEditMode]
public class InstructionsCaller : MonoBehaviour
{
	public enum CallMethod{None,OnGameObjectActiveTrue,
	    OnGameObjectActiveFalse,OnCollionEnter,OnCollionExit,OnTriggerEnter,OnTriggerExit}

    public enum InstructionType{InstructionObject,CustomInstruction}
	public InstructionType instructionType;
	public CallMethod callMethod;
	[SerializeField] int instructionIndexBegin,instructionIndexStop;
	[SerializeField]bool AutoNextWords,TypeWords;
	[Header("Tag of the object for collison and trigger")][SerializeField] string ObjectTag = "Untagged";
	string[] customInstructiontext;
	
	bool Shown;

	void Update()
	{
		if(instructionType == InstructionType.InstructionObject){
			if(gameObject.GetComponent<InstructionCustom>()){
				DestroyImmediate(gameObject.GetComponent<InstructionCustom>());
			}
		}else{
			if(!gameObject.GetComponent<InstructionCustom>()){
				gameObject.AddComponent<InstructionCustom>();
				instructionIndexBegin = 0;
				instructionIndexStop = 0;
			}
		}
		
		if(!(callMethod == CallMethod.OnGameObjectActiveTrue || callMethod == CallMethod.OnGameObjectActiveFalse)){
			if(!GetComponent<Collider>()){
				gameObject.AddComponent<BoxCollider>();
			}
			GetComponent<Collider>().isTrigger = false;
			if(callMethod == CallMethod.OnTriggerEnter || callMethod == CallMethod.OnTriggerExit){
				if(GetComponent<MeshCollider>()){
					GetComponent<MeshCollider>().convex = true;
				}
				GetComponent<Collider>().isTrigger = true;
			}
		}else{
			
		}
	}
	void Start()
	{
		if(!CheckPlaying()){
			return;
		}
        if(callMethod == CallMethod.OnGameObjectActiveTrue){
	        Invoke("SetInstruction",0.1f);
        }
	}
	public void SetInstruction(){
		if(!CustomInstructionBool()){
	        	CallInstruction();
	        }else{
	        	CallCustomInstruction();
	        }
	}
    void OnDisable()
	{
		if(!CheckPlaying()){
			return;
		}
        if(callMethod == CallMethod.OnGameObjectActiveFalse){
	        if(!CustomInstructionBool()){
	        	CallInstruction();
	        }else{
	        	CallCustomInstruction();
	        }
        }
    }

    void OnCollisionEnter(Collision c)
	{
		if(!CheckPlaying()){
			return;
		}
		if(!(c.collider.CompareTag(ObjectTag))){
			return;
		}
        if(callMethod == CallMethod.OnCollionEnter && !Shown){
	        if(!CustomInstructionBool()){
	        	CallInstruction();
	        }else{
	        	CallCustomInstruction();
	        }
			Shown = true;
        }
    }

    void OnCollisionExit(Collision c)
	{
		if(!(c.collider.CompareTag(ObjectTag))){
			return;
		}
		if(!(c.collider.CompareTag(ObjectTag))){
			return;
		}
		if(!CheckPlaying()){
			return;
		}
        if(callMethod == CallMethod.OnCollionExit && !Shown){
	        if(!CustomInstructionBool()){
	        	CallInstruction();
	        }else{
	        	CallCustomInstruction();
	        }
			Shown = true;
        }
    }
    void OnTriggerEnter(Collider t)
	{
		
		if(!CheckPlaying()){
			return;
		}
		if(!(t.CompareTag(ObjectTag))){
			return;
		}
		if(callMethod == CallMethod.OnTriggerEnter && !Shown){
	        if(!CustomInstructionBool()){
	        	CallInstruction();
	        }else{
	        	CallCustomInstruction();
	        }
			Shown = true;
        }
    }
    void OnTriggerExit(Collider t)
	{
		if(!CheckPlaying()){
			return;
		}
		if(!(t.CompareTag(ObjectTag))){
			return;
		}
        if(callMethod == CallMethod.OnTriggerExit && !Shown){
	        if(!CustomInstructionBool()){
	        	CallInstruction();
	        }else{
	        	CallCustomInstruction();
	        }
			Shown = true;
        }
    }
	void CallInstruction(){
		InstructionClass.options.SetUpInstructions(null,AutoNextWords,TypeWords,2);
	    InstructionClass.options.ShowInstruction(instructionIndexBegin,instructionIndexStop);
    }
    
	void CallCustomInstruction(){
		InstructionClass.options.SetUpInstructions(null,AutoNextWords,TypeWords,2,InstructionClass.InstructionType.CustomInstruction,customInstructiontext);
		InstructionClass.options.ShowInstruction(instructionIndexBegin,instructionIndexStop);
	}

	bool CheckPlaying(){
		if(!Application.isPlaying){
			return false;
		}else
		{
			return true;
		}
	}
	
	bool CustomInstructionBool(){
		if(instructionType == InstructionType.CustomInstruction){
			customInstructiontext = GetComponent<InstructionCustom>().TheCustomInstruction;
			return true;
		}else{
			return false;
		}
		
	}
}
