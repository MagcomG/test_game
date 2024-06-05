using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawn : MonoBehaviour {

	public GameObject waterDrop;
	private float cooldown = 0;
	private Camera mainCamera;
	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.LeftShift)) {
			cooldown -= Time.deltaTime;
			while (cooldown < 0) {
				cooldown += 0.01f;
				Instantiate (waterDrop, (Vector2)mainCamera.ScreenToWorldPoint (Input.mousePosition) + Random.insideUnitCircle * .2f, Quaternion.identity);
			}
		}
	}
}
