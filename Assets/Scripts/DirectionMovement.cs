using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMovement : MonoBehaviour
{
    [SerializeField] protected float speed;

    [SerializeField] protected Vector2 direction;


    // Update is called once per frame
    public virtual void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);


    }
}
