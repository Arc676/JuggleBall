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
	public int scoreFactor = 1;

	private bool gameIsOver = false;
	[SerializeField] private Text gameOverLabel;

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

	public bool getGameOver() {
		return gameIsOver;
	}

	public void gameOver() {
		if (score > hiScore) {
			updateHiScore(score);
		}
		updateScore(-score);
		gameIsOver = true;
		gameObject.transform.position = new Vector2(-10, -4.5f);
		gameOverLabel.gameObject.SetActive(true);
		env.clearAllPowerups();
	}

	void Update () {
		if (gameIsOver) {
			if (Input.anyKey) {
				gameIsOver = false;
				env.spawnBall();
				gameOverLabel.gameObject.SetActive(false);
			}
			return;
		}
		Vector2 pos = gameObject.transform.position;
		Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.x = mouse.x;
		mouseY = mouse.y;
		gameObject.transform.position = pos;
		if (env.getBallCount() == 0) {
			gameOver();
		}
	}

	private void updateScore(int delta) {
		score += delta * (delta > 0 ? scoreFactor : 1);
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
