using UnityEngine;

public class SelfDisable : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("DisableObj",3);
    }

    void DisableObj(){
        gameObject.SetActive(false);
    }
}
