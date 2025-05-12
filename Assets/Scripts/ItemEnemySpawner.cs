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

            while (enemySpawnCount > 0) // ������ ������ ���� ��
            {





                // ���� �������� ������ ���������� ���� �ε��� ��÷��
                int enemyIndex = Random.Range(0, enemyIndexsByLevel[level]);

                // ������ ���� ��ġ �ε����� ��÷��
                int spawnTrIndex = Random.Range(0, enemySpawnpos.Length);

                Instantiate(enemyPrefabs[enemyIndex], enemySpawnpos[spawnTrIndex].position, enemyPrefabs[enemyIndex].transform.rotation);

                enemySpawnCount--; // ���� ī��Ʈ ����

                yield return new WaitForSeconds(spawnDelayTime); // ���� ���� ����
            }

            level++;
        }



        // ���� ���� ó�� 8

    }

}
