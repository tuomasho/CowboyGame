using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private GameObject target;
    private float smoothTime = 0.3f;
    private ManagerScript manager;
    private Vector3 velocity = Vector3.zero, startPos;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerScript>();
        startPos.z = target.transform.position.z - 23.9f;
        startPos.x = target.transform.position.x - 25.3f;
        transform.position = new Vector3(startPos.x, transform.position.y, startPos.z);
    }

	void Update () {

        if (!manager.playerDead)
        {
            Vector3 goalPos;
            goalPos.y = transform.position.y;
            goalPos.z = target.transform.position.z - 23.9f;
            goalPos.x = target.transform.position.x - 25.3f;
            transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);
        }
	}
}
