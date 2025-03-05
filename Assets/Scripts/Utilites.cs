using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilites : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
