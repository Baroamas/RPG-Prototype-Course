using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class ScPortal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D
        }


        [Header("Destination")]
        [SerializeField] string _destinationName = "0";
        [SerializeField] DestinationIdentifier _destinationPortalIndex;
        public Transform SpawnPosition { get; private set; }

        [Header("Fader")]
        ScFader fader;
        [SerializeField] float _fadeOutTime = 1f;
        [SerializeField] float _fadeInTime = 0.5f;
        [SerializeField] float _fadeWaitTime = 0.5f;
        private void Awake()
        {
            SpawnPosition = transform.GetChild(0);

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && _destinationName != "0")
            {
                Debug.Log("I'm Entering");
                StartCoroutine(LoadScene());
            }
        }

        private IEnumerator LoadScene()
        {
            DontDestroyOnLoad(gameObject);

            fader = GameObject.FindObjectOfType<ScFader>();
            yield return fader.FadeOut(_fadeOutTime);
            //ScSavingWrapper.Instance.Save();

            yield return SceneManager.LoadSceneAsync(_destinationName);

            // ScSavingWrapper.Instance.Load();

            SetPlayerSpawn(GetPortal());
            //ScSavingWrapper.Instance.Save();

            yield return new WaitForSeconds(_fadeInTime);
            yield return fader.FadeIn(_fadeWaitTime);

            Destroy(gameObject);
        }


        private ScPortal GetPortal()
        {
            foreach (ScPortal portal in FindObjectsOfType<ScPortal>())
            {
                if (portal._destinationPortalIndex == this._destinationPortalIndex && portal != this)
                {
                    return portal;
                }
            }
            return null;
        }

        private void SetPlayerSpawn(ScPortal otherPortal)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            player.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(otherPortal.SpawnPosition.position);
            player.rotation = otherPortal.SpawnPosition.rotation;
        }


    }
}