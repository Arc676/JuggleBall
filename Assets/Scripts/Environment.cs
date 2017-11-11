using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

	[SerializeField] private GameObject ballPrefab;
	private List<GameObject> balls = new List<GameObject>();

	private float timeSinceLastSpawn = 0;

	void Start() {
		spawnBall();
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
		balls.Add(
			obj
		);
	}

	void Update() {
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn > 10) {
			timeSinceLastSpawn = 0;
			spawnBall();
		}
		for (int i = 0; i < balls.Count;) {
			GameObject obj=  balls[i];
			if (obj.transform.position.y < -5) {
				balls.Remove(obj);
				Destroy(obj);
			} else {
				i++;
			}
		}
	}

}