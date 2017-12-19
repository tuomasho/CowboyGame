using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy : MonoBehaviour {

    public int hp = 3;

    private Transform target, gunpoint;
    private Animator anim;
    private NavMeshAgent meshAgent;
    private GameObject manager;
    private bool isDead = false, isShooting = false;
    private float fireDelay = 3f, bulletSpeed = 40f;

    [SerializeField]
    private float sightDistance, sightHeight;
    [SerializeField]
    private GameObject bulletPrefab, heartPrefab;

    void Start () {
        anim = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        manager = GameObject.FindGameObjectWithTag("Manager");
        gunpoint = gameObject.transform.GetComponentsInChildren<Transform>(true).FirstOrDefault(t => t.name == "GunPoint");
    }
    void FixedUpdate ()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * sightHeight, transform.forward * sightDistance, Color.blue);

        if (Physics.Raycast(transform.position + Vector3.up * sightHeight, transform.forward, out hit, sightDistance) && !isShooting)
        {
            if (hit.collider.gameObject.tag == "Player" && fireDelay < 0)
            {
                fireDelay = 10f;
                StartCoroutine(Shooting());
            }
            else
            {
                fireDelay -= Time.deltaTime;
            }
        }
        if (hp == 0)
        {
            meshAgent.isStopped = true;
            Die();
            return;
        }
        else if(hp > 0 && !isDead && !isShooting && !manager.GetComponent<ManagerScript>().playerDead)
        {
            fireDelay -= Time.deltaTime;
            meshAgent.SetDestination(target.position);
        }
        else if(manager.GetComponent<ManagerScript>().playerDead)
        {
            StartCoroutine(GameEnding());
        }

	}

    void Die()
    {
        Debug.Log("die");
        isDead = true;
        hp--;
        anim.SetBool("isDead", true);

        manager.GetComponent<ManagerScript>().aliveGangsters--;
        if(manager.GetComponent<ManagerScript>().aliveGangsters == 0)
        {
            manager.GetComponent<ManagerScript>().roundStarted = false;
        }

        int rand = Random.Range(1, 5);

        Debug.Log(rand);

        if(rand == 1)
        {
            Instantiate(heartPrefab, GetComponentInParent<Transform>().position, transform.rotation);
        }

        Destroy(this.gameObject, 5f);
    }
    IEnumerator Shooting()
    {
        meshAgent.isStopped = true;
        isShooting = true;
        anim.SetBool("isShooting", true);
        yield return new WaitForSeconds(0.55f);

        //Shooting
        var clone = Instantiate(bulletPrefab, gunpoint.position, gunpoint.rotation);
        clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        Destroy(clone.gameObject, 2f);

        anim.SetBool("isShooting", false);
        meshAgent.isStopped = false;
        isShooting = false;
    }

    IEnumerator GameEnding()
    {
        meshAgent.isStopped = true;
        anim.SetBool("playerDead", true);

        yield return new WaitForSeconds(5f);

        Destroy(this.gameObject);
    }
}
