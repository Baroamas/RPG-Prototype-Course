using RPG.Core;
using RPG.Combat;
using UnityEngine;
using RPG.Movement;

namespace RPG.Control
{
    public class ScAIController : MonoBehaviour
    {
        GameObject _aggressor;
        [SerializeField] float _chaseDistance = 8f;
        [SerializeField] float _suspicionTime = 3f;
        [SerializeField] float _dwellingTime = 3f;
        [Range(0f,10f)][SerializeField] float _patrolSpeed = 2.33f;
        [Range(0f,10f)][SerializeField] float _chaseSpeed = 5f;
        [SerializeField] ScPatrolPath _patrolPath;
        //temp
        Vector3 _guardPos;//defaultpos
        int _totalWaypoint;
        Vector3 _currentWaypoint;//position
        int _currentWaypointIndex = 0;

        ScMover _mover;
        ScFighter _fighter;
        ScHealth _health;

        float _timeSinceLastSawPlayer = Mathf.Infinity;
        float _dwellingCount = 0;
        private void Awake()
        {

        }
        private void Start()
        {
            _aggressor = GameObject.FindGameObjectWithTag("Player");
            _mover = GetComponent<ScMover>();
            _fighter = GetComponent<ScFighter>();
            _health = GetComponent<ScHealth>();

            _guardPos = transform.position;
            _currentWaypoint = _guardPos;

            _totalWaypoint = (_patrolPath == null) ? 0 : _patrolPath.GetPatrolCount();

        }
        private void Update()
        {
            if (_health.IsDead) return;

            if (_fighter.CanAttack(_aggressor) && InAtackRange())
            {
                AttackBehaviour();
            }
            else if (_timeSinceLastSawPlayer < _suspicionTime)
            {
                //_sussingCount += Time.deltaTime; //moreperformance
                SusBehaviour();
            }
            else
            {
                PatrolBehaviour();//CanAttack sangat menentukan else.. karna kalau target mati && masih di range.. tetap jalan
            }
            _timeSinceLastSawPlayer += Time.deltaTime;//moresense
        }

        private void AttackBehaviour()
        {   _mover.SetMoveSpeed(_chaseSpeed);
            _timeSinceLastSawPlayer = 0;
            _fighter.Attack(_aggressor);
        }
        private bool InAtackRange()
        {
            return Vector3.Distance(this.transform.position, _aggressor.transform.position) < _chaseDistance;
        }
        private void SusBehaviour()
        {
            GetComponent<ScActionScheduler>().CancelCurrentAction();
        }
        private void PatrolBehaviour()
        {
            _mover.SetMoveSpeed(_patrolSpeed);
            if (_totalWaypoint > 0)
            {
                if (AtWaypoint() && !IsDwelling())
                {
                    CycleWaypoint();
                    _dwellingCount = 0;
                    _dwellingTime = Random.Range(3, 6);//Hapus Boleh
                }
                _mover.StartToMove(_currentWaypoint);
            }
            else _mover.StartToMove(_guardPos);
        }

        private bool IsDwelling()
        {
            _dwellingCount += Time.deltaTime;
            return _dwellingCount < _dwellingTime;
        }

        private bool AtWaypoint()
        {
            return Vector3.Distance(transform.position, _currentWaypoint) < 1f;
        }
        private void CycleWaypoint()
        {
            _currentWaypointIndex = _patrolPath.GetNextIndex(_currentWaypointIndex);
            _currentWaypoint = _patrolPath.GetWaypoint(_currentWaypointIndex);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
    }
}