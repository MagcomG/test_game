using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	private float timeBtnwAttack;
	public float startTimeDtwAttack;

	public Transform attackPos;
	public LayerMask enemy;
	public float attackRange;
	public int damage;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	// Update is called once per frame
	void Update () {
		
		if (timeBtnwAttack <= 0) {
			if (Input.GetMouseButton(0)) {
				anim.SetTrigger ("onAttack");
			}
			timeBtnwAttack = startTimeDtwAttack;
		} else
			timeBtnwAttack -= Time.deltaTime;
	}
	public void OnAttack2(){
		Collider2D[] enemies = Physics2D.OverlapCircleAll (attackPos.position, attackRange , enemy);
		for (int i = 0; i < enemies.Length; i++) {
			enemies [i].GetComponent<Patroler> ().TakeDamage (damage);

		}
	}
	public void OnAttack(){
		Collider2D[] enemies = Physics2D.OverlapCircleAll (attackPos.position, attackRange, enemy);
		for (int n = 0; n < enemies.Length; n++) {
			enemies [n].GetComponent<Patroler2> ().TakeDamage (damage);

		}
	}
}