using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ScPersistentObjectSpawner : MonoBehaviour
    {
        static bool hasSpawn = false;
        [SerializeField] GameObject _prefabPersistentObject;
        private void Awake()
        {
            if (!hasSpawn)
            {
                hasSpawn = true;
                GameObject persistentObject = Instantiate(_prefabPersistentObject);
                DontDestroyOnLoad(persistentObject);
            }
        }
    }

}