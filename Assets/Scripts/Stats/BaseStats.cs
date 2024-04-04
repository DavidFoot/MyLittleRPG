using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifiers = false;
        public event Action onLevelUp;
        int currentLevel = 0;
        Experience experience;
        private void Awake()
        {
            experience = GetComponent<Experience>();
        }
        public void Start()
        {
            currentLevel = CalculateLevel();
        }
        private void OnEnable()
        {
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }
        private void OnDisable()
        {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }
        }
        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel != currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();

            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, this.transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifiers(stat)) * (1+(GetPercentageModifiers(stat)/100));
        }


        private int GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }
        private float GetPercentageModifiers(Stat stat)
        {
            if (!shouldUseModifiers) return 0;
            float sum = 0;
            foreach (var provider in GetComponents<IModifierProvider>())
            {
                if (provider != null)
                {
                    foreach (float percentageBonus in provider.GetPercentageModifiers(stat))
                    {
                        sum += percentageBonus;
                    }
                }
            }
            return sum;
        }

        private float GetAdditiveModifiers(Stat stat)
        {
            if (!shouldUseModifiers) return 0;
            float sum = 0;
            foreach (var provider in GetComponents<IModifierProvider>())
            {
                if (provider != null)
                {
                    foreach( float additiveValue  in provider.GetAdditiveModifiers(stat))
                    {
                        sum += additiveValue;
                    }
                }
            }
            return sum;
        }

        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        private int CalculateLevel() {
            Experience experience = GetComponent<Experience>();
            if (experience == null) { return startingLevel; }

            float experiencePoints = experience.GetPoints();
            int levelBeforeLastLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for(int level = 1; level <= levelBeforeLastLevel; level++)
            {
                int neededXPToLevel = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if(experiencePoints <= neededXPToLevel) {  return level; }
            }

            return levelBeforeLastLevel+1;
        }
    }

}

