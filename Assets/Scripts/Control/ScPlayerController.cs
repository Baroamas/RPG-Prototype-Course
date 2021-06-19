using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class ScPlayerController : MonoBehaviour
    {
        private ScHealth _health;
        private void Start()
        {
            _health = GetComponent<ScHealth>();
        }
        private void Update()
        {
            if (!_health.IsDead && Input.GetMouseButton(1))
            {//bisa letakkan input getmouse di method interact with movement, tapi raycast pada interactwithcombat jalan terus

                if (InteractWithCombat()) return;

                if (InteractWithMovement()) return;
                InteractWithVoid();
            }
        }
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                ScCombatTarget target = hit.transform.GetComponent<ScCombatTarget>();

                if (!GetComponent<ScFighter>().CanAttack(target?.gameObject)) continue;

                if (Input.GetMouseButtonDown(1))
                {
                    GetComponent<ScFighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;//Debug.DrawRay(_lastRay.origin, _lastRay.direction * 100, Color.green, 20);

            if (Physics.Raycast(GetMouseRay(), out hit))
            {
                GetComponent<ScMover>().StartToMove(hit.point);
                return true;
            }
            return false;

        }
        private void InteractWithVoid()
        {
            if (Input.GetMouseButtonDown(1))
            {
                print("Nothing Here");
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}