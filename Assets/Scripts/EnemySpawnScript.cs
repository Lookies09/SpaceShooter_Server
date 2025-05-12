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

            
                // ���� �ؽ�Ʈ ���ӿ�����Ʈ Ȱ��ȭ
                waveText.gameObject.SetActive(true);

                waveText.text = $"Wave {level + 1}";
                yield return new WaitForSeconds(2f);

                waveText.gameObject.SetActive(false);
            

            

            int enemySpawnCount = enemyCountsByLevel[level];

            while (enemySpawnCount > 0) // ������ ������ ���� ��
            {
                
                enemyLeftText.text = $"{enemySpawnCount - 1}";
                

                

                // ���� �������� ������ ���������� ���� �ε��� ��÷��
                int enemyIndex = Random.Range(0, enemyIndexsByLevel[level]);

                // ������ ���� ��ġ �ε����� ��÷��
                int spawnTrIndex = Random.Range(0, enemySpawnpos.Length);

                Instantiate(enemyPrefabs[enemyIndex], enemySpawnpos[spawnTrIndex].position, Quaternion.identity);

                enemySpawnCount--; // ���� ī��Ʈ ����

                yield return new WaitForSeconds(spawnDelayTime); // ���� ���� ����
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


        
        


        // ���� ���� ó�� 8

    }

}
