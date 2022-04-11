using UnityEngine;

public class Enterfillerdetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider c) {
        if(c.CompareTag("Player") && InstructionClass.options.CurrentIndex == 14){
            InstructionClass.options.ShowInstruction(15);
        }else if(c.CompareTag("Phone") && InstructionClass.options.CurrentIndex == 21){
            InstructionClass.options.ShowInstruction(22);
        }else if(c.CompareTag("Player") && transform.gameObject.name == "paletizerChecker" 
        && InstructionClass.options.CurrentIndex == 25)
        {
            InstructionClass.options.ShowInstruction(26);
        }
    }
}
