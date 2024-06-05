using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolerArrow : MonoBehaviour {

	public float distance;

	Transform player;
	public GameObject arrow;
	public Transform arrowPos;
	private Animator anim;

	private float timeBtwShot;
	public float startTimeBtwShot;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 differ = player.position - transform.position;
		float rotZ = Mathf.Atan2 (differ.y, differ.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rotZ );
		if (transform.position.x > player.position.x && transform.localScale.x > 0) {
			Vector3 Scaler = transform.localScale;
			Scaler.x *= -1;
			transform.localScale = Scaler;
		} else if(transform.position.x < player.position.x && transform.localScale.x < 0) {
			Vector3 Scaler = transform.localScale;
			Scaler.x *= -1;
			transform.localScale = Scaler;
		}
		if (timeBtwShot < 0) {
			if (((player.position.x - distance) < transform.position.x) && (transform.position.x < player.position.x)) {
				anim.SetTrigger ("ArrowAttack");
				timeBtwShot = startTimeBtwShot;

			} else if ((player.position.x + distance) > transform.position.x && transform.position.x > player.position.x) {
				anim.SetTrigger ("ArrowAttack");
				timeBtwShot = startTimeBtwShot;

			}
		} else
			timeBtwShot -= Time.deltaTime;
		
	}
	public void OnArrow(){
		Instantiate (arrow, arrowPos.position, transform.rotation);
	}
}
