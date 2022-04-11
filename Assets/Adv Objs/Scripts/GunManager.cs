using UnityEngine;
using TMPro;

public class GunManager : MonoBehaviour
{
    [SerializeField] Transform shotPoint;
    [SerializeField] ParticleSystem particle;
    [SerializeField] AudioClip Shotaudio, EmptyAudio;
    [SerializeField] AudioSource source;
    [SerializeField] TextMeshProUGUI amountCountText;
    [SerializeField] LayerMask EnemyMask, PlayerMask;
    [SerializeField] int amountCount = 20;

    int ShowingGizmos;
    float dist;
    RaycastHit myHit(LayerMask mask)
    {
        RaycastHit hit;
        Physics.BoxCast(shotPoint.position, shotPoint.localScale, shotPoint.forward, out hit, shotPoint.rotation, PlayerObject.Instance.shotLenght, mask);
        source.clip = Shotaudio;
        source.Play();
        particle.gameObject.SetActive(true);
        particle.Play();
        CustomInvoke.i.Invoke(1, () => { particle.gameObject.SetActive(false); });
        return hit;
    }
    public void ShootGunEnemy()
    {
        if (myHit(PlayerMask).collider)
        {
            myHit(PlayerMask).collider.GetComponent<PlayerObject>().DamagePlayer(5);
        }
    }
    public void ShootGunPlayer()
    {
        if (amountCount > 0)
        {
            if (myHit(EnemyMask).collider && myHit(EnemyMask).collider.GetComponent<AIagent>())
            {
                myHit(EnemyMask).collider.GetComponent<AIagent>().TakeDamage(35);
            }
            PlayerObject.Instance.Receive(shotPoint, myHit(EnemyMask).distance);
            amountCount--;
            amountCountText.text = amountCount.ToString();
            AiController.Instance.NotifyEnemies();
        }
        else
        {
            source.clip = EmptyAudio;
            source.Play();
        }
    }

    /* public void ReloadPistol(){
        amountCount+=20;
        amountCountText.text = amountCount.ToString();
    } */
    public void EnableAmoutpanel(bool val)
    {
        amountCountText.transform.parent.parent.gameObject.SetActive(val);
    }
}
