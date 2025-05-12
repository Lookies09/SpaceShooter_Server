using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMovement : DirectionMovement
{
    public float delaytime;

    private void Start()
    {
        StartCoroutine("BossMoveStopCoroutine");
    }


    IEnumerator BossMoveStopCoroutine()
    {
        Update();
        yield return new WaitForSeconds(delaytime);
        speed = 0;
    }
}
