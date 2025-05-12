using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevelManager : MonoBehaviour
{



    public void clickButton1()
    {
        PlayerShipHp.Playerhp = 200;
        PlayerShieldSkill.skillUseTime = 3;
        SceneManager.LoadScene("InGameScene");
    }

    public void clickButton2()
    {
        PlayerShipHp.Playerhp = 100;
        PlayerShieldSkill.skillUseTime = 2;
        SceneManager.LoadScene("InGameScene");
    }

    public void clickButton3()
    {
        PlayerShipHp.Playerhp = 50;
        PlayerShieldSkill.skillUseTime = 1;
        SceneManager.LoadScene("InGameScene");
    }
}
