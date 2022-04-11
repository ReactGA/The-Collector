using UnityEngine;

public class DisplayInfrontCamera : MonoBehaviour
{
    [SerializeField] float Uidistance = 0.9f;
    [SerializeField] bool ActivateOnEnable = true;
    GameObject camInstance;
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
        Destroy(camInstance);
    }
}
