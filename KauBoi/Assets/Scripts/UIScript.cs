using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public Text ammoCount, roundCount, gangsterCount, liveCount;

    private ManagerScript manager;
    private PlayerShoot player;

	void Start () {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
    }
	
	void Update () {
        ammoCount.text = player.ammo + "/";
        roundCount.text = "Round: " + manager.roundNumber;
        gangsterCount.text = " X " + manager.aliveGangsters;
        liveCount.text = " X " + manager.playerHp;
	}
}
