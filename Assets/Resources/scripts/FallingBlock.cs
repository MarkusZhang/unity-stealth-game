﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {

	public float maxSpeed = 20;
	public float minSpeed = 5;
	public float speed;
	public int damage = 1;

	public GameObject explosionEffect;

	// Use this for initialization
	void Start () {
		speed = Mathf.Lerp (minSpeed, maxSpeed, Difficulty.GetDifficultyPercent ());
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.down * speed * Time.deltaTime);

		if (transform.position.y < -Camera.main.orthographicSize) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "player") {
			other.gameObject.GetComponent<Player> ().TakeDamage (damage);
			ExplodeAndDestroy ();
		} else if (other.tag == "bullet") {
			Destroy (other.gameObject);
			ExplodeAndDestroy ();
			ScoreCtrl.AddScore ();
		} else if (other.tag == "weapon") {
			ExplodeAndDestroy ();
			ScoreCtrl.AddScore ();
		}
	}

	public void ExplodeAndDestroy(){
		Instantiate (explosionEffect, transform.position, transform.rotation); // destroy animation
		Destroy (gameObject);
	}

}
