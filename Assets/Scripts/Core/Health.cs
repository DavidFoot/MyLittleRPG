using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] public float health = 100f;
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }
        public void TakingDamage(float damage)
        {
            if (health > 0)
            {
                health = Mathf.Max(health - damage,0f);
                print("Vie restante : " + health);
            }
            if (health == 0)
            {
                Dead();
            }
        }
        private void Dead()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }

}

