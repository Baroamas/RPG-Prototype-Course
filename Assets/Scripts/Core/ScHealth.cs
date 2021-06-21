
using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    public class ScHealth : MonoBehaviour, ISaveable
    {
        [SerializeField] float _healthPoints = 100f;
        public bool IsDead { get; private set; } = false;

        public void TakeDamage(float damage)
        {
            _healthPoints = Mathf.Max(0, _healthPoints - damage);
            print("Ouchie!! " + _healthPoints);
            if (_healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (IsDead) return;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<RPG.Core.ScActionScheduler>().CancelCurrentAction();
            IsDead = true;
        }

        public object CaptureState()
        {
            return _healthPoints;
        }

        public void RestoreState(object state)
        {
            _healthPoints = (float)state;
            if (_healthPoints == 0)
            {
                Die();
            }
        }

    }
}