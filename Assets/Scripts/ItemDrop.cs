using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{

    [SerializeField] private GameObject[] Items;


    private int itemNum;




    public void Drop()
    {

        itemNum = Random.Range(0, 3);
        Instantiate(Items[itemNum], transform.position, Quaternion.identity);

    }

}
