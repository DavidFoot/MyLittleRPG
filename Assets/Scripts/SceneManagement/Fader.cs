using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup cg;

        public void Start()
        {
            cg = GetComponent<CanvasGroup>();
        }
        public IEnumerator FadeIn(float fadeInTime)
        {
            cg.alpha = 0;
            while (cg.alpha < 1)
            {
                
                cg.alpha  += Time.deltaTime/ fadeInTime;
                yield return null;
            }
        }
        public IEnumerator FadeOut(float fadeOutTime)
        {
            cg.alpha = 1;
            while (cg.alpha > 0)
            {

                cg.alpha -= Time.deltaTime / fadeOutTime;
                yield return null;
            }
        }


    }
}

