using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRatePowerup : Powerup {

	override public void powerup(Player p, Environment e) {
		e.spawnTime /= 2;
	}

	override public void powerdown(Player p, Environment e) {
		e.spawnTime *= 2;
	}

}