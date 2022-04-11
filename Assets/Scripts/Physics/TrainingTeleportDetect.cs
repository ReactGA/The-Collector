using UnityEngine;

public class TrainingTeleportDetect : MonoBehaviour
{
    bool isDone;
    [SerializeField]ControllerManager cvm;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && !isDone){
            cvm.highlightObject(cvm.leftJoystick, true);
            InstructionClass.options.ShowInstruction(7);
            Invoke("ShowNext",15);
            isDone = true;
        }
    }
    void ShowNext(){
        cvm.CallNextInstr8();
    }
}
