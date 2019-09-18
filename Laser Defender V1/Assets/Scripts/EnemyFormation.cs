using UnityEngine;

public class EnemyFormation : MonoBehaviour {

	public float health = 150f, projectileVelocity = 5f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	public GameObject projectile;
	public AudioClip enemyLaser, enemyDies, enemyEnters;

	private ScoreKeeper scoreKeeper;
	private Animator animator;

	private void Start() {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		animator = this.GetComponent<Animator>();
		if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Arrival")) {
			AudioSource.PlayClipAtPoint(enemyEnters, this.transform.position);
		}
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile) {
			this.health -= missile.GetDamage();
			missile.DestroyOnHit();
			if (this.health <= 0) {
				Destroy(this.gameObject);
				scoreKeeper.Score(scoreValue);
				AudioSource.PlayClipAtPoint(enemyDies, this.transform.position);
			}
		}
	}

	private void Update() {
		float probability = shotsPerSecond * Time.deltaTime;
		if (Random.value <= probability) {
			EnemyFire();
		}
	}

	private void EnemyFire() {
		Vector3 startPosition = this.transform.position + new Vector3(0, -1);
		GameObject beam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileVelocity);
		AudioSource.PlayClipAtPoint(enemyLaser, this.transform.position);
	}
}
