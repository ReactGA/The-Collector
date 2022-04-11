using UnityEngine;

public class PointerTween : MonoBehaviour
{
    [SerializeField]float translation = 2,Twtime = 2,Repetdelay = 5;
    float delay;
    private void Update() {
        if(delay <= 0){
            Animate();
            delay = Repetdelay;
        }else{
            delay -= Time.deltaTime;
        }
    }
    void Animate()
    {
        LeanTween.scale(transform.gameObject,transform.localScale * 0.5f,Twtime).setLoopPingPong(1);
    }
}
