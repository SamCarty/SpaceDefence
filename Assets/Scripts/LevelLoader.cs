using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public void LoadStartMenu() {
        SceneManager.LoadScene(0);
    }

    public void LoadGame() {
        SceneManager.LoadScene("Level");
    }
    
    public void LoadGameOver() {
        SceneManager.LoadScene("Game Over");
    }

    public void Quit() {
        Application.Quit();
    }
    
}
