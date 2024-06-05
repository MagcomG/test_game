using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBreath : MonoBehaviour {

	public Transform headPos;
	public Transform legPos;
	public LayerMask water;
	bool isWater;
	bool isWaterD;
	public float checkRadius;
	private float waterBreath;
	public float startBreath;
	private float swimInput;
	private float speed = 5;

	public int numOfBreah;
	public Image[] breahs;
	public Sprite fullBreath;
	public Sprite emptyBreath;

	private BoxMove player;
	private Rigidbody2D rb;

	public GameObject breathWindow;

	// Use this for initialization
	void Start () {
		player = GetComponent<BoxMove> ();
		waterBreath = startBreath;
		rb = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		isWater = Physics2D.OverlapCircle (headPos.position, checkRadius, water);
		isWaterD = Physics2D.OverlapCircle (legPos.position, checkRadius, water);
		if (isWater == true) {
			
			if (waterBreath < 0) {
				waterBreath += 1;
				player.TakeDamage (1);
			} else
				waterBreath -= Time.deltaTime;
			breathWindow.SetActive (true);

		} else {
			swimInput = 0;
			waterBreath = startBreath;
			breathWindow.SetActive (false);
		}


	}
	void FixedUpdate () {
		if (isWaterD == true) {
			swimInput = Input.GetAxis ("Vertical");
			rb.velocity = new Vector2 (rb.velocity.x, swimInput * speed);
		}
		for (int i = 0; i < breahs.Length; i++) {
			if (i < Mathf.RoundToInt (waterBreath)) {
				breahs [i].sprite = fullBreath;
			} else
				breahs [i].sprite = emptyBreath;
			if (i < numOfBreah) {
				breahs [i].enabled = true;
			} else
				breahs [i].enabled = false;
		}
	}
	//void OnDrawGizmos(){
		//Gizmos.color = Color.red;
		//Gizmos.DrawSphere (headPos.position, headPos.position.x  checkRadius);
	//}

}
