using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.XR;

using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(CharacterController))]
public class ContinousMovement : MonoBehaviour
{
    [SerializeField] XRNode inputSource;
    [SerializeField] float movespeed = 1, additionalHeight = 0.2f;
    [SerializeField] LayerMask floorMask;
    float fallingSpeed, gravity = -9.81f;
    XRRig rig;
    Vector2 inputAxis;
    CharacterController character;
    [SerializeField]bool AllowJoystickMovement = true;

    //Change Speed In-game

    /*     [SerializeField]Slider speedSl;
    [SerializeField]Text speedDisplay; */

    //end

    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();

        //Change Speed In-game
        //speedSl.value = 0.5f;
    }

    void Update()
    {
        if (AllowJoystickMovement)
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        }

        //Change Speed In-game
        /* movespeed = speedSl.value*2;
        speedDisplay.text = movespeed.ToString(); */
    }

    void FixedUpdate()
    {
        CapsuleFollowHeadset();
        if (AllowJoystickMovement)
        {
            Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
            Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
            character.Move(direction * movespeed * Time.fixedDeltaTime);
        }

        //gravity
        bool isgrounded = CheckIfGrounded();
        if (isgrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLenght = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLenght, floorMask);
        return hasHit;
    }

    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }
}
