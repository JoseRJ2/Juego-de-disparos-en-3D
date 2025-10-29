using UnityEngine;
using System.Collections;

public class ladronController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 2f;                 // Velocidad de movimiento
    public float waitTime = 2f;              // Tiempo de espera al llegar a un extremo
    public float moveDistance = 5f;          // Distancia total de patrullaje desde el punto inicial

    [Header("Visual")]
    public bool flipSprite = true;           // Si debe rotarse o voltearse al cambiar de dirección

    private Vector3 startPoint;
    private Vector3 targetPoint;
    private bool movingRight = true;
    private bool isWaiting = false;
    private float moveThreshold = 0.01f; // Umbral para detectar movimiento
    private Vector3 lastPosition;
    public Animator animator; // Referencia al Animator
    void Start()
    {
        startPoint = transform.position;
        targetPoint = startPoint + Vector3.right * moveDistance;
    }

    void Update()
    {
        if (!isWaiting)
            Move();
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        bool isMoving = distanceMoved > moveThreshold;
        // Actualizar la velocidad en el Animator
        if (animator != null)
        {
            animator.SetBool("isRunning",true);
        }
    }

    private void Move()
    {
        // Mover hacia el punto objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

        // Si llegó al destino, esperar antes de regresar
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            StartCoroutine(WaitAndSwitchDirection());
        }
    }

    IEnumerator WaitAndSwitchDirection()
    {
        isWaiting = true;

        // Esperar en el punto
        yield return new WaitForSeconds(waitTime);

        // Cambiar dirección
        movingRight = !movingRight;

        // Definir nuevo punto objetivo
        if (movingRight)
            targetPoint = startPoint + Vector3.right * moveDistance;
        else
            targetPoint = startPoint + Vector3.left * moveDistance;

        // Rotar el NPC si es necesario
        if (flipSprite)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = movingRight ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }

        isWaiting = false;
    }

    // Dibuja la ruta de patrulla en la vista de escena
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * moveDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * moveDistance);
    }
}
