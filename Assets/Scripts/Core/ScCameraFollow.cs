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
        Vector2 _startPoint;
        float _startRotation;
        Vector3 _offset;

        private void Start()
        {
            transform.position = _target.position;
            _defaultCameraPosition = Camera.main.transform.localPosition;
            _relativeCameraPosition = _defaultCameraPosition;

            _offset = _defaultCameraPosition * 0.2f;

        }
        void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, _lerpSpeed * Time.deltaTime);
        }
        void Update()
        {
            ZoomCamera();
            RotateCamera();

        }

        private void RotateCamera()
        {
            if (Input.GetMouseButtonDown(2))
            {
                _startPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Debug.Log(Input.mousePosition);
                Debug.Log((_startPoint));
                //Cursor.lockState = CursorLockMode.Locked;
                // Invoke("CenteredCursor", 0.2f);
                Cursor.visible = false;
            }
            else if ((Input.GetMouseButton(2)))// && Cursor.lockState == CursorLockMode.None)
            {
                float i = Camera.main.ScreenToViewportPoint(Input.mousePosition).x - _startPoint.x;

                if (Mathf.Abs(i) < 0.01f) return;
                //i = (i > 10f) ? 1 : -1;

                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + i * 1100f * Time.deltaTime, 0);

                 _startPoint.x += ( Camera.main.ScreenToViewportPoint(Input.mousePosition).x - _startPoint.x) * Time.deltaTime *2;

            }
            if (Input.GetMouseButtonUp(2))
            {
                Cursor.visible = true;
            }
        }

        private void ZoomCamera()
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

        private void CenteredCursor()
        {
            Cursor.lockState = CursorLockMode.None;
        }


        private IEnumerator CameraAnimation()
        {
            yield return null;
            while (Camera.main.transform.localPosition != _relativeCameraPosition)
            {
                Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, _relativeCameraPosition, 10 * Time.deltaTime);
                yield return null;
            }
        }
    }
}