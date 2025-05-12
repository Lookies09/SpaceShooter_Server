using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPlayerMovement : DirectionMovement
{
    // Start is called before the first frame update
    void Start()
    {
        direction = GameObject.Find("PlayerShip").transform.position - transform.position ;
        direction = direction.normalized;

        
    }

}
