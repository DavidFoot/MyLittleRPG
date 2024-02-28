using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapons", menuName = "MyLittleRPG/Weapons/Make new Weapon", order = 0)]
    public class Weapons : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject weaponPrefab = null;

        public void Spawn(Transform handPosition, Animator animator)
        {
            Instantiate(weaponPrefab, handPosition);
            animator.runtimeAnimatorController = animatorOverride;
        }
    }
}