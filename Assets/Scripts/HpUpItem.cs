using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUpItem : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ObjectHp objectHp = GameObject.Find("PlayerShip").GetComponent<ObjectHp>();

            objectHp.PlusHp(20);

            Destroy(gameObject);

        }

        
    }



}
