using RPG.Combat;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapons weapon;
        [SerializeField] float respawnTime = 15f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(SpawnPickup(respawnTime));
            }
        }

        IEnumerator SpawnPickup(float respawnTime)
        {
            ShouldShow(false);
            yield return new WaitForSeconds(respawnTime);
            ShouldShow(true);
        }

        private void ShouldShow( bool active)
        {
            //GetComponentInChildren<Collider>().enabled = active;
            foreach( Transform t in transform)
            {
                t.GameObject().SetActive(active);
            }
        }
    }
}

