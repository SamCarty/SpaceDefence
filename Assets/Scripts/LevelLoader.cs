using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float gameOverDelaySeconds = 1f;
    
    MusicBox musicBox;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start() {
        musicBox = GetMusicBox();
        musicBox.PlayMenuMusic();
    }

    public void LoadStartMenu() {
        SceneManager.LoadScene(0);
        musicBox = GetMusicBox();
        musicBox.PlayMenuMusic();
    }

    public void LoadGame() {
        SceneManager.LoadScene("Level");
        musicBox = GetMusicBox();
        musicBox.PlayGameMusic();
    }
    
    public void LoadGameOver() {
        musicBox.StopMusic();
        StartCoroutine(WaitForGameOver());
    }

    IEnumerator WaitForGameOver() {
        yield return new WaitForSeconds(gameOverDelaySeconds);
        SceneManager.LoadScene("Game Over");
        musicBox = GetMusicBox();
        musicBox.PlayMenuMusic();
    }

    public void Quit() {
        Application.Quit();
    }
    
    MusicBox GetMusicBox() {
        return GameObject.FindGameObjectWithTag("Music").GetComponent<MusicBox>();
    }
    
}
