using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Control
{

    public class ScControlRemover : MonoBehaviour
    {
        GameObject player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            ScFakePlayableDirector.Instance.onPlayed += DisableControl; //subscribe ke event handler
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