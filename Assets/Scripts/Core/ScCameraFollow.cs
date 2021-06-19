using UnityEngine;
using System.Collections;

namespace RPG.Core
{
    public class ScCameraFollow : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] float _lerpSpeed;
        Vector3 _defaultCameraPosition;
        Vector3 _relativeCameraPosition;
        Vector3 _offset;

        private void Start()
        {
            transform.position = _target.position;
            _defaultCameraPosition = Camera.main.transform.localPosition;
            _relativeCameraPosition = _defaultCameraPosition;

            _offset = _defaultCameraPosition * 0.2f;

        }
        void Update()
        {

            if (Input.mouseScrollDelta.y != 0)
            {
                StopCoroutine(CameraAnimation());
                _relativeCameraPosition = _relativeCameraPosition - _offset * Input.mouseScrollDelta.y;
                StartCoroutine(CameraAnimation());
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                StopCoroutine(CameraAnimation());
                _relativeCameraPosition = _defaultCameraPosition;
                StartCoroutine(CameraAnimation());
            }

        }
        void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, _lerpSpeed * Time.deltaTime);
        }

        private IEnumerator CameraAnimation()
        {   yield return null;
            while (Camera.main.transform.localPosition != _relativeCameraPosition)
            {
                Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, _relativeCameraPosition, 10 * Time.deltaTime);
                yield return null;
            }
        }
    }
}