using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBoat : MonoBehaviour {

	public Transform dCheck;
	public Transform sitPos;
	public float checkR; 
	public LayerMask whatIsBoat;
	private bool infoB;
	private bool sitOn;

	
	public float speed;
	private float moveInput;

	private Rigidbody2D rb;
	private BoxMove player;


	// Use this for initialization
	void Start () {
		player = GetComponent<BoxMove> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Physics2D.queriesStartInColliders = false;
		infoB = Physics2D.OverlapCircle (dCheck.position, checkR, whatIsBoat);
		if (sitOn == false && infoB == true && Input.GetKeyDown (KeyCode.L)) {
			sitOn = true;
			player.transform.position = sitPos.position;
		} else if(Input.GetKeyDown(KeyCode.F) && sitOn == true) {
			sitOn = false;

		}
	}
	void FixedUpdate(){
		if (sitOn == true) {
			if (moveInput > 0 || moveInput < 0) {
				rb.velocity = new Vector2 (moveInput * speed, rb.velocity.y);
			}
		}
	}
	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawSphere (dCheck.position, dCheck.position.x * checkR);
	}
}
