using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] XRDirectInteractor lH;
    [SerializeField] Material originalMatleft, originalMatright, MainCol, highlightcol, frenelmat;
    public GameObject RightControllerBody, RightTrigger, RightGrab, RightJoystick, LeftControllerBody, LeftTrigger, Leftgrab, leftJoystick;
    [SerializeField] GameObject LefthandPresence, RightHandPresence;
    public GameObject spanner, wrench, drill, drillbit, ThreeDbutton,SetResetCanvasButton, MvtOptions, TeleportArea, TdButtonArrow, Loadingpanel,DXRRig,TPray;
    float spannerCounter, wrenchCounter;
    bool holdingSpanner, holdingWrench, drillOnLeft, usingdrill,drillOnLeftCheck;
    [SerializeField] float drillrotspeed = 5;
    [SerializeField] AudioSource mainSource, otherSource;
    [SerializeField] AudioClip drillSound;


    #region ControllerHandler

    public void SwithHandPresence(bool left, HandPresence.ControllerToShow presence)
    {
        if (left)
        {
            LefthandPresence.GetComponent<HandPresence>().controllerToShow = presence;
            LefthandPresence.GetComponent<HandPresence>().SetUpControllerType();
        }
        else
        {
            RightHandPresence.GetComponent<HandPresence>().controllerToShow = presence;
            RightHandPresence.GetComponent<HandPresence>().SetUpControllerType();
        }

    }

    public void RecolorRight()
    {
        RightControllerBody.GetComponent<Renderer>().material = MainCol;
        foreach (Transform chld in RightControllerBody.transform)
        {
            chld.gameObject.GetComponent<Renderer>().material = MainCol;
        }
    }
    public void RecolorLeft()
    {
        LeftControllerBody.GetComponent<Renderer>().material = MainCol;
        foreach (Transform chld in LeftControllerBody.transform)
        {
            chld.gameObject.GetComponent<Renderer>().material = MainCol;
        }
    }
    public void highlightObject(GameObject Tohighlight, bool left)
    {
        if (left)
        {
            RecolorLeft();
            Tohighlight.GetComponent<Renderer>().material = highlightcol;
        }
        else
        {
            RecolorRight();
            Tohighlight.GetComponent<Renderer>().material = highlightcol;
        }
    }

    #endregion

    private void Start()
    {
        RecolorLeft();
        RecolorRight();
        highlightObject(RightTrigger, false);
        Invoke("SetBeginningInstr", 1);
    }

    void SetBeginningInstr()
    {
        InstructionClass.options.ShowInstruction(0, 2);
    }
    void CallInstruction()
    {
        InstructionClass.options.ShowInstruction(3, 4);
    }
    public void Fresnelhighlight(GameObject Tohiglight)
    {
        var mats = new Material[]{
            Tohiglight.GetComponent<Renderer>().material,
            frenelmat,
        };
        Tohiglight.GetComponent<Renderer>().materials = mats;
    }
    private void Update()
    {
        //Editor Simulation
        if (Input.GetKey(KeyCode.Z))
        {
            HeldSpanner();
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            DropSpanner();
        }
        if (Input.GetKey(KeyCode.X))
        {
            HeldWrench();
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            DropWrench();
        }
        if (Input.GetKeyDown(KeyCode.C)){
            drillOnLeft = true;
        }


        if (holdingSpanner)
        {
            spanner.GetComponentInChildren<TextMeshProUGUI>().text = spannerCounter.ToString("0");
            if (spannerCounter < 3)
            {
                spannerCounter += Time.deltaTime;
                
            }else if(spannerCounter >= 3){
                spannerCounter = 3;
            }

        }
        if (holdingWrench)
        {
            wrench.GetComponentInChildren<TextMeshProUGUI>().text = wrenchCounter.ToString("0");
            if (wrenchCounter < 3)
            {
                wrenchCounter += Time.deltaTime;
                
            }else{
                wrenchCounter = 3;
            }
        }
        if (spannerCounter >= 3 && wrenchCounter >= 3 && !Holdingtest)
        {
            CallInstruction();
            Holdingtest = true;
        }
        if (drillOnLeft && !drillOnLeftCheck)
        {
            InstructionClass.options.ShowInstruction(5);
            DisableTeleporter();
            Invokeinst6();
            drillOnLeft = false;
            drillOnLeftCheck = true;
        }

        if (usingdrill)
        {
            drillbit.transform.Rotate(0, 0, drillrotspeed * Time.deltaTime);
        }
    }
    void DisableTeleporter(){
        DXRRig.GetComponent<RayVisibilityManager>().HideTeleport = true;
        TPray.SetActive(false);
    }
    public void EnableTeleporter(){
        DXRRig.GetComponent<RayVisibilityManager>().HideTeleport = false;
    }
    public void Invokeinst6()
    {
        Invoke("CallNextInstr", 12);
    }
    void CallNextInstr()
    {
        TeleportArea.SetActive(true);
        //EnableTeleporter();
        InstructionClass.options.ShowInstruction(6);
    }
    /* public void Invokeinst8()
    {
        Invoke("CallNextInstr8", 8);
        
    } */
    public void CallNextInstr8()
    {
        TdButtonArrow.SetActive(true);
        InstructionClass.options.ShowInstruction(8);
        SwithHandPresence(false, HandPresence.ControllerToShow.handOnly);
        SwithHandPresence(true, HandPresence.ControllerToShow.handOnly);
        ThreeDbutton.SetActive(true);
    }
    bool Holdingtest;
    public void HeldSpanner()
    {
        holdingSpanner = true;
        spanner.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void DropSpanner()
    {
        holdingSpanner = false;
        spanner.transform.GetChild(0).gameObject.SetActive(false);
        if (spannerCounter < 3)
        {
            spannerCounter = 0;
        }
    }

    public void HeldWrench()
    {
        holdingWrench = true;
        wrench.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void DropWrench()
    {
        holdingWrench = false;
        wrench.transform.GetChild(0).gameObject.SetActive(false);
        if (wrenchCounter < 3)
        {
            wrenchCounter = 0;
        }
    }

    public void CheckHoldDrill()
    {
        if (lH.selectTarget.tag == "drill")
        {
            drillOnLeft = true;
        }
    }

    public void UseDrillEnter()
    {
        usingdrill = true;
        otherSource.clip = drillSound;
        otherSource.Play();
    }
    public void UseDrillExit()
    {
        usingdrill = false;
        otherSource.clip = null;
        otherSource.Stop();
    }

    public void Click3dButton()
    {
        InstructionClass.options.ShowInstruction(9);
        SetResetCanvasButton.SetActive(true);
        
    }
    public void InvokeinstLast(){
        Invoke("ShowLastInstr",2);
    }
    public void ShowLastInstr(){
        InstructionClass.options.ShowInstruction(10);
    }
    #region MvtOptions
         
    
    /* public void Choosemovemnt()
    {
        MvtOptions.SetActive(true);
    }

    public void Teleport()
    {
        LoadNextScene();
    }
    public void Joystick()
    {
        LoadNextScene();
    }
    public void Both()
    {
        LoadNextScene();
    } */

    #endregion
    public void LoadNextScene()
    {
        Loadingpanel.SetActive(true);
        SceneManager.LoadSceneAsync(2);
    }
}
