using UnityEngine;

public class Projectile : MonoBehaviour {
	public float damage = 100f;

	public float GetDamage() {
		return this.damage;
	}

	public void DestroyOnHit() {
		Destroy(this.gameObject);
	}
}
