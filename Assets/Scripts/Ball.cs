﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	[SerializeField] private Rigidbody2D rigidBody;
	private float airtime;

	public Rigidbody2D getRigidBody() {
		return rigidBody;
	}

	public void resetAirtime() {
		airtime = 0;
	}

	public float getAirtime() {
		return airtime;
	}

	void Update() {
		airtime += Time.deltaTime;
		Vector2 v = rigidBody.velocity;
		float x = Camera.main.WorldToScreenPoint(gameObject.transform.position).x;
		if (x < 0 || x > Screen.width) {
			v.x *= -1;
			rigidBody.velocity = v;
		}
	}

}