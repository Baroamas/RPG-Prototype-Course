using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{

    public class ScArrow : MonoBehaviour
    {
        [SerializeField] Transform _arrowModel;
        [SerializeField] TrailRenderer _trail;

        ScHealth _target;
        bool _hasTarget = false;
        Vector3 _targetOffset = Vector3.zero;

        float _damage = 0; public float Damage { set { _damage = value; } }

        void Update()
        {

            if (_hasTarget)
            {
                if (_target.IsDead)
                {
                    Destroy(gameObject);
                }
                transform.LookAt(GetAimLocation());
                transform.position = Vector3.MoveTowards(transform.position, GetAimLocation(), 20 * Time.deltaTime);
            }
        }

        private Vector3 GetAimLocation()
        {
            return _target.transform.position + _targetOffset;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<ScHealth>() == _target)
            {
                _target?.TakeDamage(_damage);
                _hasTarget = false;
                StopArrow();
            }
        }

        public void SetTarget(ScHealth target)
        {

            _target = target;
            _hasTarget = true;

            CapsuleCollider collider = _target.GetComponent<CapsuleCollider>();
            if (collider == null) return;

            _targetOffset = Vector3.up * (collider.height * 0.7f);
        }

        public void StopArrow()
        {
            _arrowModel.gameObject.SetActive(false);
            StartCoroutine(SlowTrailDisable());
        }

        IEnumerator SlowTrailDisable()
        {
            float rate = _trail.time / 15f;
            while (_trail.time > 0)
            {

                _trail.time -= rate;
                yield return null;
            }
            Destroy(gameObject);
        }
    }

}
