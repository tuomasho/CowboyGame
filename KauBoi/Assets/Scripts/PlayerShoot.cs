using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    [SerializeField]
    private GameObject fireStart;
    [SerializeField]
    private float animDelay = 1f, fireDelay = 1.5f, bulletSpeed = 20f, reloadTime = 2f;

    public bool isShooting = false, isReloading = false;
    public GameObject bulletPrefab;

    public float ammo = 6f;

    static Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        fireDelay = 0f;
    }

	void FixedUpdate () {

        if (isReloading)
            return;

        if(ammo == 0)
        {
            StartCoroutine(Reload());
            fireDelay -= Time.deltaTime;
            return;
        }
        else
        {
            //if (Input.GetButton("Fire1") && fireDelay < 0)

            if(CnInputManager.GetButton("Jump") && fireDelay < 0)
            {
                fireDelay = 1.5f;
                isShooting = true;
                StartCoroutine(Shoot());
            }
            else
            {
                fireDelay -= Time.deltaTime;
            }
        }

    }

    IEnumerator Shoot()
    {
        //Shooting anim on
        anim.SetBool("isShooting", true);
        yield return new WaitForSeconds(animDelay);

        //Shooting
        ammo--;
        var clone = Instantiate(bulletPrefab, fireStart.transform.position, fireStart.transform.rotation);
        clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        Destroy(clone.gameObject, 2f);

        //Stop shooting + shooting anim off
        isShooting = false;
        anim.SetBool("isShooting", false);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        ammo = 6;
        isReloading = false;
    }
}
