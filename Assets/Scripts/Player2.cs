using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour {

	public float speed;
	public float jumpForce;
	private Rigidbody2D rb;
	bool isGround;
	public Transform groundCheck;
	public float checkRadius;
	public LayerMask whatIsGround;
	private int extraJump;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		if (isGround == true)
			extraJump = 1;
		if (Input.GetKeyDown (KeyCode.I) &&  extraJump > 0) {
			rb.AddForce ((Vector2.up * jumpForce), ForceMode2D.Impulse);
			extraJump--;
		}
	}
	void FixedUpdate () {
		isGround = Physics2D.OverlapCircle (groundCheck.position, checkRadius, whatIsGround);
		if(Input.GetKey(KeyCode.L))
			transform.position = new Vector2 (transform.position.x + speed * Time.deltaTime, transform.position.y);
		if(Input.GetKey(KeyCode.J))
			transform.position = new Vector2 (transform.position.x -speed * Time.deltaTime, transform.position.y);


	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag("Ground")){
			isGround = true;
		}
	}
}
