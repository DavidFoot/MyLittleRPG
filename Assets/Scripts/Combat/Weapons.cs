using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapons", menuName = "MyLittleRPG/Weapons/Make new Weapon", order = 0)]
    public class Weapons : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float weaponRange = 2f;

        public float WeaponDamage { get => weaponDamage; private set => weaponDamage = value; }
        public float WeaponRange { get => weaponRange; private set => weaponRange = value; }

        public void Spawn(Transform handPosition, Animator animator)
        {
            if(equippedPrefab != null)
            {
                Instantiate(equippedPrefab, handPosition);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            
        }
    }
}