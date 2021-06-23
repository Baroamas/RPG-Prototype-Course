using System;
using System.Collections;
//using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class ScSavingWrapperBackUp : MonoBehaviour
    {
        private static ScSavingWrapperBackUp _instance;
        public static ScSavingWrapperBackUp Instance
        {
            get
            {
                return _instance;
            }
        }
        const string defaultSaveFile = "save";
        private void Awake()
        {
            _instance = this;
        }
        // IEnumerator Start()
        // {
        //     ScFader fader= FindObjectOfType<ScFader>();
        //     fader.SetFaderAlpha(1);
        //  yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        //     StartCoroutine(fader.FadeIn(0.5f));
        // }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

        }

        public void Save()
        {
    //        GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
       //     GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

    }
}