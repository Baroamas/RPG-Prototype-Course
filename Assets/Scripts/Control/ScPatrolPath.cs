using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScPatrolPath : MonoBehaviour
{

    void Start()
    {

    }

    private void OnDrawGizmos()

    {
        for (int i = 0; i < GetPatrolCount(); i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.4f);
            Gizmos.DrawLine(transform.GetChild(i).position, GetWaypoint(i));
        }

    }

    public int GetPatrolCount()
    {
        return transform.childCount;
    }

    public int GetNextIndex(int i)
    {
        if (i == transform.childCount - 1) return 0;
        return i + 1;
    }

    public Vector3 GetWaypoint(int index)
    {
        return transform.GetChild(GetNextIndex(index)).position;
    }


}
