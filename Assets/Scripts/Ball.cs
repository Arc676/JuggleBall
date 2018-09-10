//Written by Alessandro Vinciguerra <alesvinciguerra@gmail.com>
//Copyright (C) 2017-8  Arc676/Alessandro Vinciguerra

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation (version 3).

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
//See README.md and LICENSE for more details

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	[SerializeField] private Rigidbody2D rigidBody;
	[SerializeField] private AudioSource powerupSound;
	private float airtime;

	private Environment env;

	public Rigidbody2D getRigidBody() {
		return rigidBody;
	}

	public void setEnvironment(Environment env) {
		this.env = env;
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
			v.x = Mathf.Abs(v.x);
			if (x > Screen.width) {
				v.x *= -1;
			}
			rigidBody.velocity = v;
		}
		if (gameObject.transform.position.y < -5) {
			env.ballDropped();
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Powerup p = other.GetComponent<Powerup>();
		if (p) {
			powerupSound.Play();
			env.obtainPowerup(p);
			other.gameObject.SetActive(false);
		}
	}

}
