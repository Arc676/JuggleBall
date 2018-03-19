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

public class Environment : MonoBehaviour {

	[SerializeField] private Player player;

	[SerializeField] private GameObject ballPrefab;
	private int ballCount = 0;
	private float timeSinceLastSpawn = 0;
	public float spawnTime = 10;
	private int ballsDropped = 0;

	private List<PowerupInfo> powerups = new List<PowerupInfo>();
	[SerializeField] private GameObject[] powerupPrefabs;
	private float timeSinceLastPowerupSpawn = 0;
	public float powerupSpawnTime = 8;

	void Start() {
		spawnBall();
	}

	public void newGame() {
		ballCount = 0;
		timeSinceLastSpawn = 0;
		ballsDropped = 0;
		timeSinceLastPowerupSpawn = 0;
		spawnBall ();
	}

	public int getBallCount() {
		return ballCount;
	}

	public void obtainPowerup(Powerup p) {
		p.powerup(player, this);
		powerups.Add(
			new PowerupInfo(p, p.timeLimit)
		);
	}

	public void spawnBall() {
		GameObject obj = (GameObject) Instantiate(
			ballPrefab,
			new Vector2(0, 5),
			Quaternion.identity
		);
		obj.GetComponent<Rigidbody2D>().AddForce(
			new Vector2(
				Random.Range(-100.0f, 100.0f),
				0
			)
		);
		obj.GetComponent<Ball>().setEnvironment(this);
		ballCount++;
	}

	private void spawnPowerup(int index) {
		if (index < 0 || index >= powerupPrefabs.Length) {
			return;
		}
		Instantiate(
			powerupPrefabs[index],
			new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-4.0f, 5.0f)),
			Quaternion.identity
		);
	}

	private void spawnRandomPowerup() {
		spawnPowerup(
			Random.Range(0, powerupPrefabs.Length)
		);
	}

	public void ballDropped() {
		if (player.getGameOver()) {
			return;
		}
		ballsDropped++;
		if (ballsDropped > 20) {
			ballsDropped = 0;
			player.gameOver();
		}
		ballCount--;
	}

	void powerdown(PowerupInfo pi) {
		powerups.Remove(pi);
		pi.powerup.powerdown(player, this);
		Destroy(pi.powerup.gameObject);
	}

	public void clearAllPowerups() {
		while (powerups.Count > 0) {
			powerdown(powerups[0]);
		}
	}

	void Update() {
		if (player.getGameOver()) {
			return;
		}
		float dt = Time.deltaTime;
		for (int i = 0; i < powerups.Count;) {
			PowerupInfo pi = powerups[i];
			pi.timeSinceObtainment += Time.deltaTime;
			if (pi.timeSinceObtainment > pi.timeLimit) {
				powerdown(pi);
			} else {
				i++;
			}
		}
		timeSinceLastSpawn += dt;
		if (timeSinceLastSpawn > spawnTime) {
			timeSinceLastSpawn = 0;
			spawnBall();
		}
		timeSinceLastPowerupSpawn += dt;
		if (timeSinceLastPowerupSpawn > powerupSpawnTime) {
			timeSinceLastPowerupSpawn = 0;
			if (Random.Range(0, 100) < 50) {
				spawnRandomPowerup();
			}
		}
	}

}