using UnityEngine;

public class HUD_Manager : MonoBehaviour
{
    public GameObject weaponInfoPrefab;

    private void Start()
    {
        eventManager.current.newGunEvent.AddListener(createWeaponInfo);
    }


    private void createWeaponInfo()
    {
        
        Instantiate(weaponInfoPrefab,transform);
    }
}
