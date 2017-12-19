using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHit : MonoBehaviour {

    public GameObject defHitEffect;
    public GameObject charHitEffect;
    public ManagerScript manager;
    private Enemy enemy;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerScript>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            var hit = Instantiate(charHitEffect, this.transform.position, Quaternion.LookRotation(this.transform.position.normalized));
            Destroy(hit, 2f);
            enemy = col.gameObject.GetComponent<Enemy>();
            enemy.hp--;
        }

        else if(col.gameObject.tag == "Player")
        {
            var hit = Instantiate(charHitEffect, this.transform.position, Quaternion.LookRotation(this.transform.position.normalized));
            manager.playerHp--;
            Destroy(hit, 2f);
        }

        else
        { 
        var hit = Instantiate(defHitEffect, this.transform.position, Quaternion.LookRotation(this.transform.position.normalized));
        Destroy(hit, 2f);
        }
        Destroy(this.gameObject);
        Debug.Log(col.gameObject.name);
    }
}
