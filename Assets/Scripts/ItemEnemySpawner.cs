using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private Transform[] enemySpawnpos;

    [SerializeField] private int[] enemyIndexsByLevel;

    [SerializeField] private int[] enemyCountsByLevel;

    private int level = 0;

    [SerializeField] private int finalLevel;

    [SerializeField] private float spawnDelayTime;

    private void Start()
    {
        StartCoroutine("EnemySpawnCoroutine");
    }


    IEnumerator EnemySpawnCoroutine()
    {
        while (level < finalLevel)
        {

            yield return new WaitForSeconds(10);





            int enemySpawnCount = enemyCountsByLevel[level];

            while (enemySpawnCount > 0) // 레벨당 나오는 적기 수
            {





                // 현재 레벨에서 출현할 적군프리팹 랜덤 인덱스 추첨함
                int enemyIndex = Random.Range(0, enemyIndexsByLevel[level]);

                // 랜덤한 생성 위치 인덱스를 추첨함
                int spawnTrIndex = Random.Range(0, enemySpawnpos.Length);

                Instantiate(enemyPrefabs[enemyIndex], enemySpawnpos[spawnTrIndex].position, enemyPrefabs[enemyIndex].transform.rotation);

                enemySpawnCount--; // 생성 카운트 감소

                yield return new WaitForSeconds(spawnDelayTime); // 다음 생성 지연
            }

            level++;
        }



        // 게임 종료 처리 8

    }

}
