using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] Transform spawnPoint;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeOutTime = 1f;

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition()); 
            }
        }
        private IEnumerator Transition()
        {
            if (sceneToLoad < 0) {
                Debug.Log("No scene to load");
                yield break;
            }
            DontDestroyOnLoad(gameObject);
            Fader fader = FindFirstObjectByType<Fader>();
            yield return fader.FadeIn(fadeInTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            Portal destinationPortal = GetDestinationPortal();
            destinationPortal.GetComponent<BoxCollider>().enabled = false;
            UpdatePlayer(destinationPortal);
            yield return fader.FadeOut(fadeOutTime);
            Destroy(gameObject);
            destinationPortal.GetComponent<BoxCollider>().enabled = true;
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

