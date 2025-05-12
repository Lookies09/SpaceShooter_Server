using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    private ObjectHp objectHp;

    private ObjectDamage objectDamage;

    [SerializeField] private string[] ignoreTagName;

    [SerializeField] private string[] nonIgnoreTagName;

    [SerializeField] private GameObject crushEffect;
        


    public void Awake()
    {
        objectHp = GetComponent<ObjectHp>();
        
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < nonIgnoreTagName.Length; i++)
        {           

            if (collision.tag == nonIgnoreTagName[i])
            {
                objectDamage = collision.GetComponent<ObjectDamage>();
                int objectDMG = objectDamage.objectDMG;

                Instantiate(crushEffect, collision.transform.position, Quaternion.identity);

                objectHp.MinusHp(objectDMG);
                
            }

        }


        for (int i = 0; i < ignoreTagName.Length; i++)
        {

            if (collision.tag == ignoreTagName[i]) return;

        }
       



    }


}
