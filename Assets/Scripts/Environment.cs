using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

	[SerializeField] private Player player;

	[SerializeField] private GameObject ballPrefab;
	private int ballCount = 0;
	private float timeSinceLastSpawn = 0;
	public float spawnTime = 10;

	private List<PowerupInfo> powerups = new List<PowerupInfo>();
	[SerializeField] private GameObject[] powerupPrefabs;

	void Start() {
		spawnBall();
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

	private void spawnBall() {
		GameObject obj = (GameObject) Instantiate(
			ballPrefab,
			new Vector2(0, 5),
			Quaternion.identity
		);
		obj.GetComponent<Rigidbody2D>().AddForce(
			new Vector2(
				Random.Range(-100, 100),
				0
			)
		);
		obj.GetComponent<Ball>().setEnvironment(this);
		ballCount++;
	}

	public void ballDropped() {
		ballCount--;
	}

	void Update() {
		timeSinceLastSpawn += Time.deltaTime;
		for (int i = 0; i < powerups.Count;) {
			PowerupInfo pi = powerups[i];
			pi.timeSinceObtainment += Time.deltaTime;
			if (pi.timeSinceObtainment > pi.timeLimit) {
				powerups.Remove(pi);
				pi.powerup.powerdown(player, this);
				Destroy(pi.powerup.gameObject);
			} else {
				i++;
			}
		}
		if (timeSinceLastSpawn > spawnTime) {
			timeSinceLastSpawn = 0;
			spawnBall();
		}
	}

}