using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField]ControllerManager ctr;
    private void OnTriggerEnter(Collider c) {
        if(c.CompareTag("Player")){
            InstructionClass.options.ShowInstruction(7);
            Invoke("ShowNextInstruction",10);
        }
    }

    void ShowNextInstruction(){
        InstructionClass.options.ShowInstruction(8);
        ctr.ThreeDbutton.SetActive(true);
    }
}
