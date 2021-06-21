using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class ScFader : MonoBehaviour
    {
        private CanvasGroup _fader;

        private void Awake()
        {
            _fader = GetComponent<CanvasGroup>();
        }
        private void Start()
        {
            //StartCoroutine(FadeOutFadeIn()); Testing Purpose
        }

        private IEnumerator FadeOutFadeIn()
        {
            yield return FadeOut(1f);
            yield return FadeIn(0.5f);
        }
        public IEnumerator FadeOut(float time)
        {
            _fader.alpha = 0;
            while (_fader.alpha < 1)
            {
                _fader.alpha += Time.deltaTime / time;
                yield return null;
            }
        }
        public IEnumerator FadeIn(float time)
        {
            _fader.alpha = 1;
            while (_fader.alpha > 0)
            {
                _fader.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }

        public void SetFaderAlpha(int value)
        {
            _fader.alpha = value;
        }
    }
}