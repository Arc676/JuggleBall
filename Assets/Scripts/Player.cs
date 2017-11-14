using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	[SerializeField] private BoxCollider2D boxCol;
	[SerializeField] private Environment env;

	private float mouseY;

	[SerializeField] private Text scoreLabel;
	[SerializeField] private Text hiscoreLabel;
	private int hiScore = 0;
	public int score = 0;

	void updateHiScore(int score) {
		updateHiScore(score, true);
	}

	void updateHiScore(int score, bool save) {
		hiScore = score;
		hiscoreLabel.text = "High Score: " + hiScore;
		if (save) {
			PlayerPrefs.SetInt("HiScore", hiScore);
			PlayerPrefs.Save();
		}
	}

	void Start() {
		if (PlayerPrefs.HasKey("HiScore")) {
			updateHiScore(PlayerPrefs.GetInt("HiScore"), false);
		}
	}

	void Update () {
		Vector2 pos = gameObject.transform.position;
		Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.x = mouse.x;
		mouseY = mouse.y;
		gameObject.transform.position = pos;
		if (env.getBallCount() == 0) {
			if (score > hiScore) {
				updateHiScore(score);
			}
			updateScore(-score);
		}
	}

	private void updateScore(int delta) {
		score += delta;
		scoreLabel.text = "Score: " + score;
	}

	void OnCollisionEnter2D(Collision2D colInfo) {
		Ball ball = colInfo.gameObject.GetComponent<Ball>();
		if (ball) {
			ball.getRigidBody().AddForce(
				new Vector2(0, 100 * (mouseY - gameObject.transform.position.y))
			);
			updateScore((int)(ball.getAirtime() * env.getBallCount()));
			ball.resetAirtime();
		}
	}

}
