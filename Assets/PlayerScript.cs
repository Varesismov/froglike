using UnityEngine;
using UnityEngine.InputSystem;

public class Playerscript : MonoBehaviour
{
    public Rigidbody2D myRb;
    public float moveSpeed = 5f;
    public InputSystem_Actions playerControls;

    Vector2 moveDirection = Vector2.zero;
    private InputAction move;

    private InputAction attack;

    private InputAction attackDirection;
    Vector2 attackDir = Vector2.zero;



    private void Awake()
    {
        playerControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
        move = playerControls.Player.Move;
        move.Enable();

        attack = playerControls.Player.Attack;
        attack.Enable();
        attack.performed += Attack;

        attackDirection = playerControls.Player.AttackDirection;
        attackDirection.Enable();
        attackDirection.performed += AttackDirection;

    }
    private void OnDisable()
    {
        move.Disable();
        attack.Disable();
        attackDirection.Disable();
    }
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();

        attackDir = attackDirection.ReadValue<Vector2>();

        if (attackDir != Vector2.zero)
        {
            Shoot(attackDir); 
        }


    }

    private void FixedUpdate()
    {
        myRb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("You've succesfully performed an attack");
    }

    private void AttackDirection(InputAction.CallbackContext context)
    {
        Debug.Log("You've succefully fired");
    }
    void Shoot(Vector2 direction)
    {
        Debug.Log("Strzelam w kierunku: " + direction);
    }

}
