using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class ScFighter : MonoBehaviour, IAction
    {
        [SerializeField] Transform _rightHandTransform = null;
        [SerializeField] Transform _leftHandTransform = null;
        [SerializeField] ScObjWeapon _defaultWeapon = null;
        [SerializeField] ScArrow _arrow;

        ScObjWeapon _currentWeapon = null;

        float _relativeDistance;
        float _timeSinceLastAttack = 0;
        ScHealth _target;

        private void Start()
        {
            EquipWeapon(_defaultWeapon);
        }
        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (_target == null) return;
            if (_target.IsDead) return;

            if (GetIsOutRange())
            {
                GetComponent<ScMover>().MoveTo(_target.transform.position);
            }
            else
            {
                GetComponent<ScMover>().CancelAction();

                AttackBehaviour();
            }

        }

        internal bool CanAttack(GameObject target)
        {
            if (target == null) return false;
            return !target.GetComponent<ScHealth>().IsDead;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ScActionScheduler>().StartAction(this);
            _target = combatTarget.GetComponent<ScHealth>();
            //_target ada == update jalan -> AttackBehaviour()

        }
        private void AttackBehaviour()
        {
            if (_timeSinceLastAttack > _currentWeapon.TimeBetweenAttacks)
            {
                GetComponent<ScMover>().RotateToward(_target.transform.position);
                TriggerAttackAnimation();
                _timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttackAnimation()
        {
            GetComponent<Animator>().ResetTrigger("cancelAttack");//ada bug dimana ini tertrigger, jadi direset sebelum melakukan attack
            //this will trigger the Hit() event;
            GetComponent<Animator>().SetTrigger("attack");
        }

        //AnimationEvent
        void Hit()
        {
            _target?.TakeDamage(_currentWeapon.WeaponDamage);
        }

        void Shoot()
        {
            if (_target == null) return;
            ScArrow arrow = Instantiate(_arrow, _leftHandTransform.position, Quaternion.identity);
            arrow.SetTarget(_target);
            arrow.Damage = _currentWeapon.WeaponDamage;
            //Simpan Arrow pada player karna arrow cuma satu.. kalau kagak mungkin bakal ada SetArrow();
        }

        void CastMagic()
        {   
            if (_target == null) return;

            ScMagicProjectile magic = Instantiate(_currentWeapon.Projectile, _leftHandTransform.position, Quaternion.identity).GetComponent<ScMagicProjectile>();
            magic.SetTarget(_target);
            magic.Damage = _currentWeapon.WeaponDamage;
        }


        private bool GetIsOutRange()
        {
            return Vector3.Distance(_target.transform.position, transform.position) > _currentWeapon.WeaponRange;
        }

        public void CancelAction()
        {
            //GetComponent<ScMover>().CancelAction(); Uncomment untuk stop moving bila perlu
            CancelAttackAnimation();
            _target = null;
            GetComponent<ScMover>().CancelAction();

        }

        private void CancelAttackAnimation()
        {
            GetComponent<Animator>().SetTrigger("cancelAttack");
            GetComponent<Animator>().ResetTrigger("attack");
        }

        public void EquipWeapon(ScObjWeapon weapon)
        {
            _currentWeapon = weapon;

            Animator animator = GetComponent<Animator>();

            weapon.Equip(_rightHandTransform, _leftHandTransform, animator);
        }

    }
}