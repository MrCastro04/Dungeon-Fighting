using Uitility;
using UnityEngine;

namespace UI
{
    public class Billboard : MonoBehaviour
    {
        private Camera _cameraCmp;

        private void Awake()
        {
            _cameraCmp = GameObject.FindGameObjectWithTag(Constants.CAMERA_TAG)
                .GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            Vector3 directionOffset = transform.position + _cameraCmp.transform.forward;

            transform.LookAt(directionOffset);
        }
    }
}