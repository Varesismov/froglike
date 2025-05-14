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
    private InputAction interact;


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

        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
    }
    private void OnDisable()
    {
        move.Disable();
        attack.Disable();
        interact.Disable();
    }
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();

    }

    private void FixedUpdate()
    {
        myRb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("You've succesfully performed an attack");
    }
    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("You've picked up and item");
    }
}
