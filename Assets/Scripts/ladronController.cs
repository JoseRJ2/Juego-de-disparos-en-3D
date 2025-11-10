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
    public string caminarAnim;

    [Header("Movimiento (NavMesh)")]
    public NavMeshAgent AI;
    public float velocidadMovimiento = 3.5f;
    public Transform[] objetivos;
    private Transform objetivo;
    private float distanciaObjetivo = 1.5f;

    void Start()
    {
        objetivo = objetivos[Random.Range(0, objetivos.Length)];
        AI.speed = velocidadMovimiento;

        // Configurar y reproducir animación de caminar en bucle
        Anim[caminarAnim].wrapMode = WrapMode.Loop;
        Anim.CrossFade(caminarAnim, 0.15f);
    }

    void Update()
    {
        distanciaObjetivo = Vector3.Distance(transform.position, objetivo.position);

        if (distanciaObjetivo < 1.5f)
        {
            objetivo = objetivos[Random.Range(0, objetivos.Length)];
        }

        AI.destination = objetivo.position;

        // Asegurar que siga animando (por seguridad)
        if (!Anim.isPlaying)
        {
            Anim.CrossFade(caminarAnim, 0.1f);
        }
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
        yield return new WaitForSeconds(deathClip.length);
        //Destroy(gameObject);
    }
}
