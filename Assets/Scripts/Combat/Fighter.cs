using RPG.Movement;
using RPG.Core;
using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttack = 2f;          
        [SerializeField] Transform rightHand = null;
        [SerializeField] Transform leftHand = null;
        
        [SerializeField] Weapons defaultWeapon = null;
        [SerializeField] Weapons currentWeapon = null;
        

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }
        public void EquipWeapon(Weapons weapon)
        {
            currentWeapon = weapon;
            currentWeapon.Spawn(rightHand, leftHand, GetComponent<Animator>());        
            
        }
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if(target == null || target.IsDead() || GetComponent<Health>().IsDead()) {
                return;
            }
            if (target != null)
            {
                bool notInRange = Vector3.Distance(target.transform.position, GetComponent<Transform>().position) > currentWeapon.WeaponRange;
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

        public void Shoot()
        {
            Hit();
        }
        public void Hit()
        {
            if (target == null) return;
            if (currentWeapon.HasProjectile())
            {
                Projectile projectile = currentWeapon.LaunchProjectile(rightHand,leftHand,target);
            }
            else
            {
                target.TakingDamage(currentWeapon.WeaponDamage);
            }
        }
    }
}

