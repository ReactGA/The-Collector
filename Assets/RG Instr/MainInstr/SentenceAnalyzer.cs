using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SentenceAnalyzer : MonoBehaviour
{
    public Text text;
    void Start()
    {
        Debug.Log(text.text.ToCharArray().Length);
    }

    //208 chars but last must not be longer than 8 chars + space = 9 chars

    void checkWords(){
        var allchars = text.text.ToCharArray();
        List<char> Dchars = new List<char>(); 
        if(text.text.ToCharArray().Length > 208){
            if(allchars[207] != ' '){
                var spcIndex = 0;
                for (int i = 207; i < 10; i--)
                {
                    if(allchars[i] == ' '){
                        spcIndex = i;
                    } 
                }

                if(207 - spcIndex > 8){
                    
                }
                
            }
            

            for (int i = 208; i < allchars.Length; i++)
            {
                Dchars.Add(allchars[i]);
            }
        }
    }
}
