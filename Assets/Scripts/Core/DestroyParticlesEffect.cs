using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPC.Core
{
    public class DestroyParticlesEffect : MonoBehaviour
    {
        [SerializeField] GameObject targetToDestroy = null;
        private void Update()
        {
            if(!GetComponent<ParticleSystem>().IsAlive()) 
            {
                if(targetToDestroy != null)
                {
                    Destroy(targetToDestroy);
                }
                else
                {
                    Destroy(gameObject);
                }
                
            }
        }
    }
}

