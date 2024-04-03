using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using UnityEngine;
namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] public float health = 100f;
        bool isDead = false;

        private void Start()
        {
            health = GetComponent<BaseStats>().GetStat(Stat.Health) ;
        }

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
        public float GetHealthPercentage()
        {
            return 100 * (health / GetComponent<BaseStats>().GetStat(Stat.Health));
        }
        public float GetMaxHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        public void TakingDamage(GameObject instigator , float damage)
        {
            if (health > 0)
            {
                health = Mathf.Max(health - damage,0f);
                print("Vie restante : " + health);
            }
            if (health == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if( experience == null )
            {
                return;
            }
            experience.GainExperiencePoints(GetComponent<BaseStats>().GetStat(Stat.ExperiencePoints));
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

