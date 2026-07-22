using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;

    private float lastMoveX;
    private float lastMoveY = -1f; // varsayýlan olarak aþaðý baksýn istersen

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 move = playerController.Movement;

        bool isMoving = move.sqrMagnitude > 0.01f;

        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            animator.SetFloat("MoveX", move.x);
            animator.SetFloat("MoveY", move.y);

            lastMoveX = move.x;
            lastMoveY = move.y;

            if (move.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (move.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            // Idle blend tree son bakýlan yönde kalsýn
            animator.SetFloat("MoveX", lastMoveX);
            animator.SetFloat("MoveY", lastMoveY);
        }
    }
}