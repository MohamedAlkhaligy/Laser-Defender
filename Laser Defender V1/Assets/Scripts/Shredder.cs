using UnityEngine;

public class Shredder : MonoBehaviour {
	private void OnTriggerExit2D(Collider2D collider) {
		Destroy(collider.gameObject);
	}
}
