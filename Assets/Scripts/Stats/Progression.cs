using System;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "MyLittleRPG/Stats/new Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] CharacterClassProgression[] characterClassesProgression = null;

        Dictionary<CharacterClass, Dictionary<Stat, int[]>> progressionLookupDictionary = null;


        public int GetStat(Stat stat,CharacterClass characterClass, int level)
        {
            BuildLookupDictionary();
            int[] levels = progressionLookupDictionary[characterClass][stat];
            if (levels.Length < level) return 0;
            return levels[level-1];
        }
        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookupDictionary();
            int[] levels = progressionLookupDictionary[characterClass][stat];
            return levels.Length;
        }
        private void BuildLookupDictionary()
        {
            if (progressionLookupDictionary != null) return;
            progressionLookupDictionary = new Dictionary<CharacterClass, Dictionary<Stat, int[]>>();
            foreach (CharacterClassProgression itemClassProgression in characterClassesProgression)
            {
                Dictionary<Stat, int[]> statLookupDictionary = new Dictionary<Stat, int[]>();
                foreach (ProgressionStat stat in itemClassProgression.stats)
                {
                    statLookupDictionary[stat.stat] = stat.levels;
                }
                progressionLookupDictionary[itemClassProgression.characterClass] = statLookupDictionary;
            }
        }

        [System.Serializable]
        public class CharacterClassProgression
        {
            [SerializeField] public CharacterClass characterClass;
            [SerializeField] public ProgressionStat[] stats ;
        }
        [System.Serializable]
        public class ProgressionStat
        {
            [SerializeField] public Stat stat;
            [SerializeField] public int[] levels;
        }
    }
}

