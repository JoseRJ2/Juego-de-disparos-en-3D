using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WeaponController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform boquillaArma;

    [Header("General")]
    public LayerMask hittableLayers;
    public GameObject bulletHolePrefab;

    [Header("Parametros de disparo")]
    public float fireRange  = 200;
    public float retroceso = 4f; //fuerza de retroceso del arma
    public float cadenciaDisparo = 0.4f; //tiempo entre disparos

    [Header("Munición")]
    public int municionMaxima = 8;
    public int municionActual;
    private float tiempoUltimoDisparo = Mathf.NegativeInfinity;
    public float tiempoRecarga = 2f;


    [Header("Efectos")]
    public GameObject efectoFlash;

    private Transform cameraPlayerTransform;
    public GameObject owner { get; set; }
    private void Awake(){
        municionActual = municionMaxima;
        eventManager.current.onAmmoChanged.Invoke(municionActual, municionMaxima);
    }
    private void Start(){
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update(){
        if (Input.GetButtonDown("Fire1"))
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
       if(tiempoUltimoDisparo + cadenciaDisparo <= Time.time )
        {
            if(municionActual > 0)
            {
                HandleShoot();
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
            if (hitInfo.collider.gameObject != owner)
            {
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
        yield return new WaitForSeconds(tiempoRecarga);
        municionActual = municionMaxima;
        eventManager.current.onAmmoChanged.Invoke(municionActual, municionMaxima);
    }

}
