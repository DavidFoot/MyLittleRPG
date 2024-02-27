using RPG.Control;
using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour , ISaveable
    {
        bool animationPlayed = false;

        public object CaptureState()
        {
            return animationPlayed;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!animationPlayed && other.gameObject.tag == "Player" ) 
            {              
                GetComponent<PlayableDirector>().Play();
                animationPlayed = true;
            }
            
        }

        public void RestoreState(object state)
        {
            animationPlayed = (bool)state;
        }
    }

}

