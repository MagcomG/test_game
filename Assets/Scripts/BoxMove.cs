using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoxMove : MonoBehaviour {

	public float speed;
	public float jumpForce;
	private float moveInput;
	private Rigidbody2D rb;
	bool isGround;
	public Transform groundCheck;

	public float checkRadius; 

	public LayerMask whatIsGround;
	public GameObject effect;
	public GameObject loseWindow;

	private int extraJump;
	private bool facingRight = true;
	public float health;

	private Animator anim;
	public int numOfHearts;
	public Image[] hearts;
	public Sprite fullHeart;
	public Sprite emptyHeart;


	RaycastHit2D hit;
	private bool hold;
	public float distance;
	public Transform holdpoint;
	public float throwObject;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.F)) {
			if (!hold) {
				Physics2D.queriesStartInColliders = false;
				hit = Physics2D.Raycast (transform.position, Vector2.right * transform.localScale.x, distance);
			
				if (hit.collider != null && hit.collider.tag == "Weapon") {
					hold = true;
				}
			} else {
				hold = false;

				if (hit.collider.gameObject.GetComponent<Rigidbody2D> () != null) {
					hit.collider.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(transform.localScale.x, 1	) * throwObject;
				}
					
			}
		}
		if (hold) {
			hit.collider.gameObject.transform.position = holdpoint.position;
			if (transform.localScale.x > 0)
				hit.collider.gameObject.transform.localScale = new Vector2 (transform.localScale.x, transform.localScale.y);
			else if (transform.localScale.x < 0 )
				hit.collider.gameObject.transform.localScale = new Vector2 (transform.localScale.x, transform.localScale.y);
		}



		isGround = Physics2D.OverlapCircle (groundCheck.position, checkRadius, whatIsGround);
		if (isGround == true)
			extraJump = 1;
		if (Input.GetKeyDown (KeyCode.W) &&  extraJump > 0) {
			rb.AddForce ((Vector2.up * jumpForce), ForceMode2D.Impulse);
			extraJump--;
		}

		if (health <= 0) {
			loseWindow.SetActive (true);
			Time.timeScale = 0;
		}

	}
	void FixedUpdate () {
		if(health > numOfHearts){
			health = numOfHearts;
		}
		for (int i = 0; i < hearts.Length; i++) {
			if (i < Mathf.RoundToInt (health)) {
				hearts [i].sprite = fullHeart;
			} else
				hearts [i].sprite = emptyHeart;
			if (i < numOfHearts) {
				hearts [i].enabled = true;
			} else
				hearts [i].enabled = false;
		}
		moveInput = Input.GetAxis ("Horizontal");
		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
		if (moveInput == 0)
			anim.SetBool ("isRunning", false);
		else
			anim.SetBool ("isRunning", true);
		if (moveInput > 0 && facingRight == false) {
			Flip ();
		} else if (moveInput < 0 && facingRight == true)
			Flip ();

	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag("Ground")){
			isGround = true;
		}
	}
	void Flip(){
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;

	}
	public void TakeDamage(int damage){
		health -= damage;
		Instantiate (effect, transform.position, Quaternion.identity);
	}
	public void RestartScene()
	{

		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		loseWindow.SetActive (false);
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, transform.position + Vector3.right * transform.localScale.x * distance );
	}
}
