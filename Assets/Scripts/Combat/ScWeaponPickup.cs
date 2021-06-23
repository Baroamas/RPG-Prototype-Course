using UnityEngine;

namespace RPG.Combat
{
    public class ScWeaponPickup : MonoBehaviour
    {
        [SerializeField] ScObjWeapon _weapon= null;

        private void Awake() {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<ScFighter>().EquipWeapon(_weapon);
                Destroy(gameObject);
            }
        }
    }
}