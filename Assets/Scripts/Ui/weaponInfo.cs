using TMPro;
using UnityEngine;

public class weaponInfo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text currentBullets;
    public TMP_Text totalBullets;

    private void OnEnable()
    {
        eventManager.current.onAmmoChanged.AddListener(UpdateAmmoInfo);
    }

    private void OnDisable()
    {
        eventManager.current.onAmmoChanged.RemoveListener(UpdateAmmoInfo);
    }

    public void UpdateAmmoInfo(int current, int total)
    {
        currentBullets.text = current.ToString();
        totalBullets.text = total.ToString();
        if (current == 0)
        {
            currentBullets.color = Color.red;
        }
        else
        {
            currentBullets.color = Color.white;
        }
    }
}
