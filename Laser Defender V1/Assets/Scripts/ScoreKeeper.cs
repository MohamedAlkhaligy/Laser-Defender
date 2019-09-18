using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;
	private Text scoreText;

	private void Start() {
		scoreText = GetComponent<Text>();
	}

	public void Score(int points) {
		score += points;
		scoreText.text = Convert.ToString(score);
	}

	public static void Reset() {
		score = 0;
	}
}
