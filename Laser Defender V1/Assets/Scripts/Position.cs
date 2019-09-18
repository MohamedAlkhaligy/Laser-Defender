using UnityEngine;

public class Position : MonoBehaviour {

	private float radius = 1f;

	private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(this.transform.position, radius);
	}

}
