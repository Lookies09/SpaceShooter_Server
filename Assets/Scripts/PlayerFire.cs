using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefabs;

    [SerializeField] protected Transform[] firePos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 0; i < firePos.Length; i++)
            {
                Instantiate(bulletPrefabs, firePos[i].position, Quaternion.identity);
            }
           
        }


    }
}
