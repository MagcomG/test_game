using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroler : MonoBehaviour {


	private float speed;
	public float normalSpeed;

	public int positionOfPatrol;
	public Transform point;
	bool moveinfRight;
	Transform player;
	public float stopingDistance;
	bool chill =false;
	bool angry =false;
	bool goBack =false;
	private bool facingRight = true;

	public GameObject effect;
	public int health;
	public float distance;

	private float stopTime;
	public float startStopTime;


	// Use this for initialization
	public void TakeDamage(int damage){
		health -= damage;
		Instantiate (effect, transform.position, Quaternion.identity);
		stopTime = startStopTime;
	}
		
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (stopTime <= 0) {
			speed = normalSpeed;
		} else {
			speed = 0;
			stopTime -= Time.deltaTime;
		}


		if (Vector2.Distance (transform.position, point.position) < positionOfPatrol && angry == false)
			chill = true;
		if (Vector2.Distance (transform.position, player.position) < stopingDistance) {
			angry = true;
			chill = false;
			goBack = false;
		}
		else if (Vector2.Distance (transform.position, player.position) > stopingDistance){
			goBack = true;
			angry = false;
		}

		if (chill == true)
			Chill ();
		else if (angry == true)
			Angry ();
		else if (goBack == true)
			GoBack ();

		if (health <= 0)
			Destroy (gameObject);
	}
	void Flip(){
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;

	}
	void Chill()
	{
		if (transform.position.x > point.position.x + positionOfPatrol) {
			moveinfRight = false;
			if (facingRight == true)
				Flip ();

		}else if (transform.position.x < point.position.x - positionOfPatrol) {
			moveinfRight = true;
			if (facingRight == false)
				Flip ();

		}
		if (moveinfRight) {
			transform.position = new Vector2 (transform.position.x + speed * Time.deltaTime, transform.position.y);
		}else
			transform.position = new Vector2 (transform.position.x - speed * Time.deltaTime, transform.position.y);
			
	}
	private void Angry()
	{
		
		if (transform.position.x - distance < player.position.x && transform.position.x > player.position.x)
			transform.position = Vector2.MoveTowards (transform.position, player.position, -speed * Time.deltaTime);
		else if (transform.position.x + distance > player.position.x && transform.position.x < player.position.x)
			transform.position = Vector2.MoveTowards (transform.position, player.position, -speed * Time.deltaTime);
		else {
			transform.position = Vector3.MoveTowards (transform.position, player.position, speed * Time.deltaTime);
		}

		if (transform.position.x > player.position.x && facingRight == true)
			Flip ();
		else if (transform.position.x < player.position.x && facingRight == false)
			Flip ();
	}
	void GoBack()
	{
		transform.position = Vector2.MoveTowards (transform.position, point.position, speed * Time.deltaTime);
		if (transform.position.x > point.position.x && facingRight == true)
			Flip ();
		else if (transform.position.x < point.position.x && facingRight == false)
			Flip ();
	}
}
