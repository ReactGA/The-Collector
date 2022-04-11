using UnityEngine;

public class DisplayInfrontCameraExtension : MonoBehaviour
{
    [SerializeField] float Uidistance = 0.9f, lerpRate = 5, offsetRot;
    [SerializeField] bool ActivateOnEnable = true;
    GameObject camInstance;
    bool islerping;
    private void OnEnable()
    {
        if (ActivateOnEnable)
        {
            ShowInfrontOfCamera();
        }
    }

    public void ShowInfrontOfCamera()
    {
        camInstance = new GameObject("camInstance");
        camInstance.transform.position = Camera.main.transform.position;
        camInstance.transform.localEulerAngles = new Vector3(0, Camera.main.transform.localEulerAngles.y, 0);
        transform.position = camInstance.transform.position + camInstance.transform.forward * Uidistance;
        var rot = Quaternion.LookRotation(transform.position - camInstance.transform.position);
        transform.rotation = rot;

        //For lerp
        camInstance.transform.localEulerAngles = new Vector3(0, Camera.main.transform.localEulerAngles.y + offsetRot, 0);
        islerping = true;
    }
    private void Update()
    {

        if (islerping)
        {
            transform.position = Vector3.Lerp(transform.position, camInstance.transform.position + camInstance.transform.forward * Uidistance, lerpRate);
            var rot = Quaternion.LookRotation(transform.position - camInstance.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, lerpRate);

            var dot = Vector3.Dot((camInstance.transform.forward * Uidistance).normalized, (transform.position - camInstance.transform.position).normalized);
            if (dot == 1)
            {
                islerping = false;
                Destroy(camInstance);
            }

        }
    }
}
