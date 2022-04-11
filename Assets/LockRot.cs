using UnityEngine;

public class LockRot : MonoBehaviour
{
    void Update()
    {
        transform.localEulerAngles = new Vector3(0,0,transform.localEulerAngles.z);
    }
}
