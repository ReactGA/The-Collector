using UnityEngine;

public class LotoBoxCoverHing : MonoBehaviour
{
    [SerializeField] GameObject handleHold;
    float handlezpos;
    Vector2 dRot;
    [HideInInspector] public bool isGrabbable = true;

    //Incase kinematic rb is not grababble
    [SerializeField] Rigidbody handlerRb;

    private void Start()
    {
        handlezpos = handleHold.transform.localPosition.z;
        dRot = new Vector2(transform.localEulerAngles.x, transform.localEulerAngles.y);

    }
    void Update()
    {
        handleHold.transform.localPosition = new Vector3(
            handleHold.transform.localPosition.x, handleHold.transform.localPosition.y, handlezpos
        );
        if (isGrabbable)
        {
            transform.localEulerAngles = new Vector3(dRot.x, dRot.y, -CalcRot());
        }
        else
        {
            transform.localEulerAngles = new Vector3(dRot.x, dRot.y, 0);
        }

        if (Input.GetKeyDown("m"))
        {
            releasedGrab();
        }
        /* else if (Input.GetKeyDown("p"))
        {
            beginGrab();
        } */
    }

    float CalcRot()
    {
        var angle = ((handleHold.transform.localPosition.x + 105.15f) * 12 * 11.2f)
         + ((handleHold.transform.localPosition.y - 0.2f) * 12 * 11.2f);
        return Mathf.Clamp(angle, 0, 90);
    }

    //Incase kinematic rb is not grababble
    /* public void beginGrab()
    {
        handlerRb.constraints = RigidbodyConstraints.None;
        handlerRb.constraints = RigidbodyConstraints.FreezePositionZ;
    } */
    public void releasedGrab()
    {
        if (handleHold.transform.localPosition.x > -104.781f)
        {
            handleHold.transform.localPosition =
            new Vector3(-104.781f, handleHold.transform.localPosition.y, handlezpos);
        }
        if (handleHold.transform.localPosition.x < -105.15f)
        {
            handleHold.transform.localPosition =
            new Vector3(-105.15f, handleHold.transform.localPosition.y, handlezpos);
        }
        if (handleHold.transform.localPosition.y > 0.515f)
        {
            handleHold.transform.localPosition =
            new Vector3(handleHold.transform.localPosition.x, 0.515f, handlezpos);
        }
        if (handleHold.transform.localPosition.y < 0.2f)
        {
            handleHold.transform.localPosition =
            new Vector3(handleHold.transform.localPosition.x, 0.2f, handlezpos);
        }

        //Incase kinematic rb is not grababble
        //handlerRb.constraints = RigidbodyConstraints.FreezeAll;

    }
}
