using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInfo {

	public Powerup powerup;
	public float timeSinceObtainment;
	public float timeLimit;

	public PowerupInfo(Powerup p, float limit) {
		powerup = p;
		timeLimit = limit;
		timeSinceObtainment = 0;
	}

}