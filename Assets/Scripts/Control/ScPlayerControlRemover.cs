using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Control
{

    public class ScPlayerControlRemover : MonoBehaviour
    {
        GameObject player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            ScFakePlayableDirector.Instance.onPlayed += DisableControl;
            ScFakePlayableDirector.Instance.onStopped += EnableControl;
        }
        void DisableControl()
        {
            player.GetComponent<ScActionScheduler>().CancelCurrentAction();
            player.GetComponent<ScPlayerController>().enabled = false;
        }

        void EnableControl()
        {
            player.GetComponent<ScPlayerController>().enabled = true;
        }
    }



}