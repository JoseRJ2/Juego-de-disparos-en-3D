using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum ShotType
{
    Manual,
    Automatic
}
public class WeaponController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform boquillaArma;


    [Header("General")]
    public LayerMask hittableLayers;
    public GameObject bulletHolePrefab;

    [Header("Parametros de disparo")]
    public float fireRange = 200;
    public float retroceso = 4f; //fuerza de retroceso del arma
    public float cadenciaDisparo = 0.2f; //tiempo entre disparos
    public ShotType shotType = ShotType.Manual;

    [Header("Munición")]
    public int municionMaxima = 8;
    public int municionActual;
    private float tiempoUltimoDisparo = Mathf.NegativeInfinity;
    public float tiempoRecarga = 2f;
    public AudioSource audioSource;
    public AudioClip disparoClip;
    public AudioClip automaticDisparoClip;
    public AudioClip recargaClip;

    [Header("Efectos")]
    public GameObject efectoFlash;

    private Transform cameraPlayerTransform;
    public GameObject owner { get; set; }
    private void Awake()
    {
        municionActual = municionMaxima;
        eventManager.current.onAmmoChanged.Invoke(municionActual, municionMaxima);
    }
    private void Start()
    {
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        if (shotType == ShotType.Manual && Input.GetButtonDown("Fire1"))
        {
            TryShoot();
        }

        else if (shotType == ShotType.Automatic && Input.GetButton("Fire1"))
        {
            TryShoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadCoroutine());
        }


        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);
    }
    private bool TryShoot()
    {
        if (tiempoUltimoDisparo + cadenciaDisparo <= Time.time)
        {
            if (municionActual > 0)
            {
                HandleShoot();

                AudioClip clipToPlay = null;
                if (shotType == ShotType.Manual)
                    clipToPlay = disparoClip;
                else if (shotType == ShotType.Automatic)
                    clipToPlay = automaticDisparoClip;

                
                audioSource.PlayOneShot(clipToPlay);

               // audioSource.PlayOneShot(disparoClip);
                municionActual--;
                eventManager.current.onAmmoChanged.Invoke(municionActual, municionMaxima);
                return true;
            }
        }
        return false;
    }
    private void HandleShoot()
    {
        GameObject flashClone = Instantiate(efectoFlash, boquillaArma.position, Quaternion.Euler(boquillaArma.forward), transform);
        Destroy(flashClone, 0.1f);
        AddRecoil();
        RaycastHit[] hits;
        hits = Physics.RaycastAll(cameraPlayerTransform.position, cameraPlayerTransform.forward, fireRange, hittableLayers);
        foreach (RaycastHit hitInfo in hits)
        {
               GameObject hitObject = hitInfo.collider.gameObject;
            if (hitObject != owner)
            {
                 if (hitObject.CompareTag("Ladron"))
        {
            Debug.Log("💥 Le diste al ladrón!");

            // Ejemplo: Si tiene un script de salud o controlador
            ladronController ladron = hitObject.GetComponent<ladronController>();
            if (ladron != null)
            {
                ladron.RecibirDisparo();
            }
        }

                GameObject bulletHoleClone = Instantiate(bulletHolePrefab, hitInfo.point + hitInfo.normal * 0.01f, Quaternion.LookRotation(hitInfo.normal));
                Destroy(bulletHoleClone, 4f);
            }
        }

        tiempoUltimoDisparo = Time.time;
    }


    private void AddRecoil()
    {
        transform.Rotate(-retroceso, 0, 0);
        transform.position = transform.position - transform.forward * (retroceso / 50f);
    }
    IEnumerator ReloadCoroutine()
    {
        // Aquí puedes agregar animaciones o efectos de recarga si lo deseas
        audioSource.PlayOneShot(recargaClip);
        yield return new WaitForSeconds(tiempoRecarga);
        municionActual = municionMaxima;
        
        eventManager.current.onAmmoChanged.Invoke(municionActual, municionMaxima);
    }

}
