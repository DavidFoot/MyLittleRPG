using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.Control
{
    public class Patrol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnDrawGizmos()
    {

        for (int i = 0; i < transform.childCount; i++)
            {
                int j;
                
                j = GetNextIndexWaypoint(i);
                       
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(transform.GetChild(i).position, 0.25f);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
            //    Gizmos.DrawLineList(myPatrolPoints);
        }

        public int GetNextIndexWaypoint(int i)
        {
            if (i + 1 < transform.childCount)
            {
                return i + 1;
            }
            else
            {
                return 0;
            }
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }


}

