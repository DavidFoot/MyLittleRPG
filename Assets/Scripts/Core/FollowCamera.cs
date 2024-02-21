using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace RPG.Core.Camera
{   
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform targetPlayer;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.transform.position = targetPlayer.position;
        }
    }
}

