using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectHp : MonoBehaviour
{
    [SerializeField] protected GameObject destroyAnimePrefab;

    [SerializeField] protected Transform explosionPos;
    [SerializeField] protected GameObject explosionEffectPrefab;
     
    public int hp;



    public void Update()
    {
        DestroyPrefab(destroyAnimePrefab);
        DestroyPrefab(explosionEffectPrefab);

        
    }

    public void DestroyPrefab(GameObject gameObject)
    {
        Destroy(GameObject.Find(gameObject.name + "(Clone)"), 1f);

    }


    public virtual void MinusHp(int Damage)
    {

        hp -= Damage;

        if (hp > 0) return;

        Dead(0.01f);
    }

    public virtual void Dead(float destroyTime)
    {
        
            hp = 0;

            Instantiate(destroyAnimePrefab, transform.position, Quaternion.identity);

            Instantiate(explosionEffectPrefab, explosionPos.position, Quaternion.identity);

            if (gameObject.name == "ItemEnemy(Clone)")
            {
                ItemDrop itemDrop = gameObject.GetComponent<ItemDrop>();
                itemDrop.Drop();
            }

            ObjectScore objectScore = GetComponent<ObjectScore>();
            int point = objectScore.point;

            GameManager.point += point;

            Destroy(gameObject, destroyTime);
        
    }



    public void PlusHp(int Heal)
    {
        hp += Heal;


    }
    
}
