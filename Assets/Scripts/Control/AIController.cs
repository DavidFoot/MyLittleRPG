using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
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

        [SerializeField]  float suspicionTime = 3f;
        [SerializeField]  float pauseBetweenWaypoint = 1.5f;
        [SerializeField]  float waypointDistanceTolerance = 1.5f;
        [SerializeField]  float patrolSpeedFraction = 0.3f;
        [SerializeField]  float chaseSpeedFraction = 0.5f;
        [SerializeField]  Patrol patrolPath;
        int patrolWaypointIndex = 0;

        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceWaypoint = Mathf.Infinity;


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
            if (GetComponent<Health>().IsDead()) { return; }
            if (IsInAttackRange() && fighter.CanAttack(player))
            {
                AttackBehavior();
                timeSinceLastSawPlayer = 0;
            }
            else if (timeSinceLastSawPlayer <= suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = originPosition;
            if(patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();

            }
            if(timeSinceWaypoint > pauseBetweenWaypoint)
            {
                fighter.GetComponent<Mover>().MoveTo(nextPosition, patrolSpeedFraction);
            }
        }

        private Vector3 GetCurrentWaypoint()
        {

            return patrolPath.GetWaypoint(patrolWaypointIndex);
        }

        private void CycleWaypoint()
        {
            patrolWaypointIndex = patrolPath.GetNextIndexWaypoint(patrolWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(GetCurrentWaypoint(), transform.position);
            if (distanceToWaypoint < waypointDistanceTolerance) { timeSinceWaypoint = 0; return true; }
            return false;
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            fighter.GetComponent<Mover>().MoveTo(player.transform.position, chaseSpeedFraction);
            // GetComponent<NavMeshAgent>().destination = player.transform.position;
            fighter.Attack(player);
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }


}


