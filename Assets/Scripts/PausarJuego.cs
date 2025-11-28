using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PausarJuego : MonoBehaviour
{
    public GameObject menuPausa;
    public static bool juegoPausado = false; // Changed to static

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }
    public void Reanudar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SalirAlMenu()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
