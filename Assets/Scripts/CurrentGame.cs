using UnityEngine;

public class CurrentGame : MonoBehaviour {
    int score = 0;

    void Awake() {
        SetupSingleton();
    }

    void SetupSingleton() {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() {
        return score;
    }

    public void AddToScore(int value) {
        score += value;
    }

    public void ResetGame() {
        Destroy(gameObject);
    }
    
}
