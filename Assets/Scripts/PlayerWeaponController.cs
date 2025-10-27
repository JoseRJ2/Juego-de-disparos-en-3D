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
        }

        if (weaponSlots[0] != null) SwitchWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
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
    
    private void SwitchWeapon(int p_weaponIndex)
    {
        if (p_weaponIndex < 0 || p_weaponIndex >= weaponSlots.Length || weaponSlots[p_weaponIndex] == null)
            return;

        if (p_weaponIndex == activeWeaponIndex) return;

        if (activeWeaponIndex >= 0 && weaponSlots[activeWeaponIndex] != null)
            weaponSlots[activeWeaponIndex].gameObject.SetActive(false);

        weaponSlots[p_weaponIndex].gameObject.SetActive(true);
        activeWeaponIndex = p_weaponIndex;
        eventManager.current.newGunEvent.Invoke();
        // Reforzar posición al cambiar de arma
        weaponSlots[p_weaponIndex].transform.position = defaultWeaponPosition.position;
        weaponSlots[p_weaponIndex].transform.rotation = defaultWeaponPosition.rotation;
    }
}