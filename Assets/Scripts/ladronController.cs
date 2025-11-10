// // using UnityEngine;
// // using UnityEngine.AI;
// // using System.Collections;

// // public class ladronController : MonoBehaviour
// // {
// //     [Header("Audio")]
// //     public AudioSource audioSource;
// //     public AudioClip deathClip;

// //     [Header("Animación")]
// //     public Animation Anim;
// //     public string caminarAnim;
// //     public string muerteAnim;
// //     [Header("Movimiento (NavMesh)")]
// //     public NavMeshAgent AI;
// //     public float velocidadMovimiento = 3.5f;
// //     public Transform[] objetivos;
// //     private Transform objetivo;
// //     private float distanciaObjetivo = 1.5f;

// //     void Start()
// //     {
// //         objetivo = objetivos[Random.Range(0, objetivos.Length)];
// //         AI.speed = velocidadMovimiento;

// //         // Configurar y reproducir animación de caminar en bucle
// //         Anim[caminarAnim].wrapMode = WrapMode.Loop;
// //         Anim.CrossFade(caminarAnim, 0.15f);
// //     }

// //     void Update()
// //     {
// //         distanciaObjetivo = Vector3.Distance(transform.position, objetivo.position);

// //         if (distanciaObjetivo < 1.5f)
// //         {
// //             objetivo = objetivos[Random.Range(0, objetivos.Length)];
// //         }

// //         AI.destination = objetivo.position;

// //         // Asegurar que siga animando (por seguridad)
// //         if (!Anim.isPlaying)
// //         {
// //             Anim.CrossFade(caminarAnim, 0.1f);
// //         }
// //     }

// //     // public void RecibirDisparo()
// //     // {
// //     //     Debug.Log("¡El ladrón ha sido alcanzado por un disparo!");
// //     //     audioSource.volume = 2f;
// //     //     audioSource.PlayOneShot(deathClip);
// //     //     StartCoroutine(DestruirDespuesDeSonido());
// //     // }
// //     public void RecibirDisparo()
// // {
// //     Debug.Log("¡El ladrón ha sido alcanzado por un disparo!");
    
// //     // Detener movimiento y navegación
// //   //  #AI.isStopped = true;
// // //AI.enabled = false;

// //     // Reproducir sonido
// //     audioSource.volume = 2f;
// //     audioSource.PlayOneShot(deathClip);

// //     // Reproducir animación de muerte (una sola vez)
// //     if (Anim[muerteAnim] != null)
// //     {
// //         Anim[muerteAnim].wrapMode = WrapMode.Once;
// //         Anim.CrossFade(muerteAnim, 0.1f);
// //     }

// //     // Destruir después del sonido o del fin de la animación
// //     float delay = Mathf.Max(deathClip.length, Anim[muerteAnim].length);
// //     StartCoroutine(DestruirDespuesDe(delay));
// // }

// // private IEnumerator DestruirDespuesDe(float tiempo)
// // {
// //     yield return new WaitForSeconds(tiempo);
// //     Destroy(gameObject);
// // }

// //     private IEnumerator DestruirDespuesDeSonido()
// //     {
// //         yield return new WaitForSeconds(deathClip.length);
// //         //Destroy(gameObject);
// //     }
// // }
// using UnityEngine;
// using UnityEngine.AI;
// using System.Collections;

// public class ladronController : MonoBehaviour
// {
//     [Header("Audio")]
//     public AudioSource audioSource;
//     public AudioClip deathClip;

//     [Header("Animación")]
//     public Animation Anim;
//     public string caminarAnim;
//     public string muerteAnim;
//     [Header("Movimiento (NavMesh)")]
//     public NavMeshAgent AI;
//     public float velocidadMovimiento = 3.5f;
//     public Transform[] objetivos;
//     private Transform objetivo;
//     private float distanciaObjetivo = 1.5f;

//     void Start()
//     {
//         objetivo = objetivos[Random.Range(0, objetivos.Length)];
//         AI.speed = velocidadMovimiento;

