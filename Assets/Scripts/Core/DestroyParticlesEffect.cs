using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPC.Core
{
    public class DestroyParticlesEffect : MonoBehaviour
    {
        private void Update()
        {
            if(!GetComponent<ParticleSystem>().IsAlive()) 
            {
                Destroy(gameObject);
            }
        }
    }
}

