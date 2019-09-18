using UnityEngine;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float formationSpeed;
	public float spawnDelay = 0.5f;

	private float width = 10f, height = 5.55f;
	private float minXBorder, maxXBorder;
	
	private bool reverseDirection = false;


	void Start () {
		float distanceToCamera = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
		minXBorder = leftBoundary.x + width / 2;
		maxXBorder = rightBoundary.x - width / 2;
		this. SpawnUntillFull();
	}

	private void SpawnEnemies() {
		foreach (Transform child in this.transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	private void SpawnUntillFull() {
		Transform freePosition = this.NextFreePosition();
		if (freePosition) {
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (this.NextFreePosition()) {
			Invoke("SpawnUntillFull", spawnDelay);
		}
	}
	
	private void OnDrawGizmos() {
		Gizmos.DrawWireCube(this.transform.position, new Vector3(width, height));
	}

	void Update () {
		this.formationMovement();

		if (AllMembersDead()) {
			this.SpawnUntillFull();
		}
	}

	private Transform NextFreePosition() {
		foreach (Transform position in this.transform) {
			if (position.childCount == 0) {
				return position;
			}
		}
		return null;
	}

	private bool AllMembersDead() {
		foreach (Transform position in this.transform) {
			if (position.childCount > 0) {
				return false;
			}
		}
		return true;
	}

	private void formationMovement() {
		if (!reverseDirection) {
			this.transform.position += Vector3.left * formationSpeed * Time.deltaTime;
		} else {
			this.transform.position += Vector3.right * formationSpeed * Time.deltaTime;
		}

		float newX = Mathf.Clamp(this.transform.position.x, minXBorder, maxXBorder);
		this.transform.position = new Vector3 (newX, this.transform.position.y,
													 this.transform.position.z);

		if (this.transform.position.x <= this.minXBorder) {
			reverseDirection = true;
		} else if (this.transform.position.x >= this.maxXBorder) { 
			reverseDirection = false;
		}
	}
}
