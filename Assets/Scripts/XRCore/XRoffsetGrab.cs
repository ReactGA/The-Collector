using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRoffsetGrab : XRBaseInteractable
{
    Vector3 InitialLocalPos;
    Quaternion InitialLocalRot;
    [SerializeField]GameObject particle;

    public void PlayMuzzleFlash(){
        particle.SetActive(true);
        particle.GetComponent<ParticleSystem>().Play();
        Debug.Log("Shotting Gun...");
    }
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectEntering(XRBaseInteractor interactor)
    {
        if(!interactor.attachTransform){
            GameObject grab  = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform,false);
            interactor.attachTransform = grab.transform;
            InitialLocalPos = interactor.attachTransform.localPosition;
            InitialLocalRot = interactor.attachTransform.localRotation;
        }
        

        if(interactor is XRDirectInteractor){
            interactor.attachTransform.position = interactor.transform.position;
            interactor.attachTransform.rotation = interactor.transform.rotation;
        }else
        {
            interactor.attachTransform.localPosition = InitialLocalPos;
            interactor.attachTransform.localRotation = InitialLocalRot;
        }
        base.OnSelectEntering(interactor);
    }
}
