using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DentedPixel;

public class UITween : MonoBehaviour
{
	public Transform Mybutton;
    

    
    void Update()
    {
	    Mybutton.GetComponent<Button>().onClick.AddListener(()=>{
	    	LeanTween.size(Mybutton.GetComponent<RectTransform>(), Mybutton.GetComponent<RectTransform>().sizeDelta * 1.1f, 0.5f).setEase(LeanTweenType.easeOutBounce);
	    });
    }
}
