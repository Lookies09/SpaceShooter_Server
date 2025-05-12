using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class PlayerShipHp : ObjectHp
{
    [SerializeField] private GameManager gameManager;

    private float PlayerHpto1Num;

    [SerializeField] private Text hpText;

    public static int Playerhp;

    [SerializeField] private Image hpProgressUI;

    public bool isDead = false;

    public void Start()
    {
        hp = Playerhp;
        PlayerHpto1Num = 1f / hp;
        
    }

    private void LateUpdate()
    {
        
        ShowHpProgress();

        hpText.text = $"Hp : {hp}";
    }

    public override void MinusHp(int Damage)
    {
        base.MinusHp(Damage);


    }

    public override void Dead(float destroyTime)
    {
        isDead= true;
        base.Dead(1f);
        gameManager.GameOver(isDead, 0);
    }


    public void ShowHpProgress()
    {

        hpProgressUI.fillAmount = hp * PlayerHpto1Num;

    }






}
