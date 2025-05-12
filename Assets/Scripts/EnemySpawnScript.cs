using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private Transform[] enemySpawnpos;

    [SerializeField] private int[] enemyIndexsByLevel;

    [SerializeField] private int[] enemyCountsByLevel;

    private int level = 0;

    [SerializeField] private int finalLevel;

    [SerializeField] private Text waveText;

    [SerializeField] private Text enemyLeftText;

    [SerializeField] private float spawnDelayTime;

    private void Start()
    {
        StartCoroutine("EnemySpawnCoroutine");
    }


    IEnumerator EnemySpawnCoroutine()
    {
        while (level < finalLevel)
        {

            
                // 레벨 텍스트 게임오브젝트 활성화
                waveText.gameObject.SetActive(true);

                waveText.text = $"Wave {level + 1}";
                yield return new WaitForSeconds(2f);

                waveText.gameObject.SetActive(false);
            

            

            int enemySpawnCount = enemyCountsByLevel[level];

            while (enemySpawnCount > 0) // 레벨당 나오는 적기 수
            {
                
                enemyLeftText.text = $"{enemySpawnCount - 1}";
                

                

                // 현재 레벨에서 출현할 적군프리팹 랜덤 인덱스 추첨함
                int enemyIndex = Random.Range(0, enemyIndexsByLevel[level]);

                // 랜덤한 생성 위치 인덱스를 추첨함
                int spawnTrIndex = Random.Range(0, enemySpawnpos.Length);

                Instantiate(enemyPrefabs[enemyIndex], enemySpawnpos[spawnTrIndex].position, Quaternion.identity);

                enemySpawnCount--; // 생성 카운트 감소

                yield return new WaitForSeconds(spawnDelayTime); // 다음 생성 지연
            }

            level++;
        }

        
            enemyLeftText.text = "Something's coming...";
            yield return new WaitForSeconds(10f);
            enemyLeftText.color = Color.red;
            enemyLeftText.text = "The Boss";

            yield return new WaitForSeconds(2f);
            enemyLeftText.gameObject.SetActive(false);

            Instantiate(enemyPrefabs[enemyPrefabs.Length - 1], enemySpawnpos[8].position, Quaternion.identity);


        
        


        // 게임 종료 처리 8

    }

}
