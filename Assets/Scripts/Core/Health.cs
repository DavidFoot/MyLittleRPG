using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;
namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] public float health = 100f;
        bool isDead = false;

        public object CaptureState()
        {
            return health;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void RestoreState(object state)
        {
            health = (float)state;
            if( health <= 0 )
            {
                Die();
            }

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
                Die();
            }
        }
        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }

}

