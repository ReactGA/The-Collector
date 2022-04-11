using UnityEngine;

[ExecuteInEditMode]
public class OptionsCreateCaller : MonoBehaviour
{
    public enum CallMethod
    {
        OnGameObjectActiveTrue,
        OnGameObjectActiveFalse, OnCollionEnter, OnCollionExit, OnTriggerEnter, OnTriggerExit
    }
    public CallMethod callMethod;
    [SerializeField] string ObjectTag = "Untagged";
    bool Shown;
    [SerializeField]int OptionPanelIndexToShow = 0;
	void Start()
    {
	    Invoke("CallStartOptions",0.1f);
        
    }
    void CallStartOptions(){
        if (!CheckPlaying())
        {
            return;
        }
        if (callMethod == CallMethod.OnGameObjectActiveTrue)
        {
            ShowOptionsPanel();
        }
    }
    void Update()
    {
        if (!(callMethod == CallMethod.OnGameObjectActiveTrue || callMethod == CallMethod.OnGameObjectActiveFalse))
        {
            if (!GetComponent<Collider>())
            {
                gameObject.AddComponent<BoxCollider>();
            }
            GetComponent<Collider>().isTrigger = false;
            if (callMethod == CallMethod.OnTriggerEnter || callMethod == CallMethod.OnTriggerExit)
            {
                if (GetComponent<MeshCollider>())
                {
                    GetComponent<MeshCollider>().convex = true;
                }
                GetComponent<Collider>().isTrigger = true;
            }
        }
    }
    void OnDisable()
    {
        if (!CheckPlaying())
        {
            return;
        }
        if (callMethod == CallMethod.OnGameObjectActiveFalse)
        {
            ShowOptionsPanel();
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if (!CheckPlaying())
        {
            return;
        }
        if (!(c.collider.CompareTag(ObjectTag)))
        {
            return;
        }
        if (callMethod == CallMethod.OnCollionEnter && !Shown)
        {
            ShowOptionsPanel();
            Shown = true;
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (!(c.collider.CompareTag(ObjectTag)))
        {
            return;
        }
        if (!(c.collider.CompareTag(ObjectTag)))
        {
            return;
        }
        if (!CheckPlaying())
        {
            return;
        }
        if (callMethod == CallMethod.OnCollionExit && !Shown)
        {
            ShowOptionsPanel();
            Shown = true;
        }
    }
    void OnTriggerEnter(Collider t)
    {

        if (!CheckPlaying())
        {
            return;
        }
        if (!(t.CompareTag(ObjectTag)))
        {
            return;
        }
        if (callMethod == CallMethod.OnTriggerEnter && !Shown)
        {
            ShowOptionsPanel();
            Shown = true;
        }
    }
    void OnTriggerExit(Collider t)
    {
        if (!CheckPlaying())
        {
            return;
        }
        if (!(t.CompareTag(ObjectTag)))
        {
            return;
        }
        if (callMethod == CallMethod.OnTriggerExit && !Shown)
        {
            ShowOptionsPanel();
            Shown = true;
        }
    }
    void ShowOptionsPanel()
    {
        OptionsDisplay.Instance.ShowButtonOptionPanel(OptionPanelIndexToShow);
    }

    bool CheckPlaying()
    {
        if (!Application.isPlaying)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
