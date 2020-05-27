using System.Text;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour {
    TextMeshProUGUI text;
    CurrentGame currentGame;
    
    void Start() {
        text = GetComponent<TextMeshProUGUI>();
        currentGame = FindObjectOfType<CurrentGame>();
    }
    
    void Update() {
        var score = currentGame.GetScore();
        StringBuilder scoreString = new StringBuilder();
        if (score <= 9) scoreString.Append("0");
        if (score <= 99) scoreString.Append("0");
        if (score <= 999) scoreString.Append("0");
        if (score <= 9999) scoreString.Append("0");
        scoreString.Append(score.ToString());
        text.text = scoreString.ToString();
    }
}
