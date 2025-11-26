using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuWin;
    public float tiempoFinal = 164f; // 2 minutos 39 segundos = 
    private float tiempo;

    void Start()
    {
        menuWin.SetActive(false); // ocultamos el menú al comenzar
    }

    void Update()
    {
        tiempo += Time.deltaTime;

        if (tiempo >= tiempoFinal)
        {
            ShowWinMenu();
        }
    }

    public void ShowWinMenu()
    {
        menuWin.SetActive(true);
        Time.timeScale = 0f; // pausa el juego
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego (solo funciona en build)");
    }
}
