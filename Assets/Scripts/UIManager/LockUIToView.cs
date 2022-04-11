using UnityEngine;
using System.Collections;

public class LockUIToView : MonoBehaviour
{
    [SerializeField] float Uidistance = 0.5f, Yoffset = 0.08f;
    [Range(0, 1)] [SerializeField] float lerpspeed = 0.15f;
    GameObject camInstance, camInstance1;
    [SerializeField] float threshold = 5;
    Vector3 post, post1;
    bool islerping, hasStarted;
    float dotProduct;

    GameObject FindOrCreateObj(string name)
    {
        var Inst = new GameObject(name);
        return Inst;
    }
    private void Start()
    {
        camInstance = FindOrCreateObj("camInstance");
        camInstance1 = FindOrCreateObj("camInstance1");
        Invoke("begin", 0.5f);


    }
    void begin()
    {
        //hasStarted = true;

        camInstance.transform.position = Camera.main.transform.position;
        camInstance.transform.localEulerAngles = new Vector3(0, Camera.main.transform.localEulerAngles.y, 0);


        camInstance1.transform.position = camInstance.transform.position + new Vector3(0, Yoffset, 0);
        camInstance1.transform.rotation = camInstance.transform.rotation;
        post1 = camInstance1.transform.position + camInstance1.transform.forward * Uidistance;
        islerping = true;
    }
    void Update()
    {

        camInstance.transform.position = Camera.main.transform.position;
        camInstance.transform.localEulerAngles = new Vector3(0, Camera.main.transform.localEulerAngles.y, 0);

        if (!islerping)
        {
            dotProduct = Vector3.Dot((camInstance.transform.forward * Uidistance).normalized, (transform.position - camInstance.transform.position).normalized);
        }
        //if (hasStarted)
        //{
        if (dotProduct <= 0.75f && !islerping)
        {
            camInstance1.transform.position = camInstance.transform.position + new Vector3(0, Yoffset, 0);
            camInstance1.transform.rotation = camInstance.transform.rotation;
            islerping = true;
        }
        //}

        if (islerping)
        {
            if (transform.parent == null)
            {
                post1 = camInstance1.transform.position + camInstance1.transform.forward * Uidistance;
                transform.position = Vector3.Lerp(transform.position, post1, lerpspeed);
                var rot = Quaternion.LookRotation(transform.position - camInstance1.transform.position);
                transform.rotation = rot;
            }
            else
            {
                post1 = camInstance1.transform.position + camInstance1.transform.forward * Uidistance;
                transform.parent.position = Vector3.Lerp(transform.parent.position, post1, lerpspeed);
                var rot = Quaternion.LookRotation(transform.parent.position - camInstance1.transform.position);
                transform.parent.rotation = rot;
            }

            var dot = Vector3.Dot((camInstance1.transform.forward * Uidistance).normalized, (transform.position - camInstance1.transform.position).normalized);
            if (dot == 1)
            {
                islerping = false;
            }

        }



    }
    //var camPlane = Vector3.ProjectOnPlane(Camera.main.transform.position,Vector3.up);
}
