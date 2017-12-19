using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour {

    public GameObject[] spawns;
    public GameObject gangster;
    public GameObject player;
    public GameObject gameOverScreen;
    public float aliveGangsters, roundNumber = 0;
    public bool roundStarted, playerDead;
    public float playerHp;

    [SerializeField]
    private Text lastRound, recordRound;

    [SerializeField]
    private float spawnAmmount;
    

    void Start () {
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);
        playerDead = false;
        roundStarted = false;
        playerHp = 5;
	}
	

	void FixedUpdate ()
    {
        if (aliveGangsters == 0 && !roundStarted)
        {
            roundNumber++;
            StartCoroutine(NewRound());
            return;
        }

        if(playerHp == 0)
        {
            StartCoroutine(PlayerDeath());
        }
	}

    IEnumerator PlayerDeath()
    {
        playerDead = true;
        player.GetComponent<Animator>().SetBool("isDead", true);
        yield return new WaitForSeconds(5f);
        Destroy(player);
        Time.timeScale = 0;

        lastRound.text = roundNumber.ToString();
        recordRound.text = roundNumber.ToString();
        gameOverScreen.SetActive(true);
    }

    IEnumerator NewRound()
    {
        roundStarted = true;
        //NOTIFICATION THAT NEW ROUND STARTS IN 5 SECONDS
        yield return new WaitForSeconds(10f);

        //Round Starts
        if(spawnAmmount < 20)
            spawnAmmount += 1 * (roundNumber / 2);

        for(int x = 0; x < spawnAmmount; x++)
        {
            int spawnNumber = Random.Range(0, 3);
            Instantiate(gangster, spawns[spawnNumber].transform);
            aliveGangsters++;
            //yield return new WaitForSeconds(2f);
        }

    }
}
