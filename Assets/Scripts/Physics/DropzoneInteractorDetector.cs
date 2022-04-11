using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DropzoneInteractorDetector : MonoBehaviour
{
    [SerializeField]string TagToCheck;

    private void OnTriggerEnter(Collider c) {
        if(c.CompareTag(TagToCheck)){
            transform.GetComponentInChildren<XRSocketInteractor>().enabled = true;
            transform.GetComponentInChildren<Collider>().enabled = true;
        }
    }
}
