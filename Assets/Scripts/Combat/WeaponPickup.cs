using RPG.Combat;
using Unity.VisualScripting;
using UnityEngine;
namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapons weapon;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collider de l'arme");
            if (other.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}

