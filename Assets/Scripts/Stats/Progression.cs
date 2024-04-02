using UnityEngine;
namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "MyLittleRPG/Stats/new Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] CharacterClassProgression[] characterClassesProgression = null;
        

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (CharacterClassProgression itemProgression in characterClassesProgression) 
            {
                if(itemProgression.characterClass == characterClass)
                {
                    return itemProgression.health[level - 1];
                }
            }
            return 0;
        }
        [System.Serializable]
        public class CharacterClassProgression
        {
            [SerializeField] public CharacterClass characterClass;
            [SerializeField] public float[] health ;
        }
    }
}

