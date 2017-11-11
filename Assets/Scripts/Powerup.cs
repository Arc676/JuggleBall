using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	public float timeLimit;

	public virtual void powerup(Player p) {}
	public virtual void powerdown(Player p) {}

}