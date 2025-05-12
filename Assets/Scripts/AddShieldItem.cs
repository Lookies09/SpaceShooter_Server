using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddShieldItem : MonoBehaviour
{
    

    private int skillUseTime;

    private void Awake()
    {
        
        skillUseTime = PlayerShieldSkill.skillUseTime;

    }



    void OnTriggerEnter2D(Collider2D collision)
    {        

        if (collision.tag == "Player")
        {
            skillUseTime++;
            PlayerShieldSkill.skillUseTime = skillUseTime;
            Destroy(gameObject);

        }

    }
}
