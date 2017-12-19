using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [SerializeField]
    float moveSpeed = 4f;
    Rigidbody rb;

    static Animator anim;
    Vector3 forward, right;

    static PlayerShoot ps;

	void Start ()
    {
        ps = GetComponent<PlayerShoot>();
        anim = GetComponent<Animator>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

	}
    /*
        void FixedUpdate () {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if(!ps.isShooting)
                    Move();  
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

        }
    */

    void FixedUpdate()
    {
        if (CnInputManager.GetAxis("Horizontal") != 0 || CnInputManager.GetAxis("Vertical") != 0)
        {
            if (!ps.isShooting)
                Move();
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

    }

    void Move()
    {
        // Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        // Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * CnInputManager.GetAxis("Horizontal");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * CnInputManager.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        
        transform.position += rightMovement;
        transform.position += upMovement;

        if(upMovement != new Vector3(0,0,0))
        {
            transform.forward = heading;
            anim.SetBool("isWalking", true);
        }
        else if (rightMovement != new Vector3(0,0,0))
        {
            transform.forward = heading;
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}
