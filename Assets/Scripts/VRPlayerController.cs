using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class VRPlayerMovement : MonoBehaviourPunCallbacks
{
    public float speed = 2.0f;
    private InputAction moveAction;

    private void OnEnable()
    {
        // Find the action asset and the action
        var inputActions = new XRInputActions();
        moveAction = inputActions.Player.Move;
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        Vector2 inputVector = moveAction.ReadValue<Vector2>();

        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;  // Ignore the y component to prevent moving up and down
        forward.Normalize();

        Vector3 right = Camera.main.transform.right;
        right.y = 0;  // Ignore the y component to prevent moving up and down
        right.Normalize();

        Vector3 moveDirection = forward * inputVector.y + right * inputVector.x;
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
