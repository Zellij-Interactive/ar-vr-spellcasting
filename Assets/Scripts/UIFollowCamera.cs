using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    [SerializeField] public Transform targetCamera;
    [SerializeField] private Vector3 offset = new Vector3(0, -0.2f, 1.5f);
    [SerializeField] private bool faceCamera = true;

    private void LateUpdate()
    {
        if (targetCamera == null && Camera.main != null)
            targetCamera = Camera.main.transform;

        if (targetCamera == null) return;

        transform.position = targetCamera.position + targetCamera.rotation * offset;

        if (faceCamera)
        {
            transform.LookAt(targetCamera);
            transform.Rotate(0, 180f, 0); // So it faces the user, not away
        }
    }
}