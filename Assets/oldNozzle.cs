using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class oldNozzle : MonoBehaviour
{
    [SerializeField] GameObject ReplacePut, TochangePart;
    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("hand"))
        {
            Destroy(GetComponent<ConfigurableJoint>());
            GetComponent<Collider>().isTrigger = false;
            ReplacePut.GetComponent<Collider>().enabled = true;
            TochangePart.GetComponent<MeshRenderer>().enabled = false;
            GetComponent<XRGrabInteractable>().movementType = XRBaseInteractable.MovementType.Kinematic;
        }
    }
}
