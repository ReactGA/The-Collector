using UnityEngine;

[ExecuteInEditMode]
public class MyUITween : MonoBehaviour
{
	[Tooltip("Toggle to generate new animation")]
	public bool AutoGenerateSizeByKey;
	[SerializeField] Vector2 StartSize,EndSize;
    public enum EaseType{In,InOut,Out}
	public enum EaseMethod{Back,Bounce,Circ,Cubic,Elastic,Expo,Quad,Quart,Qunt,Sine,Shake,Spring}
	[Header("Animation Settings")]
    public EaseType easeType = EaseType.Out;
    public EaseMethod easeMethod= EaseMethod.Elastic;
    LeanTweenType myTweenType;
	public float TweenTime = 1f;
	[SerializeField]bool AnimateOnEnable = true;
	[HideInInspector][SerializeField] Vector2 StartPos,EndPos,StartScale,EndScale;
	public bool ShowPreviewButton = true;
	[HideInInspector]public bool useDefaultPopAnim_forButtons;
	[HideInInspector]public float Scalefactor = 1.1f;
	
		
    void Start()
    {
	    PerfomSettings();
    }
	
	void OnEnable()
	{
		PerfomSettings();
	}
	
	void PerfomSettings(){
		SetUpComp();
		SetTweenType();
		if(AnimateOnEnable && Application.isPlaying){
			Invoke("PlayAnimation",0.15f);
		}
	}
    void SetUpComp(){
        
        if(transform.childCount > 0)
            for (int i = 0; i < transform.childCount; i++)
            {
                var rect = transform.GetChild(i).GetComponent<RectTransform>();
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.pivot = Vector2.one * 0.5f;
            }
    }
    void SetTweenType()
    {
        
        #region EaseSetting
        
        if(easeType == EaseType.In){
            switch (easeMethod)
            {
                case EaseMethod.Back:
                myTweenType =  LeanTweenType.easeInBack;
                break;

                case EaseMethod.Bounce:
                myTweenType =  LeanTweenType.easeInBack;
                break;

                case EaseMethod.Circ:
                myTweenType =  LeanTweenType.easeInCirc;
                break;

                case EaseMethod.Cubic:
                myTweenType =  LeanTweenType.easeInCubic;
                break;

                case EaseMethod.Elastic:
                myTweenType =  LeanTweenType.easeInElastic;
                break;

                case EaseMethod.Expo:
                myTweenType =  LeanTweenType.easeInExpo;
	                break;
                
                case EaseMethod.Quad:
	                myTweenType =  LeanTweenType.easeInQuad;
	                break;
	                
                case EaseMethod.Quart:
	                myTweenType =  LeanTweenType.easeInQuart;
	                break;
	                
                case EaseMethod.Qunt:
	                myTweenType =  LeanTweenType.easeInQuint;
	                break;
	                
                case EaseMethod.Sine:
	                myTweenType =  LeanTweenType.easeInSine;
	                break;
	                
                case EaseMethod.Shake:
	                myTweenType =  LeanTweenType.easeShake;
	                break;
	                
                case EaseMethod.Spring:
	                myTweenType =  LeanTweenType.easeSpring;
	                break;

                default:
                break;
            }
        }else if(easeType == EaseType.InOut)
        {
            switch (easeMethod)
            {
                case EaseMethod.Back:
                myTweenType =  LeanTweenType.easeInOutBack;
                break;

                case EaseMethod.Bounce:
                myTweenType =  LeanTweenType.easeInOutBack;
                break;

                case EaseMethod.Circ:
                myTweenType =  LeanTweenType.easeInOutCirc;
                break;

                case EaseMethod.Cubic:
                myTweenType =  LeanTweenType.easeInOutCubic;
                break;

                case EaseMethod.Elastic:
                myTweenType =  LeanTweenType.easeInOutElastic;
                break;

                case EaseMethod.Expo:
                myTweenType =  LeanTweenType.easeInOutExpo;
	                break;
                case EaseMethod.Quad:
	                myTweenType =  LeanTweenType.easeInOutQuad;
	                break;
	                
                case EaseMethod.Quart:
	                myTweenType =  LeanTweenType.easeInOutQuart;
	                break;
	                
                case EaseMethod.Qunt:
	                myTweenType =  LeanTweenType.easeInOutQuint;
	                break;
	                
                case EaseMethod.Sine:
	                myTweenType =  LeanTweenType.easeInOutSine;
	                break;
	                
                case EaseMethod.Shake:
	                myTweenType =  LeanTweenType.easeShake;
	                break;
	                
                case EaseMethod.Spring:
	                myTweenType =  LeanTweenType.easeSpring;
	                break;
                
                default:
                break;
            }
        }else if(easeType == EaseType.Out)
        {
            switch (easeMethod)
            {
                case EaseMethod.Back:
                myTweenType =  LeanTweenType.easeOutBack;
                break;

                case EaseMethod.Bounce:
                myTweenType =  LeanTweenType.easeOutBack;
                break;

                case EaseMethod.Circ:
                myTweenType =  LeanTweenType.easeOutCirc;
                break;

                case EaseMethod.Cubic:
                myTweenType =  LeanTweenType.easeOutCubic;
                break;

                case EaseMethod.Elastic:
                myTweenType =  LeanTweenType.easeOutElastic;
                break;

                case EaseMethod.Expo:
                myTweenType =  LeanTweenType.easeOutExpo;
	            break;
                
                case EaseMethod.Quad:
	            myTweenType =  LeanTweenType.easeOutQuad;
	            break;
	                
                case EaseMethod.Quart:
	            myTweenType =  LeanTweenType.easeOutQuart;
	            break;
	                
                case EaseMethod.Qunt:
	            myTweenType =  LeanTweenType.easeOutQuint;
	            break;
	                
                case EaseMethod.Sine:
	            myTweenType =  LeanTweenType.easeOutSine;
	            break;
	                
                case EaseMethod.Shake:
	            myTweenType =  LeanTweenType.easeShake;
	            break;
	                
                case EaseMethod.Spring:
	            myTweenType =  LeanTweenType.easeSpring;
	            break;
                
                default:
                break;
            }
        }

        #endregion

        //Anchor the Child UIs
        
    }

