using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaFinal : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
