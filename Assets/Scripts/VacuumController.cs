using UnityEngine;
using UnityEngine.InputSystem; 

public class VacuumController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;

    [Header("Vacuum Settings")]
    [SerializeField] private float vacuumRange = 3f;
    [SerializeField] private LayerMask garbageLayer;

    private Vector2 movementInput;

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    private void Update()
    {
        Move();
        Vacuum();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(movementInput.x, movementInput.y, 0f).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    private void Vacuum()
    {
        Collider2D[] closeGarbage = Physics2D.OverlapCircleAll(transform.position, vacuumRange, garbageLayer);

        foreach (Collider2D garbageCollider in closeGarbage)
        {
            if (garbageCollider.TryGetComponent<Collectable>(out var garbageScript))
            {
                garbageScript.StartVacuumPull(transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, vacuumRange);
    }
}