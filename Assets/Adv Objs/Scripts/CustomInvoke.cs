using UnityEngine;
using System.Collections;

public class CustomInvoke : MonoBehaviour
{
    public static CustomInvoke i;
    public delegate void Method();
    private void Awake()
    {
        i = this;
    }
    public void Invoke(float delay, Method MethodtoCall)
    {
        StartCoroutine(InvokeCorotine(delay, MethodtoCall));
    }
    IEnumerator InvokeCorotine(float delay, Method MethodToCall)
    {
        yield return new WaitForSeconds(delay);
        MethodToCall();

    }
}
