using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    public Vector2 Movement => movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // MOVE (Input System)
    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    // INTERACT (Input System)
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UIManager.Instance.TryInteract();
        }
    }

    // MOVE (Mobile on-screen joystick)
    public void SetVirtualMove(Vector2 direction)
    {
        movement = direction;
    }

    // INTERACT (Mobile on-screen button)
    public void MobileInteractPressed()
    {
        if (UIManager.Instance != null)
            UIManager.Instance.TryInteract();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }
}