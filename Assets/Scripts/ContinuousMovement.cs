using UnityEngine;
using UnityEngine.InputSystem;

public class SampleMovement : MonoBehaviour
{
    public float speed = 1.5f;
    public CharacterController characterController;
    public Transform headTransform;
    public InputActionProperty moveInput;


    // Update is called once per frame
    void Update()
    {
        Vector2 input = moveInput.action.ReadValue<Vector2>();
        Vector3 direction = headTransform.right * input.x + headTransform.forward * input.y;
        direction.y = 0; // don't move up/down
        characterController.Move(direction * speed * Time.deltaTime);
    }
}
