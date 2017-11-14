using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	public float timeLimit;

	public virtual void powerup(Player p, Environment e) {}
	public virtual void powerdown(Player p, Environment e) {}

}