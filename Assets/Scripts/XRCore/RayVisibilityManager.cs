using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayVisibilityManager : MonoBehaviour
{
    [SerializeField] XRController teleportRay;
    [SerializeField] InputHelpers.Button activationButton;
    [SerializeField] float activationThreshold;
    [SerializeField] GameObject teleportReticle;
    [SerializeField] XRDirectInteractor lH;
    [HideInInspector] public bool HideTeleport;

    private void Start()
    {
        teleportReticle.SetActive(true);
    }
    bool checkActivation(XRController teleportRay)
    {
        InputHelpers.IsPressed(teleportRay.inputDevice, activationButton, out bool isactivated, activationThreshold);
        return isactivated;
    }
    void Update()
    {
        if (!HideTeleport)
        {
            if (lH != null && !lH.isSelectActive)
            {
                teleportRay.gameObject.SetActive(checkActivation(teleportRay));
            }
            else
            {
                teleportRay.gameObject.SetActive(checkActivation(teleportRay));
            }

        }else{
             teleportRay.gameObject.SetActive(false);
        }

    }
}
