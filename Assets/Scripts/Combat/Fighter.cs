using RPG.Movement;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField]
        float weaponRange = 2f;

        [SerializeField]
        float timeBetweenAttack = 2f;

        [SerializeField]
        float weaponDamage = 5f;      
        [SerializeField] Transform weaponPosition = null;
        [SerializeField] Weapons weapon = null;
        

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            SpawnWeapon();
        }
        private void SpawnWeapon()
        {
            if (weapon == null) return;     
            weapon.Spawn(weaponPosition,GetComponent<Animator>());        
            
        }
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if(target == null || target.IsDead() || GetComponent<Health>().IsDead()) {
                return;
            }
            if (target != null)
            {
                bool notInRange = Vector3.Distance(target.transform.position, GetComponent<Transform>().position) > weaponRange;
                if (notInRange)
                {
                    GetComponent<Mover>().MoveTo(target.transform.position,0.8f);
                    
                }
                else
                {
                    GetComponent<Mover>().Cancel();
                    AttackBehaviour();
                }
            }
        }
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null || combatTarget.GetComponent<Health>().IsDead()) { return false; }   
            return true;
        }
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack <= timeBetweenAttack) return;

            TriggerAttack();
            timeSinceLastAttack = 0;
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("CancelAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        public void Cancel()
        {
            target = null;
            GetComponent<Mover>().Cancel();
            StopAttack();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().SetTrigger("CancelAttack");
            GetComponent<Animator>().ResetTrigger("Attack");
        }

        public void Hit()
        {
            if (target == null) return;
            target.TakingDamage(weaponDamage);
        }
    }
}

