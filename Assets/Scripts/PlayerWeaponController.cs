using UnityEngine;
using System.Collections.Generic;

public class PlayerWeaponController : MonoBehaviour
{
    public List<WeaponController> startingWeapons = new List<WeaponController>();
    public Transform weaponParentSocket;
    public Transform defaultWeaponPosition;
    public Transform aimingPosition;

    public int activeWeaponIndex { get; private set; }

    private WeaponController[] weaponSlots = new WeaponController[2];
    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
        
        // NO cambiar el padre del weaponParentSocket - ya está correctamente ubicado
        // En su lugar, asegurarnos de que las armas se posicionen correctamente

        activeWeaponIndex = -1;
        foreach(WeaponController startingWeapon in startingWeapons)
        {
            AddWeapon(startingWeapon);
            //Debug.Log("Added weapon: " + startingWeapon.name);
        }
        SwitchWeapon();
        //if (weaponSlots[0] != null) SwitchWeapon();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon();
        }
        
        // DEBUG: Tecla para probar posiciones
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Default Position: " + defaultWeaponPosition.position);
            Debug.Log("Weapon Position: " + weaponSlots[activeWeaponIndex].transform.position);
        }
    }

    private void AddWeapon(WeaponController p_weaponPrefab)
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i] == null)
            {
                WeaponController weaponClone = Instantiate(p_weaponPrefab, weaponParentSocket);
                weaponClone.owner = gameObject;
                // Usar posición MUNDIAL en lugar de posición local
                weaponClone.transform.position = defaultWeaponPosition.position;
                weaponClone.transform.rotation = defaultWeaponPosition.rotation;
                
                weaponClone.gameObject.SetActive(false);
                weaponSlots[i] = weaponClone;

                
                return;
            }
        }
    }
    
    private void SwitchWeapon()
    {
        int temp_index = (activeWeaponIndex + 1) % weaponSlots.Length;
        if (weaponSlots[temp_index] == null)
        {
            return;
        }
        foreach (WeaponController weapon in weaponSlots)
        {
            if (weapon != null)
            {
                weapon.gameObject.SetActive(false);
            }
        }

        weaponSlots[temp_index].gameObject.SetActive(true);
        activeWeaponIndex = temp_index;
        eventManager.current.newGunEvent.Invoke();
        // Reforzar posición al cambiar de arma
        weaponSlots[temp_index].transform.position = defaultWeaponPosition.position;
        weaponSlots[temp_index].transform.rotation = defaultWeaponPosition.rotation;
    }
}