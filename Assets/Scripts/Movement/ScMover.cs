using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement
{
    public class ScMover : MonoBehaviour, IAction, ISaveable
    {
        NavMeshAgent _navMeshAgent;

        private bool _arrived = false;
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            CheckArrival();
            UpdateAnimator();
        }
        private void CheckArrival() //reduce animation jittery
        {
            if (!_arrived)
                if (_navMeshAgent.remainingDistance < 0.01)
                {
                    _arrived = true;
                    CancelAction();
                }
        }

        public void StartToMove(Vector3 newPosition)
        {
            GetComponent<ScActionScheduler>().StartAction(this);
            MoveTo(newPosition);
        }
        public void MoveTo(Vector3 newPosition)
        {
            _arrived = false;
            _navMeshAgent.destination = newPosition;
            _navMeshAgent.isStopped = false;
        }
        public void RotateToward(Vector3 target)
        {
            transform.LookAt(target);

            Vector3 eulerAngles = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(0, eulerAngles.y, 0);
        }

        public void SetMoveSpeed(float speed)
        {
            _navMeshAgent.speed = speed;
        }

        public void CancelAction()
        {
            _navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            GetComponent<Animator>().SetFloat("forwardSpeed", localVelocity.z);

            #region Debug/Comment Berkaitan dengan global to local velocity
            //Debug.Log("Velocity" + (localVelocity.z));
            //Simplenya: velocity pada  navmesh itu global, jadi perspectivenya perubahan posisi object terhadap world space..
            //sedangkan jika di inverse transform direction, merupakan perubahan velocity berdasarkan subject local karakter..
            //kucoba pake magnitude(velocity) bisa2 aja... cuma lebih masuk akal jika diubah ke local..
            //karna ibarat, andai dia terbang ke atas(y)... magnitudenya tetaplah besar. sedangkan kalau local..
            //magnitudenya berdasarkan posisi, x y z nya...
            #endregion
        }

        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);

            return data;
            //return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            MoverSaveData data = (MoverSaveData)state;
            GetComponent<NavMeshAgent>().Warp(data.position.ToVector());
            transform.eulerAngles = data.rotation.ToVector();


            //SerializableVector3 position = (SerializableVector3)state;
            //GetComponent<NavMeshAgent>().Warp(position.ToVector());

        }
    }

}