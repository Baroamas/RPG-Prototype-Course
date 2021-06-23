using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class ScSavingWrapper : MonoBehaviour
    {
        private const string _defaultFileName = "save01";

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<ScSavingSystem>().Save(_defaultFileName);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                GetComponent<ScSavingSystem>().Load(_defaultFileName);
            }
        }
    }

}