//         // Configurar y reproducir animación de caminar en bucle
//         Anim[caminarAnim].wrapMode = WrapMode.Loop;
//         Anim.CrossFade(caminarAnim, 0.15f);
//     }

//     void Update()
//     {
//         distanciaObjetivo = Vector3.Distance(transform.position, objetivo.position);

//         if (distanciaObjetivo < 1.5f)
//         {
//             objetivo = objetivos[Random.Range(0, objetivos.Length)];
//         }

//         AI.destination = objetivo.position;

//         // Asegurar que siga animando (por seguridad)
//         if (!Anim.isPlaying)
//         {
//             Anim.CrossFade(caminarAnim, 0.1f);
//         }
//     }

//     // public void RecibirDisparo()
//     // {
//     //     Debug.Log("¡El ladrón ha sido alcanzado por un disparo!");
//     //     audioSource.volume = 2f;
//     //     audioSource.PlayOneShot(deathClip);
//     //     StartCoroutine(DestruirDespuesDeSonido());
//     // }
//     public void RecibirDisparo()
//     {
//         Debug.Log("¡El ladrón ha sido alcanzado por un disparo!");
//         audioSource.volume = 2f;
//         audioSource.PlayOneShot(deathClip);

//         // Detener movimiento y navegación
//         AI.isStopped = true;


//         // Reproducir sonido


//         // Reproducir animación de muerte (una sola vez)
//         if (Anim[muerteAnim] != null)
//         {
//             Anim[muerteAnim].wrapMode = WrapMode.Once;
//             Anim.CrossFade(muerteAnim, 0.1f);
//         }

//         // Destruir después del sonido o del fin de la animación
//         float delay = Mathf.Max(deathClip.length, Anim[muerteAnim].length);
//         StartCoroutine(DestruirDespuesDe(delay));
//     }

//     private IEnumerator DestruirDespuesDe(float tiempo)
//     {
//         yield return new WaitForSeconds(tiempo);
//         AI.enabled = false;
//         Destroy(gameObject);
//     }


// }

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
    public string muerteAnim;

    [Header("Movimiento (NavMesh)")]
    public NavMeshAgent AI;
    public float velocidadMovimiento = 3.5f;
    public Transform[] objetivos;

    private Transform objetivo;
    private float distanciaObjetivo = 1.5f;
    private bool muerto = false;

    void Start()
    {
        objetivo = objetivos[Random.Range(0, objetivos.Length)];
        AI.speed = velocidadMovimiento;

        // Configurar y reproducir animación de caminar en bucle
        if (Anim[caminarAnim] != null)
        {
            Anim[caminarAnim].wrapMode = WrapMode.Loop;
            Anim.CrossFade(caminarAnim, 0.15f);
        }
    }

    void Update()
    {
        if (muerto) return; // 👈 Evita animaciones o movimiento después de morir

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
        if (muerto) return; // 👈 evita que se ejecute más de una vez
        muerto = true;

        Debug.Log("¡El ladrón ha sido alcanzado por un disparo!");

        // Detener movimiento y navegación
        AI.isStopped = true;

        // Reproducir sonido
        audioSource.volume = 2f;
        audioSource.PlayOneShot(deathClip);

        // Reproducir animación de muerte (una sola vez)
        if (Anim[muerteAnim] != null)
        {
            Anim[muerteAnim].wrapMode = WrapMode.Once;
            Anim.CrossFade(muerteAnim, 0.1f);
        }
        else
        {
            Debug.LogWarning("⚠️ No se encontró la animación de muerte: " + muerteAnim);
        }

        // Esperar a que termine sonido o animación
        float delay = Mathf.Max(
            deathClip != null ? deathClip.length : 0f,
            (Anim[muerteAnim] != null) ? Anim[muerteAnim].length : 0f
        );

        StartCoroutine(DestruirDespuesDe(delay));
    }

    private IEnumerator DestruirDespuesDe(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        AI.enabled = false;
        Destroy(gameObject);
    }
}
