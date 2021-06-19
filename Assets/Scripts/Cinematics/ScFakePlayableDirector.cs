using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScFakePlayableDirector : MonoBehaviour
{
    private static ScFakePlayableDirector _instance;
    public static ScFakePlayableDirector Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScFakePlayableDirector>();
            }
            return _instance;
        }

    }
    public event System.Action onPlayed;
    public event System.Action onStopped;

    public void OnPlayedEvent()
    {
        onPlayed?.Invoke();
    }
    public void OnStoppedEvent()
    {
        onStopped?.Invoke();
    }
    public void Play()
    {
        OnPlayedEvent();
        Invoke("OnStoppedEvent",3);

    }
}
