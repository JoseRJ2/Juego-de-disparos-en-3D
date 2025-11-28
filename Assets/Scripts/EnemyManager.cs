using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private int enemigosVivos = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void RegistrarEnemigo()
    {
        enemigosVivos++;
        Debug.Log("Enemigos vivos: " + enemigosVivos);
    }

    public void EnemigoMuerto()
    {
        enemigosVivos--;
        Debug.Log("Enemigos vivos: " + enemigosVivos);

        if (enemigosVivos <= 0)
        {
            Debug.Log("¡Nivel completado!");
            PasarAlSiguienteNivel();
        }
    }

    private void PasarAlSiguienteNivel()
    {
        int nivelActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(nivelActual + 1);
    }
}
