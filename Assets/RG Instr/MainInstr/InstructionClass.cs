using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class InstructionClass : MonoBehaviour
{
    public enum InstructionType { InstructionObject, CustomInstruction }
    InstructionType instructionType;
    public static InstructionClass options;
    [Header("Panel UIs Reference")]
    public GameObject Instructionpanel;
    public GameObject Continuebutton, NextButton, PrevButton;
    Button nextButtonUI, prevButtonUI, continueButtonUI;
    [HideInInspector] public int startingIndex, CurrentIndex, indextoreach;
    public bool Typewords = true, EditorSimualtor = false;
    [SerializeField] KeyCode PrevKeyCode, NextKeyCode, ContinueKeyCode;
    [HideInInspector] bool isInstructing = false, Autonext;
    [SerializeField] TextMeshProUGUI textToDispaly;
    [Header("Panel Transform Settings (Deprecated)")]
    public float offsetFromCamera = 0.7f;
    public Vector3 Scale;
    [Header("Instruction Object To Display")]
    [SerializeField] InstructionObject instructionObject;
    [HideInInspector] public Vector3 OffsetFromCamera;
    string[] StringToDisplay;
    static bool IsSetup;
    float autoNextTimer = 2;
    [Header("Attacment Object for its script")]
    [SerializeField] GameObject Attachment;
    [SerializeField] DisplayInfrontCamera display;
    [SerializeField] SoundManger soundManger;

    void Awake()
    {
        options = this;
        Instructionpanel.SetActive(false);
        /* for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        } */
    }
    void Start()
    {
        if (instructionObject != null)
        {
            SetUpInstructions();
        }

        offsetFromCamera = OffsetFromCamera.z;
        display.GetComponent<DisplayInfrontCamera>();
    }

    public void SetUpInstructions(/*bool CfS = false,*/InstructionObject instObj = null, bool NextAuto = false, bool typeWords = true, float autonextDuration = 2, InstructionType Type = InstructionType.InstructionObject, string[] customInstruction = null)
    {
        IsSetup = true;
        if (!(instObj == null))
        {
            instructionObject = instObj;
        }
        if (Scale != Vector3.zero)
        {
            transform.localScale = Scale;
        }

        Autonext = NextAuto;
        autoNextTimer = autonextDuration;
        /* if(CfS){
            Typewords = typeWords;
        } */


        if (Type == InstructionType.InstructionObject)
        {
            StringToDisplay = instructionObject.Instructions;
        }
        else
        {
            StringToDisplay = customInstruction;
        }
        Instructionpanel.SetActive(false);

    }

    public void ShowInstruction(int InstructionIndex, int StopIndex = 0)
    {
        if (StopIndex == 0)
        {
            StopIndex = InstructionIndex;
        }
        isInstructing = true;
        startingIndex = InstructionIndex;
        CurrentIndex = startingIndex;
        indextoreach = StopIndex;


        SetUp();
        Instructionpanel.SetActive(true);
        if (!NextButton.GetComponent<Button>() || !PrevButton.GetComponent<Button>() || !Continuebutton.GetComponent<Button>())
        {
            Debug.LogWarning("Error the Next,Previous or continue button object assigned does not have a button component");
            return;
        }

        nextButtonUI = NextButton.GetComponent<Button>();
        prevButtonUI = PrevButton.GetComponent<Button>();
        continueButtonUI = Continuebutton.GetComponent<Button>();

        nextButtonUI.onClick.RemoveAllListeners();
        nextButtonUI.onClick.AddListener(NextWord);

        prevButtonUI.onClick.RemoveAllListeners();
        prevButtonUI.onClick.AddListener(PrevWord);

        continueButtonUI.onClick.RemoveAllListeners();
        continueButtonUI.onClick.AddListener(ContinueOnClick);

        Continuebutton.SetActive(false);
        if (!Autonext)
        {
            if (CurrentIndex == indextoreach)
            {
                NextButton.SetActive(false);
                Continuebutton.SetActive(true);
            }
            else
            {
                NextButton.SetActive(true);
            }
            PrevButton.SetActive(false);
        }
        display.ShowInfrontOfCamera();

    }

    void Update()
    {
        if (isInstructing)
        {
            if (EditorSimualtor)
            {
                if (Input.GetKeyDown(PrevKeyCode))
                {
                    PrevWord();
                }
                else if (Input.GetKeyDown(NextKeyCode) && !Continuebutton.activeInHierarchy)
                {
                    NextWord();
                }
                else if (Input.GetKeyDown(ContinueKeyCode) && !NextButton.activeInHierarchy)
                {
                    ContinueOnClick();
                }
            }

            if (Autonext && !Typewords && !(CurrentIndex == indextoreach))
            {
                if (autoNextTimer <= 0)
                {
                    NextWord();
                    autoNextTimer = 2;
                }
                else
                {
                    autoNextTimer -= Time.deltaTime;
                }
            }
        }
    }

    /*  void setpos(bool rePosition = false){
         if(!Instructionpanel.activeInHierarchy || rePosition){
             transform.position = Camera.main.transform.position + Camera.main.transform.forward * offsetFromCamera;
         }
         Instructionpanel.SetActive(true);
     } */
    void SetUp()
    {
        StopAllCoroutines();
        textToDispaly.gameObject.SetActive(false);
        textToDispaly.gameObject.SetActive(true);
        //setpos();

        if (!Typewords)
        {
            textToDispaly.text = StringToDisplay[CurrentIndex];
        }
        else
        {
            StartCoroutine("Type");
        }

        if (soundManger != null)
        {
            soundManger.PlayInstrClip(CurrentIndex);
        }
    }

    #region type      
    IEnumerator Type()
    {
        textToDispaly.text = "";
        NextButton.SetActive(false);

        foreach (char letter in StringToDisplay[CurrentIndex].ToCharArray())
        {
            textToDispaly.text += letter;

            yield return new WaitForSeconds(0.000001f);
        }

        if (textToDispaly.text == StringToDisplay[CurrentIndex])
        {
            if (!Autonext)
            {
                if (!Continuebutton.activeInHierarchy) { NextButton.SetActive(true); }
            }
            else
            {
                yield return new WaitForSeconds(2);
                if (textToDispaly.text == StringToDisplay[CurrentIndex] && CurrentIndex != indextoreach)
                {
                    textToDispaly.text = "";
                    CurrentIndex += 1;
                    StartCoroutine("Type");
                }
                else if (CurrentIndex == indextoreach)
                {
                    //
                    NextButton.SetActive(false);
                    //
                    Continuebutton.SetActive(true);
                    StopCoroutine("Type");
                }
            }

        }

    }
    #endregion
    public void NextWord()
    {
        if (CurrentIndex == startingIndex)
        {
            PrevButton.SetActive(true);
        }
        CurrentIndex += 1;
        SetUp();
        if (CurrentIndex == indextoreach)
        {
            NextButton.SetActive(false);
            Continuebutton.SetActive(true);
        }
        if (Attachment != null)
        {
            UseAttamentScriptNext();
        }
    }
    public void PrevWord()
    {
        if (CurrentIndex == indextoreach)
        {
            NextButton.SetActive(true);
            Continuebutton.SetActive(false);
        }
        CurrentIndex -= 1;
        SetUp();
        if (CurrentIndex == startingIndex)
        {
            PrevButton.SetActive(false);
        }

    }
    public void ContinueOnClick()
    {
        if (soundManger != null) { soundManger.StopSound(); }
        if (Instructionpanel.GetComponent<MyUITween>())
        {
            MyUITween animClass = Instructionpanel.GetComponent<MyUITween>();
            animClass.easeType = MyUITween.EaseType.In;
            animClass.PlayAnimation();
            Invoke("DisableInstruction", animClass.TweenTime + 0.1f);
            return;
        }
        else
        {
            Instructionpanel.SetActive(false);
            isInstructing = false;
        }
        Continuebutton.SetActive(false);
        textToDispaly.text = "";
        if (Attachment != null)
        {
            UseAttamentScriptCont();
        }


    }

    void DisableInstruction()
    {

        Instructionpanel.SetActive(false);
        isInstructing = false;
    }

    void UseAttamentScriptCont()
    {
        var att = Attachment.GetComponent<IAttachment>();
        if (att != null)
        {
            att.AttachmentCallCont(CurrentIndex);
        }
    }

    void UseAttamentScriptNext()
    {
        var att = Attachment.GetComponent<IAttachment>();
        if (att != null)
        {
            att.AttachmentCallNext(CurrentIndex);
        }
    }
}

public interface IAttachment
{
    void AttachmentCallNext(int Index);
    void AttachmentCallCont(int Index);
}


