using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHp : ObjectHp
{
    public bool isDead = false;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public override void Dead(float destroyTime)
    {
        isDead = true;

        base.Dead(1f);

        gameManager.GameOver(isDead, 1);
    }

}
