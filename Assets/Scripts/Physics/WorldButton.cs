using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour
{
    public UnityEvent onPressed, onReleased;
    [SerializeField] float threshold = 0.1f, deadzone = 0.025f;

    bool isPressed;
    Vector3 startPos;
    ConfigurableJoint joint;

    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
        if (transform.name == "Clicker1")
        {
            Debug.Log(GetValue() + "  press bool   " + isPressed);
        }

        if (!isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }
        else if (isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }
    float GetValue()
    {
        var val = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;
        if (Mathf.Abs(val) < deadzone)
            val = 0;
        return Mathf.Clamp(val, -1, 1);
    }
    void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
    }

    void Released()
    {
        isPressed = false;
        onReleased.Invoke();
    }


    //New Implementation
    [SerializeField] float PressedThreshold = 1.5f;
    [HideInInspector] public bool hasPressed;
    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("hand") && !hasPressed)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - PressedThreshold, transform.localPosition.z);
            hasPressed = true;
        }
    }
}
