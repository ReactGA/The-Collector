using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System.Collections;

public class PlayerObject : MonoBehaviour, IAttachment
{
    public static PlayerObject Instance;
    public float shotLenght = 20;
    [SerializeField] LayerMask EnemyMask;
    [SerializeField] int health = 100;
    [SerializeField] GameObject DeadPanel, HandPanel;
    [SerializeField] TextMeshProUGUI HealthText;
    [SerializeField] AiController AIctrl;
    [SerializeField] GunManager gm;
    [SerializeField] XRController left, right;
    //[SerializeField]GunManager myPistol;

    private void Awake()
    {
        Instance = this;
        HealthText.text = health.ToString();
    }
    private void Start()
    {
        CustomInvoke.i.Invoke(1.2f, () => { InstructionClass.options.ShowInstruction(0, 1); });
    }
    public void AttachmentCallNext(int i) { }
    public void AttachmentCallCont(int i)
    {
        if (i == 1) { AIctrl.StartGame(); }
    }
    public void ShootGun(Transform shotPoint)
    {
        var ray = new Ray(shotPoint.position, shotPoint.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, shotLenght, EnemyMask);
        if (hit.collider)
        {
            hit.collider.GetComponent<AIagent>().TakeDamage(35);
        }
    }
    /* public void Reload(){
        myPistol.ReloadPistol();
    } */
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gm.ShootGunPlayer();
        }
    }
    Transform shotPoint;
    float dist;
    int ShowingGizmos;
    public void Receive(Transform ShotPoint, float Dist)
    {
        shotPoint = ShotPoint;
        dist = Dist;
        ShowingGizmos = 10;
        StartCoroutine(Decrement());
    }
    IEnumerator Decrement(){
        while(ShowingGizmos>0) {
            ShowingGizmos --;
            yield return new WaitForSeconds(1);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (ShowingGizmos > 0)
        {
            Gizmos.DrawRay(shotPoint.position, shotPoint.forward * dist);
            Gizmos.DrawWireCube(shotPoint.position + shotPoint.forward * dist, shotPoint.localScale);
        }
    }
    public void DamagePlayer(int DamageAmount)
    {
        health -= DamageAmount;
        HealthText.text = health.ToString();
        left.SendHapticImpulse(0.7f, 0.3f);
        right.SendHapticImpulse(0.7f, 0.3f);
        if (health <= 0)
        {
            HealthText.text = "0";
            Dead();
        }
    }
    void Dead()
    {
        DeadPanel.SetActive(true);
        CustomInvoke.i.Invoke(3, () => { SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex); });
    }
    bool handPanelActive;
    public void EnableHandPanel()
    {
        handPanelActive = !handPanelActive;
        HandPanel.SetActive(handPanelActive);
    }

    //Ingrdent Tracker
    [HideInInspector] public bool HasHoldIng;
    [HideInInspector] public int NumOfIngOn;
    public TextMeshProUGUI IngredentText;
    [HideInInspector] public GameObject IngToKeep;
    [SerializeField] GameObject WonCanvas;
    [SerializeField] Rader rd;
    bool hasKept;

    public void KeepIngr(GameObject c)
    {
        if (hasKept) { return; }
        NumOfIngOn += 1;
        IngredentText.text = NumOfIngOn.ToString() + "/5";
        IngToKeep = c.gameObject;
        if (NumOfIngOn == 5)
        {
            WinGame();
        }

        if (rd.Targets.Contains(IngToKeep.transform))
        {
            var index = rd.Targets.IndexOf(IngToKeep.transform);
            rd.TargetPoints[index].gameObject.SetActive(false);
            rd.TargetPoints.RemoveAt(index);
            rd.Targets.Remove(IngToKeep.transform);
        }

        CustomInvoke.i.Invoke(1, () =>
        {
            IngToKeep.GetComponent<MeshRenderer>().enabled = false;
            IngToKeep.gameObject.layer = 14;
            Destroy(IngToKeep.GetComponent<XRGrabInteractable>());
        });
        hasKept = true;
        CustomInvoke.i.Invoke(5, () => { hasKept = false; });
    }

    public void WinGame()
    {
        WonCanvas.SetActive(true);
        CustomInvoke.i.Invoke(3, () => { SceneManager.LoadSceneAsync(3); });
    }
}
