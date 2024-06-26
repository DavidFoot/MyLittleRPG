using RPG.Attributes;
using RPG.Stats;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapons", menuName = "MyLittleRPG/Weapons/Make new Weapon", order = 0)]
    public class Weapons : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float weaponPercentageModifier = 0f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        const string weaponName = "weapon";
        public float WeaponDamage { get => weaponDamage; private set => weaponDamage = value; }
        public float WeaponPercentageModifier { get => weaponPercentageModifier; private set => weaponPercentageModifier = value; }
        public float WeaponRange { get => weaponRange; private set => weaponRange = value; }

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);
            if(equippedPrefab != null)
            {
                Transform handSelected = GetHandTransform(rightHand, leftHand);
                GameObject weaponInstance = Instantiate(equippedPrefab, handSelected);
                weaponInstance.name = weaponName;
            }
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
            
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null) {
                oldWeapon = leftHand.Find(weaponName);
                if(oldWeapon == null)
                {
                    return;
                }
            }
            oldWeapon.name = "ToDestroy";
            Destroy(oldWeapon.gameObject);
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }
        public Projectile LaunchProjectile(Transform leftHand, Transform rightHand, Health target, GameObject instigator, float damage) { 
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position,Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, damage);
            return projectileInstance;
        }
        private Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            Transform handSelected = leftHand;
            if (isRightHanded) { handSelected = rightHand; }

            return handSelected;
        }
    }
}