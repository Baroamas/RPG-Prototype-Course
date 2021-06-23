using System;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Create New Weapon", order = 0)]
    public class ScObjWeapon : ScriptableObject
    {
        const string WEAPON_NAME = "EquippedWeapon";
        [SerializeField] GameObject _weaponPrefab = null;
        [SerializeField] AnimatorOverrideController _weaponAnimatorOverride = null;

        [SerializeField] float _weaponDamage; public float WeaponDamage { get { return _weaponDamage; } }
        [SerializeField] float _weaponRange; public float WeaponRange { get { return _weaponRange; } }
        [SerializeField] float _timeBetweenAttacks; public float TimeBetweenAttacks { get { return _timeBetweenAttacks; } }
        [SerializeField] bool _isRightHanded = true;

        [SerializeField] GameObject _projectilePrefab; public GameObject Projectile { get { return _projectilePrefab; } }

        public void Equip(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (_weaponPrefab != null)
            {
                DestroyOldWeapon(rightHand, leftHand);
                GameObject weapon;
                if (_isRightHanded)
                {
                    weapon = Instantiate(_weaponPrefab, rightHand);

                }
                else
                {
                    weapon = Instantiate(_weaponPrefab, leftHand);
                }
                weapon.name = WEAPON_NAME;
            }
            animator.runtimeAnimatorController = _weaponAnimatorOverride;
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(WEAPON_NAME);
            if (oldWeapon == null) oldWeapon = leftHand.Find(WEAPON_NAME);
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }


        //public void Hit(){}
        //public void Shoot(){} Depend on Game Mechanic. Ammo bisa unlimited atau tidak, bisa berubah2 atau tetap..

    }
}