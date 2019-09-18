using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    private Text scoreText;
    private GameSession gameSession;

	private void Start () {
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
	}
	
	private void Update () {
        scoreText.text = gameSession.GetScore().ToString();
	}
}
