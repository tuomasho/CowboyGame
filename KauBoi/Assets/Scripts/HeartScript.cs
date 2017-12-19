using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour {

    private ManagerScript manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerScript>();
        Destroy(this.gameObject, 20f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            manager.playerHp++;
            Destroy(this.gameObject);
        }
    }
}
