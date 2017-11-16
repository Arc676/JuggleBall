using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score2xPowerup : Powerup {

	override public void powerup(Player p, Environment e) {
		p.scoreFactor *= 2;
	}

	override public void powerdown(Player p, Environment e) {
		p.scoreFactor /= 2;
	}

}