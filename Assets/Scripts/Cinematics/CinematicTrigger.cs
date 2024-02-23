using RPG.Control;
using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool animationPlayed = false;
        public void OnTriggerEnter(Collider other)
        {
            if (!animationPlayed && other.gameObject.tag == "Player" ) 
            {              
                GetComponent<PlayableDirector>().Play();
                animationPlayed = true;
            }
            
        }
    }

}

