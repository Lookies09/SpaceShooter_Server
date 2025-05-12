using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShieldSkill : MonoBehaviour
{
    [SerializeField] private GameObject PlayerShieldPrefab;

    public Text ShieldSkillUseText;

    public static int skillUseTime;

    public float duringTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.X) && skillUseTime > 0)
            {
                
                Instantiate(PlayerShieldPrefab, transform.position, Quaternion.identity);
                

                skillUseTime--;
            }

        Destroy(GameObject.Find("PlayerShield(Clone)"), duringTime);

        ShieldSkillUseText.text = $"Shield : {skillUseTime}";




    }


}
