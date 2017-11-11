using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePowerup : Powerup {

	override public void powerup(Player p) {
		Time.timeScale /= 2;
	}

	override public void powerdown(Player p) {
		Time.timeScale *= 2;
	}

}