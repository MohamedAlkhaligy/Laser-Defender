using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	private static MusicPlayer instance = null;

	public AudioClip startClip, gameClip, endClip;

	private AudioSource music;
	
	private void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			var scene = SceneManager.GetActiveScene();
			OnSceneLoaded(scene, LoadSceneMode.Single);
		}
	}

	private void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (music == null) return;
		var levelIndex = scene.buildIndex;
		music.Stop();
		switch (levelIndex) {
			case 0:
				music.clip = startClip;
				break;
			case 1:
				music.clip = gameClip;
				break;
			case 2:
				music.clip = endClip;
				break;
		}
		music.loop = true;
		music.Play();
	}
}
