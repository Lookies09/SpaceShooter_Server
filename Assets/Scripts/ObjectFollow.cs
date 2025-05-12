using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : DirectionMovement
{
    public GameObject playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("PlayerShip");

        

    }

    // Update is called once per frame
    public override void Update()
    {
        if (GameObject.Find("PlayerShip") == null)
        {
            direction = Vector2.down;

        }
        else
        {
            direction = playerTransform.transform.position - transform.position;
            direction = direction.normalized;
        }

        

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);

        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.1f);


    }




    private void LateUpdate()
    {
        base.Move();
    }
}
