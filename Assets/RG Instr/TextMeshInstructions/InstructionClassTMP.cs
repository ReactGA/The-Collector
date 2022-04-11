using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class InstructionClassTMP : MonoBehaviour
{
    public static InstructionClassTMP options;
    public GameObject Instructionpanel, Continuebutton, NextButton, PrevButton;
    Button nextButtonUI, prevButtonUI, continueButtonUI;
    [HideInInspector] public int startingIndex, CurrentIndex, indextoreach;
    [HideInInspector] public bool Typewords, Autonext, isInstructing = false;
    [SerializeField]  TextMeshProUGUI textToDispalyTmPro;
    [SerializeField] InstructionObject instructionObject;
    public Vector3 Scale, OffsetFromCamera;
    string[] StringToDisplay;
    [HideInInspector] public float typingspeed = 0.01f;
    static bool IsSetup;
    float autoNextTimer = 2;
    void Awake()
    {

        options = this;

    }
    void Start()
    {
        SetUpInstructions();
        Typewords = true;
    }

    public void SetUpInstructions(InstructionObject instObj = null, bool NextAuto = false, float autonextDuration = 2)
    {
        IsSetup = true;
        if (!(instObj == null))
        {
            instructionObject = instObj;
        }
        Instructionpanel.SetActive(false);
        transform.localScale = Scale;
        Autonext = NextAuto;
        autoNextTimer = autonextDuration;
        StringToDisplay = instructionObject.Instructions;
        textToDispalyTmPro.gameObject.SetActive(false);
        Instructionpanel.SetActive(false);

    }

    public void ShowInstruction(int InstructionIndex, int StopIndex = 0)
    {
        if (StopIndex == 0)
        {
            StopIndex = InstructionIndex;
        }
        isInstructing = true;
        Instructionpanel.SetActive(true);
        startingIndex = InstructionIndex;
        startingIndex = CurrentIndex;
        indextoreach = StopIndex;
        SetUp();

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

    }

    void Update()
    {
        if (isInstructing)
        {
            transform.position = Camera.main.transform.position + OffsetFromCamera;
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
    void SetUp()
    {
        /* if (Application.platform == RuntimePlatform.Android)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 1.9f, transform.localPosition.z);
        } */

        transform.localEulerAngles = new Vector3(0, Camera.main.transform.localEulerAngles.y, 0);
        Instructionpanel.SetActive(true);

        if (!Typewords)
        {
            textToDispalyTmPro.text = StringToDisplay[CurrentIndex];
        }
        else
        {
            StartCoroutine("Type");
        }


    }

    #region type      
    IEnumerator Type()
    {
        
            textToDispalyTmPro.text = "";
            foreach (char letter in StringToDisplay[CurrentIndex].ToCharArray())
            {
                textToDispalyTmPro.text += letter;

                yield return new WaitForSeconds(typingspeed);
            }

            if (textToDispalyTmPro.text == StringToDisplay[CurrentIndex])
            {
                if (!Autonext)
                {
                    NextButton.SetActive(true);
                }
                else
                {
                    yield return new WaitForSeconds(2);
                    if (textToDispalyTmPro.text == StringToDisplay[CurrentIndex] && CurrentIndex != indextoreach)
                    {
                        textToDispalyTmPro.text = "";
                        CurrentIndex += 1;
                        StartCoroutine("Type");
                    }
                    else if (CurrentIndex == indextoreach)
                    {
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
        Continuebutton.SetActive(false);
        textToDispalyTmPro.text = "";
        Instructionpanel.SetActive(false);
        transform.localPosition = new Vector3(0, 0, 0.5f);
        transform.localEulerAngles = Vector3.zero;
        Instructionpanel.SetActive(false);
        isInstructing = false;
    }

}


