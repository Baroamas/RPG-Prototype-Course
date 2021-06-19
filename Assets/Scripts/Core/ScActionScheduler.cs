using UnityEngine;

namespace RPG.Core
{
    public class ScActionScheduler : MonoBehaviour
    {
        IAction _currentAction;
        public void StartAction(IAction action)
        {
            if (_currentAction == action) return;
            if (_currentAction != null)
            {
                _currentAction.CancelAction();
            }
            _currentAction = action;
        }
        public void CancelCurrentAction(){
            StartAction(null);
        }

    }
}