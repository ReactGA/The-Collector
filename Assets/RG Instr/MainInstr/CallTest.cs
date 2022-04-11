using UnityEngine;

public class CallTest : MonoBehaviour
{

    void OnEnable()
    {
        gameObject.AddComponent<ConfigurableJoint>();
        gameObject.GetComponent<ConfigurableJoint>().xMotion = ConfigurableJointMotion.Locked;
    }
   public void Selected()
   {
       Invoke("Done",2);
   }
    
    void Done(){
        gameObject.layer = 0;
    }
}
