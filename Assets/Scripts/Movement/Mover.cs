using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover :MonoBehaviour, IAction
    {
        // Start is called before the first frame update
        [SerializeField] float maxSpeed = 6f;


        NavMeshAgent navMeshAgent;
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);

        }
        public void MoveTo(Vector3 destination, float speedFraction)
        {
           
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        // Update is called once per frame
        void Update()
        {
            navMeshAgent.enabled = !GetComponent<Health>().IsDead();
            UpdateAnimation();
        }
        private void UpdateAnimation()
        {
            Vector3 veloce = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(veloce);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }
    }

}
