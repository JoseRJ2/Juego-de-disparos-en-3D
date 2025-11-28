using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NivelSelector : MonoBehaviour
{
    public void Nivel1()
    {
        SceneManager.LoadScene("Nivel_1");
    }
    
    public void Nivel2()
    {
        SceneManager.LoadScene("Nivel_2");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
