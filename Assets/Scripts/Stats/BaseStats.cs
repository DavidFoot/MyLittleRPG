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
        int currentLevel = 0;

        public void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
            
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel != currentLevel)
            {
                currentLevel = newLevel;
                print("Level Up!");
            }
        }

        public int GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }
        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        public int CalculateLevel() {
            Experience experience = GetComponent<Experience>();
            if (experience == null) { return startingLevel; }

            int experiencePoints = experience.GetPoints();
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

