using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform[] firepos;

    [SerializeField] private float delayTime;

    [SerializeField] public bool doubleFire;

    [SerializeField] private float doubleFireTime;


    public void Start()
    {
        StartCoroutine("DelayFire");
    }

   

    IEnumerator DelayFire()
    {
        while (true)
        {
            

            for (int i = 0; i < firepos.Length; i++)
            {
                


                if (doubleFire == false)
                {
                    
                    Instantiate(bulletPrefab, firepos[i].position, firepos[i].rotation);
                }
                else
                {
                    
                    Instantiate(bulletPrefab, firepos[i].position, firepos[i].rotation);

                    yield return new WaitForSeconds(doubleFireTime);
                    
                    Instantiate(bulletPrefab, firepos[i].position, firepos[i].rotation);
                    
                }

                yield return new WaitForSeconds(delayTime);

            }
                                               
            
           

            
        }

    }

}
