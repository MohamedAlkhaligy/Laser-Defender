using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10f, padding = 0.5f;
	private float health = 250f;
	public GameObject projectile;
	public float firingRate;
	public AudioClip playerLaser;


	private Vector2 projectileVelocity;
	private float minXBorder, maxXBorder;

	private void Start () {
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		minXBorder = leftmost.x + padding;
		maxXBorder = rightmost.x - padding;

		projectileVelocity = new Vector2(0f, 10f);
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile) {
			this.health -= missile.GetDamage();
			missile.DestroyOnHit();
			if (this.health <= 0) {
				Destroy(this.gameObject);
				GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadLevel("End");
			}
		}
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.00000001f, firingRate);
		}

		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			this.transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			this.transform.position += Vector3.right * speed * Time.deltaTime;
		}

		float newX = Mathf.Clamp(this.transform.position.x, minXBorder, maxXBorder);
		this.transform.position = new Vector3(newX, this.transform.position.y,
													this.transform.position.z);
	}

	private void Fire() {
		Vector3 startPosition = this.transform.position + new Vector3(0, 1);
		GameObject beam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = projectileVelocity;
		AudioSource.PlayClipAtPoint(playerLaser, this.transform.position);
	}
}
