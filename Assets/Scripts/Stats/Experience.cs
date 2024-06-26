using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0f;

        public event Action onExperienceGained;

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void GainExperiencePoints(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }
        public float GetPoints()
        {
            return experiencePoints;
        }
        public void RestoreState(object state)
        {
            experiencePoints = (int)state;
        }
    }
}

