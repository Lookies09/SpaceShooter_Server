using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMiniShipItem : MonoBehaviour
{

    public GameObject miniShipsPrefab;





    void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.tag == "Player")
            {
                
                {

                    Instantiate(miniShipsPrefab, GameObject.Find("PlayerShip").transform.position, Quaternion.identity);



                }

                Destroy(gameObject);


            }

       

    }


    
}
