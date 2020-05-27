using UnityEngine;

public class MusicBox : MonoBehaviour {

    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;
    
    AudioSource audioSource;
    AudioClip currentlyPlaying;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMenuMusic() {
        if (currentlyPlaying == menuMusic) return;
        PlayMusic(menuMusic);
    }

    public void PlayGameMusic() {
        if (currentlyPlaying == gameMusic) return;
        PlayMusic(gameMusic);
    }

    public void StopMusic() {
        audioSource.Stop();
    }

    void PlayMusic(AudioClip music) {
        StopMusic();
        currentlyPlaying = music;
        audioSource.clip = music;
        audioSource.Play();
    }
}
