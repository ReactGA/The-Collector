using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class HandPresence : MonoBehaviour
{
    public enum ControllerToShow { handOnly, handwithcontroller, NoneforIK, ControllerOnly }
    public ControllerToShow controllerToShow;
    [SerializeField] GameObject HandPrefab, controllerPrefab;
    [SerializeField] Animator animator;
    [SerializeField] InputDeviceCharacteristics inputDeviceCharacteristics;
    InputDevice targetDevice;
    [SerializeField] GameObject col1, col2;

    void Start()
    {
        SetUpControllerType();
    }

    void SetUpInput()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            //Debug.Log(devices.Count);
        }
        else
        {
            //Debug.Log("No devices found");
        }
    }
    public void SetUpControllerType()
    {

        HandPrefab.SetActive(false);
        controllerPrefab.SetActive(false);

        switch (controllerToShow)
        {
            case ControllerToShow.handOnly:
                HandPrefab.SetActive(true);
                break;

            case ControllerToShow.handwithcontroller:
                HandPrefab.SetActive(true);
                controllerPrefab.SetActive(true);
                break;

            case ControllerToShow.NoneforIK:
                break;

            case ControllerToShow.ControllerOnly:
                controllerPrefab.SetActive(true);
                break;

            default:
                break;
        }

    }

    void AnimateHands()
    {
        /* targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);

        if(primaryButtonValue){} */
        //Debug.Log("Pressing primary button");

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float TriggerButtonValue))
        {
            animator.SetFloat("trigger", TriggerButtonValue);
        }
        else
        {
            animator.SetFloat("trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            animator.SetFloat("grip", gripValue);
            if (col1 != null && col2 != null)
            {
                if (gripValue >= 0.6f)
                {
                    col1.SetActive(false);
                    col2.SetActive(true);
                }
                else
                {
                    col1.SetActive(true);
                    col2.SetActive(false);
                }
            }

        }
        else
        {
            animator.SetFloat("grip", 0);
            col1.SetActive(true);
            col2.SetActive(false);
        }

        /* targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);

        if(primary2DAxisValue != Vector2.zero){} */
        //Debug.Log("Pressing primary2DAxisValue" + primary2DAxisValue);


    }
    void Update()
    {
        if (!targetDevice.isValid)
        {
            SetUpInput();
        }
        else
        {
            if (controllerToShow == ControllerToShow.handOnly || controllerToShow == ControllerToShow.handwithcontroller)
            {
                AnimateHands();
            }
        }

    }
}
