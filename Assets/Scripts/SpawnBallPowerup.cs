using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBallPowerup : Powerup {

	override public void powerup(Player p, Environment e) {
		e.spawnBall();
	}

	override public void powerdown(Player p, Environment e) {}

}