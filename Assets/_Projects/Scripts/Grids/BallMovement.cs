using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    private GameInput gameInput;
    private GridSystem gridManager;
    private Vector3 targetPos;
    private bool isMoving;
    private readonly float sqrtMinDistance = .01f;

    private void Awake()
    {
        gameInput = GetComponent<GameInput>();
    }

    private void Update()
    {
        HandleMovement();   
    }

    public void SetupBallMovement(GridSystem gridManager)
    {
        this.gridManager = gridManager;
    }

    private void HandleMovement()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            gridManager.PaintCell(transform.position);
            if ((targetPos - transform.position).sqrMagnitude <= sqrtMinDistance)
            {
                gridManager.ClearPathCells();
                transform.position = targetPos;
                isMoving = false;
            }

            return;
        }

        GetTargetPosition();
    }

    private void GetTargetPosition()
    {
        Vector2 moveDir = gameInput.GetMoveDirection();
        if (moveDir != Vector2.zero)
        {
            targetPos = GetFinalDestination(moveDir);
            if (transform.position != targetPos)
            {
                isMoving = true;
                GameEvents.RaiseBallMoved();
            }
        }
    }

    private Vector3 GetFinalDestination(Vector2 moveDirection)
    {
        Vector3 currentPos = transform.position;
        while (true)
        {
            Vector3 nextPos = currentPos + (Vector3)moveDirection;
            if (gridManager.IsWallCell(nextPos))
                break;

            currentPos = nextPos;
        }

        return gridManager.ClampToGridBoundaries(currentPos);
    }
}
