using RPG.Combat;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
namespace RPG.Control
{
    public class AICOntroller : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        GameObject player;
        Fighter fighter;
        [SerializeField]  float endChaseSpeed = 0.25f;

        Vector3 originPosition;
        public void Start ()
        {
            player = GameObject.FindWithTag("Player");
            fighter = gameObject.GetComponent<Fighter>();

            originPosition = transform.position;
        }
        public bool IsInAttackRange()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        }
        public void Update()
        {
            if (IsInAttackRange() && fighter.CanAttack(player))
            {
                print(name + "Est en vie et peut attaquer");
                GetComponent<NavMeshAgent>().destination = player.transform.position;
                fighter.Attack(player); 
            }
            else
            {
                if (!IsInAttackRange())
                {
                    fighter.Cancel();
                    if (Vector3.Distance(transform.position, originPosition) > 1 && !IsInAttackRange())
                    {
                        Vector3.MoveTowards(transform.position, originPosition, endChaseSpeed * Time.deltaTime);
                        GetComponent<NavMeshAgent>().destination = originPosition;
                    }
                }

            }
        }
    }


}


