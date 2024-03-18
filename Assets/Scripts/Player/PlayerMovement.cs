using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] Vector2 movementDirection;

    PlayerActions actions;
    PlayerData playerData;
    PlayerAnimation playerAnimation;
    Rigidbody2D rb;

    private void Awake()
    {
        actions = new PlayerActions();
        playerData = GetComponent<PlayerData>();
        playerAnimation = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        ReadMovement();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if (playerData.PlayerStats.CurrentHealth <= 0)
            return;

        rb.MovePosition(rb.position + movementDirection * (movementSpeed * Time.fixedDeltaTime));
    }

    void ReadMovement()
    {
        movementDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;
        
        
        if (movementDirection == Vector2.zero)
        {
            playerAnimation.HandleMoveBoolAnimation(false);
            return;
        }

        playerAnimation.HandleMoveBoolAnimation(true);
        playerAnimation.HandleMovingAnimation(movementDirection);
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}