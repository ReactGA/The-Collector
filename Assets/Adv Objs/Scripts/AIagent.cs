using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.AI;
using Dreamteck.Splines;
using System.Collections;

public class AIagent : MonoBehaviour
{
    [SerializeField] Transform RayPoint, RotPoint, GunHeld;
    //[SerializeField]GameObject magazinePrefab;
    [SerializeField] float rayLenght = 3, attackDistance = 4, escapeDist = 8;
    [SerializeField] LayerMask playerLayer;
    bool SingleCall, Running, attacking;
    Transform playerTarget;
    Animator anim;
    [SerializeField] int health = 100;
    SplineFollower spl;
    //[HideInInspector] public GameObject otherPrefab;
    bool isDead;


    void Start()
    {
        anim = GetComponent<Animator>();
        spl = GetComponent<SplineFollower>();
        FollowPath();
    }
    public void SetPlayerTarget()
    {
        playerTarget = PlayerObject.Instance.transform;
    }
    void FixedUpdate()
    {
        if (!isDead)
        {
            if (playerTarget != null)
            {
                Engage(playerTarget);
            }
            else
            {
                DebugVisuals();
                //mmm();
                foreach (var h in HitTarget())
                {
                    if (h.collider)
                    {
                        playerTarget = h.collider.transform;
                    }
                }
            }
        }
    }
    void mmm(){
        float v = GetComponent<NavMeshAgent>().velocity.magnitude;
        if (v >= 1.48f ) {
            anim.Play("Walking");
        }     
    }
    Vector3[] Points;
    void FollowPath()
    {
        var spln = spl.spline;
        Points = new Vector3[spln.GetPoints().Length];
        for (var i = 0; i < Points.Length; i++)
        {
            Points[i] = spln.GetPoint(i).position;
        }
        StartCoroutine(MovePos(Points));
    }
    bool forward;
    int index = 0;
    IEnumerator MovePos(Vector3[] points)
    {
        while (transform.position != points[index])
        {
            if (isDead || playerTarget != null) { yield break; }

            if (index == points.Length - 1)
            {
                anim.Play("Idle");
                yield return new WaitForSeconds(Random.Range(5, 10));
                anim.Play("Walking");
                forward = false;
            }
            else if (index == 0)
            {
                anim.Play("Idle");
                yield return new WaitForSeconds(Random.Range(5, 10));
                anim.Play("Walking");
                forward = true;
            }
            anim.Play("Walking");
            GetComponent<NavMeshAgent>().SetDestination(points[index]);
            if (Vector3.Distance(transform.position, Points[index]) < 0.2f)
            {
                if (forward) { index++; } else { index--; }
                StartCoroutine(MovePos(Points));
            }
            yield return null;
        }
    }
    Vector3 vect;
    void Engage(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) > escapeDist)
        {
            StopAllCoroutines();
            playerTarget = null;
        }
        if (!SingleCall) { vect = (transform.position - target.position).normalized; SingleCall = true; }


        var dist = Vector3.Distance(transform.position, target.position);
        if (Mathf.Abs(dist - attackDistance) > 0.2f)
        {
            GetComponent<NavMeshAgent>().SetDestination(target.position + (vect * attackDistance));
            anim.speed = 2;
            anim.Play("RunAttack");
            attacking = false;
        }
        else
        {
            if (!attacking)
            {
                anim.speed = 1;
                anim.Play("Attack");
                StartCoroutine(BeginAttacking());
                attacking = true;
            }

        }
        transform.LookAt(target);
    }
    Quaternion rot(Transform t)
    {
        var Rot = -(RotPoint.position - t.position).normalized;
        return Quaternion.LookRotation(Rot, Vector3.up);
    }
    IEnumerator BeginAttacking()
    {
        while (true)
        {
            if (isDead) { yield break; }
            if (!TakingDamage)
            {
                GunHeld.GetComponent<GunManager>().ShootGunEnemy();
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
    RaycastHit[] HitTarget()
    {
        RaycastHit[] h = new RaycastHit[ray().Length];
        for (var i = 0; i < h.Length; i++)
        {
            Physics.Raycast(ray()[i], out h[i], rayLenght, playerLayer);
        }
        return h;
    }

    Ray[] ray()
    {
        var Rays = new Ray[5];
        Rays[0] = new Ray(RayPoint.position, RayPoint.forward);
        Rays[1] = new Ray(RayPoint.position, RayPoint.forward + RayPoint.right);
        Rays[2] = new Ray(RayPoint.position, RayPoint.forward - RayPoint.right);
        Rays[3] = new Ray(RayPoint.position, -RayPoint.right);
        Rays[4] = new Ray(RayPoint.position, RayPoint.right);
        return Rays;
    }

    private void DebugVisuals()
    {
        Debug.DrawRay(ray()[0].origin, ray()[0].direction * rayLenght, Color.red);
        Debug.DrawRay(ray()[1].origin, ray()[1].direction * rayLenght, Color.red);
        Debug.DrawRay(ray()[2].origin, ray()[2].direction * rayLenght, Color.red);
        Debug.DrawRay(ray()[3].origin, ray()[3].direction * rayLenght, Color.red);
        Debug.DrawRay(ray()[4].origin, ray()[4].direction * rayLenght, Color.red);
    }
    bool TakingDamage;
    public void TakeDamage(int DamageAmount)
    {
        //if (isDead) { return; }
        Debug.Log("Enemy taking Damege!");
        health -= DamageAmount;
        anim.Play("Hit");
        if (playerTarget == null)
        {
            Engage(PlayerObject.Instance.transform);
        }
        TakingDamage = true;
        CustomInvoke.i.Invoke(0.7f, () => { TakingDamage = false; });
        if (health <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        //if (isDead) { return; }
        isDead = true;
        GetComponent<NavMeshAgent>().enabled = false;
        AiController.Instance.RemoveEnemyInList(gameObject);
        AiController.Instance.RespawnAnother(spl.spline);
        anim.applyRootMotion = true;

        anim.Play("Dead");
        DropObtainable();
        Destroy(gameObject, 4);
    }
    public void SetDeadRb()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
    void DropObtainable()
    {
        GunHeld.parent = null;
        GunHeld.GetComponent<Rigidbody>().isKinematic = false;
        GunHeld.gameObject.layer = 9;
        GunHeld.GetComponent<XRGrabInteractable>().interactionLayerMask = -1;
        //Instantiate(magazinePrefab, transform.position + Vector3.up * 1.2f, Quaternion.identity);
        
    }
}
