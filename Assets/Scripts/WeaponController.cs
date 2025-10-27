using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    [Header("Efectos")]
    public GameObject efectoFlash;

    private Transform cameraPlayerTransform;

    private void Start(){
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update(){
        HandleShoot();

        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);
    }

    private void HandleShoot(){
        if(Input.GetButtonDown("Fire1")){

            GameObject flashClone =Instantiate(efectoFlash, boquillaArma.position, Quaternion.Euler(boquillaArma.forward), transform);
            Destroy(flashClone, 0.1f);
            AddRecoil();

            RaycastHit hit;
            if(Physics.Raycast(cameraPlayerTransform.position, cameraPlayerTransform.forward, out hit, fireRange, hittableLayers)){
                if(bulletHolePrefab != null){
                    GameObject bulletHoleClone = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
                    Destroy(bulletHoleClone, 4f);
                }else{
                    Debug.LogWarning("No bullet hole prefab assigned in WeaponController.");
                }
            }
        }
    }

    private void AddRecoil(){
        transform.Rotate(-retroceso, 0, 0);
        transform.position = transform.position - transform.forward * (retroceso / 50f);
    }

}
