using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    private Text healthText;
    private Player player;

    private void Start() {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }

    private void Update() {
        healthText.text = player.GetHealth().ToString();
    }

}
