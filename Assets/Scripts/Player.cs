using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	[SerializeField] private BoxCollider2D boxCol;
	[SerializeField] private Environment env;

	private float mouseY;

	[SerializeField] private Text scoreLabel;
	public int score = 0;

	void Update () {
		Vector2 pos = gameObject.transform.position;
		Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.x = mouse.x;
		mouseY = mouse.y;
		gameObject.transform.position = pos;
	}

	void OnCollisionEnter2D(Collision2D colInfo) {
		Ball ball = colInfo.gameObject.GetComponent<Ball>();
		if (ball) {
			ball.getRigidBody().AddForce(
				new Vector2(0, 100 * (mouseY - gameObject.transform.position.y))
			);
			score += (int)(ball.getAirtime() * env.getBallCount());
			ball.resetAirtime();
			scoreLabel.text = "Score: " + score;
		}
	}

}