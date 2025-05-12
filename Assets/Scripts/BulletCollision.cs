using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public string[] nonIgnoreTagName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < nonIgnoreTagName.Length; i++)
        {
            if (collision.tag == nonIgnoreTagName[i])
            {
                Destroy(gameObject);

            }
        }
    }
}
