using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	void Start () {
		Text score = this.GetComponent<Text>();
		score.text = ScoreKeeper.score.ToString();
		ScoreKeeper.Reset();
	}
	
	void Update () {
		
	}
}
