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
	}

	void newGame() {
		gameIsOver = false;
		gameOverLabel.gameObject.SetActive(false);
		env.newGame ();
	}

	void Update () {
		if (gameIsOver) {
			if (Input.anyKey) {
				newGame ();
			}
			return;
		}
		Vector2 pos = gameObject.transform.position;
		Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.x = mouse.x;
		mouseY = mouse.y;
		gameObject.transform.position = pos;
		if (env.getBallCount() <= 0) {
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
			float dx = ball.transform.position.x - transform.position.x;
			ball.getRigidBody().AddForce(
				new Vector2(dx * 50, 100 * (mouseY - gameObject.transform.position.y))
			);
			updateScore((int)(ball.getAirtime() * env.getBallCount()));
			ball.resetAirtime();
		}
	}

}
