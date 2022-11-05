using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AI : MonoBehaviour
{

    public float walkSpeed;
    public float turnSpeed;
    public float range;

    public GameObject[] patrolPoints;
    public GameObject player;

    enum behaviour
    {
        chase,
        patrol
    }
    behaviour current;
    Rigidbody rb;
    GameObject target;
    int index;

    // Start is called before the first frame update
    void Start()
    {   
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (patrolPoints == null)
            patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoints");

        index = 0;
        current = behaviour.patrol;
        target = patrolPoints[index];

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (current)
        {
            case behaviour.patrol:
                if(Vector3.Distance(transform.position, target.transform.position) < 1f)
                {
                    index += 1;
                    if(index >= patrolPoints.Length)
                    {
                        index = 0;
                    }

                    target = patrolPoints[index];
                }
                if(Vector3.Distance(transform.position, player.transform.position) < range)
                {
                    current = behaviour.chase;
                    target = player;
                }
                break;

            case behaviour.chase:
                
                if (Vector3.Distance(transform.position, target.transform.position) > range)
                {
                    current = behaviour.patrol;
                    target = patrolPoints[index];
                }
                break;
        }

        transform.LookAt(target.transform);
        transform.Translate(0, 0, walkSpeed * Time.deltaTime);


    }
}
