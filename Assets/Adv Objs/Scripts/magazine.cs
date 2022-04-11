using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class magazine : MonoBehaviour
{
    bool done;
    public void PlayerSelected()
    {
        CustomInvoke.i.Invoke(1.5f, () =>
        {
            if (!done)
            {
                //PlayerObject.Instance.Reload();
                GetComponent<MeshRenderer>().enabled = false;
                gameObject.layer = 14;
                Destroy(GetComponent<XRGrabInteractable>());
                done = true;
            }
        });

    }
}
