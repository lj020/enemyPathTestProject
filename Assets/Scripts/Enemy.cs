using System.Collections.Generic;
using com.hats.enemyPaths;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private EnemyPath enemyPath;
    private int wayPointCount = 0;
    private List<Vector3> globalWayPoints = new List<Vector3>();

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyPath = GetComponent<EnemyPath>();
    }

    private void Start()
    {
        foreach (var wayPoint in enemyPath.WayPoints)
        {
            globalWayPoints.Add(transform.TransformPoint(wayPoint));
        }

        GotoNextPoint();
    }

    void Update()
    {
        if (wayPointCount < enemyPath.WayPoints.Count && !navMeshAgent.pathPending &&
            navMeshAgent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (enemyPath.WayPoints.Count == 0)
            return;

        navMeshAgent.destination = globalWayPoints[wayPointCount];

        wayPointCount += 1;
    }
}