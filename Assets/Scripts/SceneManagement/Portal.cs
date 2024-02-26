using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad;
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] Transform spawnPoint;
        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }
        private IEnumerator Transition()
        {
            print("Enter in a new scene");
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            // Trouver comment apparaitre sur le spawnpoint du portail suivant
            Portal destinationPortal = GetDestinationPortal();
            Debug.Log("Destination : " + destinationPortal.destination);
            // Placer le player sur le spawnPoint du portal
            UpdatePlayer(destinationPortal);
            print("scene loaded");
            Destroy(gameObject);
        }
        private void UpdatePlayer(Portal portal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = portal.spawnPoint.transform.position;
            player.transform.rotation = portal.spawnPoint.transform.rotation; 
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
        private Portal GetDestinationPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != this.destination) continue;
                return portal;
            }
            return null;
        }
    }

}

