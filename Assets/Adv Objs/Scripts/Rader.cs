using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class Rader : MonoBehaviour
{
    [SerializeField] Transform playerPointonMap, ImagePrefab, Player;
    public List<Transform> Targets;
    [HideInInspector] public List<Transform> TargetPoints;
    [SerializeField] float MaxDistonMap, MaxDistinGame, increment;

    IEnumerator Start()
    {
        yield return null;
        for (var i = 0; i < Targets.Count; i++)
        {
            var g = Instantiate(ImagePrefab);
            g.gameObject.SetActive(true);
            g.parent = playerPointonMap.parent;
            g.transform.position = Vector3.zero;
            g.transform.localEulerAngles = Vector3.zero;
            g.transform.localScale = Vector3.one;
            TargetPoints.Add(g);
        }
    }
    void Update()
    {
        playerPointonMap.transform.localEulerAngles = new Vector3(0, 0, -Player.localEulerAngles.y);
        if (TargetPoints.Count == Targets.Count) { SetTargets(); };
    }

    void SetTargets()
    {
        for (var i = 0; i < Targets.Count; i++)
        {
            var dir = (Targets[i].position - Player.position).normalized;
            var dist = Vector3.Distance(Targets[i].position, Player.position);
            dir = new Vector3(dir.x, 0, dir.z);
            if (dist > MaxDistinGame)
            {
                TargetPoints[i].localPosition = new Vector3(dir.x * MaxDistonMap, dir.z * MaxDistonMap, 0);
            }
            else
            {
                TargetPoints[i].localPosition = new Vector3(dir.x * MaxDistonMap * increment, dir.z * MaxDistonMap * increment, 0);
            }
            TargetPoints[i].GetComponentInChildren<TextMeshProUGUI>().text = dist.ToString("0.0") + " m";
        }
    }
}
