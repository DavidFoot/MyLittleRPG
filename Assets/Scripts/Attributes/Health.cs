using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using UnityEngine;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        float health = -1f;
        bool isDead = false;
        [SerializeField] float regenHealthPercentageOnLevelUp = 70f;

        BaseStats stats = null;
        private void Awake()
        {
            stats = GetComponent<BaseStats>();
        }
        private void Start()
        {            
            if (health < 0)
            {
                health = stats.GetStat(Stat.Health);
            }
        }
        private void OnEnable()
        {
            stats.onLevelUp += RegenHealth;
        }
        private void OnDisable()
        {
            stats.onLevelUp -= RegenHealth;
        }
        private void RegenHealth()
        {
            float healthPointsToRegen = GetMaxHealth() * (regenHealthPercentageOnLevelUp/100);
            health = MathF.Max(health, healthPointsToRegen);
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
            if (health <= 0)
            {
                Die();
            }

        }
        public float GetPercentage()
        {
            return 100 * (health / GetComponent<BaseStats>().GetStat(Stat.Health));
        }
        public float GetMaxHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        public float GetHealth()
        {
            return health;
        }
        public void TakingDamage(GameObject instigator, float damage)
        {
            print(this.name + " take : " + damage + " damage");
            if (health > 0)
            {
                health = Mathf.Max(health - damage, 0f);
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
            if (experience == null)
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
