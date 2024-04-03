using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] int experiencePoints = 0;

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void GainExperiencePoints(int experience)
        {
            experiencePoints += experience;
        }
        public int GetPoints()
        {
            return experiencePoints;
        }
        public void RestoreState(object state)
        {
            experiencePoints = (int)state;
        }
    }
}

