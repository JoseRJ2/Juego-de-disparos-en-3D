using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ladronController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip deathClip;

    [Header("Animación")]
    public Animation Anim;
    public string caminarAnim; // nombre del clip de caminar
    //public string idleAnim;    // opcional: nombre del clip de idle

    [Header("Movimiento (NavMesh)")]
    public NavMeshAgent AI;
    public float velocidadMovimiento = 3.5f;
    public Transform[] objetivos;
    Transform objetivo;

    private float distanciaObjetivo = 1.5f;

    void Start()
    {
        objetivo = objetivos[Random.Range(0, objetivos.Length)];
        //AI.speed = velocidadMovimiento;

        
        Anim.Play(caminarAnim);
    }

    void Update()
    {
        

        distanciaObjetivo = Vector3.Distance(transform.position, objetivo.position);

        // Cambiar objetivo al llegar
        if (distanciaObjetivo < 1.5f)
        {
            objetivo = objetivos[Random.Range(0, objetivos.Length)];
        }

        // Actualizar destino
        AI.destination = objetivo.position;

        // 💡 Detectar si se está moviendo
        AI.speed = velocidadMovimiento;
    }


public void RecibirDisparo()
{
    Debug.Log("¡El ladrón ha sido alcanzado por un disparo!");
    audioSource.volume = 2f;
    audioSource.PlayOneShot(deathClip);

    StartCoroutine(DestruirDespuesDeSonido());
}

private IEnumerator DestruirDespuesDeSonido()
{
    // Esperar la duración del clip
    yield return new WaitForSeconds(deathClip.length);
    //Destroy(gameObject);
}
}
