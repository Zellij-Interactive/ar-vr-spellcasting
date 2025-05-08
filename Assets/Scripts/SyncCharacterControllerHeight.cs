using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCharacterControllerHeight : MonoBehaviour
{
    public Transform xrCamera;
    public CharacterController characterController;

    void Update()
    {
        characterController.height = xrCamera.localPosition.y + 0.2f;
        characterController.center = new Vector3(xrCamera.localPosition.x, characterController.height / 2, xrCamera.localPosition.z);
    }
}
