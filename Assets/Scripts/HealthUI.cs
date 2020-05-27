using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    TextMeshProUGUI text;
    Player player;
    
    void Start() {
        text = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }
    
    void Update() {
        int health = player.GetHealth();
        if (health < 0) health = 0;
        text.text = health.ToString();
    }
}
