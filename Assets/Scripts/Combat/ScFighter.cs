using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class ScFighter : MonoBehaviour, IAction
    {
        [SerializeField] float _weaponRange = 2f;
        [SerializeField] float _timeBetweenAttacks = 1f;
        [SerializeField] float _weaponDamage = 10f;
        float _relativeDistance;
        float _timeSinceLastAttack = 0;
        ScHealth _target;


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
            if (_timeSinceLastAttack > _timeBetweenAttacks)
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
            _target?.TakeDamage(_weaponDamage);
        }

        private bool GetIsOutRange()
        {
            return Vector3.Distance(_target.transform.position, transform.position) > _weaponRange;
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
    }
}