    public void KeyStart(){
        StartPos = GetComponent<RectTransform>().localPosition;
        StartSize = GetComponent<RectTransform>().sizeDelta;
        StartScale = GetComponent<RectTransform>().localScale;
    }
    public void KeyEnd(){
        EndPos = GetComponent<RectTransform>().localPosition;
        EndSize = GetComponent<RectTransform>().sizeDelta;
        EndScale = GetComponent<RectTransform>().localScale;
    }

    public void FinishKey(){
        AutoGenerateSizeByKey = false;
    }
    //AnimationLogic
    
    public void AnimateOut()
    {
        SetTweenType();
        var myrect = GetComponent<RectTransform>();
        myrect.localPosition = StartPos;
        myrect.sizeDelta = StartSize;
        myrect.localScale = StartScale;

        LeanTween.move(myrect,EndPos,TweenTime).setEase(myTweenType);
        LeanTween.size(myrect,EndSize,TweenTime).setEase(myTweenType);
        LeanTween.scale(gameObject,EndScale,TweenTime).setEase(myTweenType);
    }

    public void AnimateIn()
    {
        SetTweenType();
        var myrect = GetComponent<RectTransform>();
        myrect.localPosition = EndPos;
        myrect.sizeDelta = EndSize;
        myrect.localScale = EndScale;

        LeanTween.move(myrect,StartPos,TweenTime).setEase(myTweenType);
        LeanTween.size(myrect,StartSize,TweenTime).setEase(myTweenType);
        LeanTween.scale(gameObject,StartScale,TweenTime).setEase(myTweenType);
    }
    
	public void AnimateInOut()
	{
		SetTweenType();
		var myrect = GetComponent<RectTransform>();
		myrect.localPosition = StartPos;
		myrect.sizeDelta = StartSize;
		myrect.localScale = StartScale;

		LeanTween.move(myrect,EndPos,TweenTime).setEase(myTweenType).setLoopPingPong(1);
		LeanTween.size(myrect,EndSize,TweenTime).setEase(myTweenType).setLoopPingPong(1);
		LeanTween.scale(gameObject,EndScale,TweenTime).setEase(myTweenType).setLoopPingPong(1);
	}
    
    
	public void PlayAnimation(){
		if (easeType == MyUITween.EaseType.In)
		{
			AnimateIn();
		}
		else if (easeType == MyUITween.EaseType.Out)
		{
			AnimateOut();
		}else if (easeType == MyUITween.EaseType.InOut)
		{
			AnimateInOut();
		}
	}
	
	
#if UNITY_EDITOR

	void OnValidate()
	{
		if(Application.isPlaying){
			PlayAnimation();
		}
	}
#endif


}
