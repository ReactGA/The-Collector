using UnityEngine;
using System.Collections.Generic;
using Dreamteck.Splines;

public class AiController : MonoBehaviour
{
    public static AiController Instance;
    [SerializeField] SplineComputer[] splines;
    //[SerializeField] GameObject[] Goals;
    [SerializeField] GameObject EnemyPrefab;
    //To count the  number of enemies each spline has spawned
    Dictionary<SplineComputer, int> spwanDict = new Dictionary<SplineComputer, int>();
    [HideInInspector] public List<GameObject> enemiesList = new List<GameObject>();

    //int hadGoal;
    private void Awake()
    {
        Instance = this;
    }
    public void StartGame()
    {
        for (var i = 0; i < splines.Length; i++)
        {
            var e = SpawnEnemy(splines[i].GetPoint(0).position);
            SetEnemy(e, splines[i]);
            enemiesList.Add(e);
            spwanDict.Add(splines[i], 1);
            /* if (i == 0)
            {
                e.GetComponent<AIagent>().otherPrefab = Goals[hadGoal];
                hadGoal += 1;
            } */
        }
    }
    public void RemoveEnemyInList(GameObject g)
    {
        if(enemiesList.Contains(g))
            enemiesList.Remove(g);
    }
    public void RespawnAnother(SplineComputer spl)
    {
        CustomInvoke.i.Invoke(9, () =>
        {
            if (spwanDict[spl] < 3)
            {
                var e = SpawnEnemy(spl.GetPoint(0).position);
                SetEnemy(e, spl);
                enemiesList.Add(e);

                //Incrementing the number of enemies the spline has spawned
                spwanDict[spl] += 1;
            }
        });

    }
    public void NotifyEnemies()
    {
        var playerPos = PlayerObject.Instance.transform.position;
        foreach (var e in enemiesList)
        {
            if (Vector3.Distance(e.transform.position, playerPos) < PlayerObject.Instance.shotLenght)
            {
                e.GetComponent<AIagent>().SetPlayerTarget();
            }
        }
    }
    void SetEnemy(GameObject Enemy, SplineComputer spl)
    {
        var Splinefollow = Enemy.GetComponent<SplineFollower>();
        Splinefollow.spline = spl;
        //Splinefollow.follow = true;
    }
    GameObject SpawnEnemy(Vector3 pos)
    {
        pos += new Vector3(0.5f, 0, 0.5f);
        var obj = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        return obj;
    }
}
