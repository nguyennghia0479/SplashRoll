using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const int MIN_DISTANCE_TO_MOVE = 10;

    private PlayerInputSet inputs;
    private InputAction clickAction;
    private InputAction pointerAction;
    private Vector2 moveDirection;
    private Vector2 pressPos;
    private Vector2 releasePos;

    private void Awake()
    {
        inputs = new PlayerInputSet();
        clickAction = inputs.Player.Click;
        pointerAction = inputs.Player.Pointer;
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Update()
    {
        if (clickAction.WasPressedThisFrame())
        {
            pressPos = pointerAction.ReadValue<Vector2>();
        }
        else if (clickAction.WasReleasedThisFrame())
        {
            releasePos = pointerAction.ReadValue<Vector2>();
            Vector2 direction = releasePos - pressPos;
            if (direction.magnitude > MIN_DISTANCE_TO_MOVE)
                moveDirection = GetDirection(direction);
        }
    }

    private Vector2 GetDirection(Vector2 moveDirection)
    {
        if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
            return moveDirection.x > 0 ? Vector2.right : Vector2.left;
        else
            return moveDirection.y > 0 ? Vector2.up : Vector2.down;
    }

    public Vector2 GetMoveDirection()
    {
        Vector2 moveDir = moveDirection;
        moveDirection = Vector2.zero;

        return moveDir;
    }
}
