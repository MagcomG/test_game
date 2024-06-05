using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : MonoBehaviour {

	public float speed;
	private float lifetime = 10;
	public float distance;
	public int damage;
	public LayerMask whatIsSolid;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hitInfo = Physics2D.Raycast (transform.position, transform.up, distance, whatIsSolid);
		if (hitInfo.collider != null) {
			if (hitInfo.collider.CompareTag ("Player")) {
				hitInfo.collider.GetComponent<BoxMove>().TakeDamage (damage);
			}
			Destroy (gameObject);
		}
		transform.Translate (Vector2.right * speed * Time.deltaTime);
		Destroy (gameObject, lifetime);
	}
}
