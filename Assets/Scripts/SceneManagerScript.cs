using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0); // Ana menü sahnesinin index'i genellikle 0'dýr
    }

    public void QuitGame()
    {
        Application.Quit(); // Oyunu kapatmak için
    }
}
