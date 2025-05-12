using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    // �α� �޽��� �ؽ�Ʈ
    [SerializeField] private Text logMessageText;

    [SerializeField] private Transform rankScrollView;
    [SerializeField] private GameObject rankCellPrefab;

    // ��ŷ ��� ���� ���� ����Ƽ �̺�Ʈ
    NetworkLogTextEvent logTextEvent = new NetworkLogTextEvent();
    NetworkResponseDataEvent responseRankDataEvent = new NetworkResponseDataEvent();

    private void OnEnable()
    {
        // �̺�Ʈ ����
        logTextEvent.AddListener(OnPrintLogText);
        responseRankDataEvent.AddListener(OnRankCompleteData);
    }

    private void OnDisable()
    {
        // �̺�Ʈ ���� ����
        logTextEvent.RemoveListener(OnPrintLogText);
        responseRankDataEvent.RemoveListener(OnRankCompleteData);
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score : " + GameManager.point.ToString();

        // ���� ��ŷ�� ��ȸ��
        NetAPIManager.instance.UserRankList(logTextEvent, responseRankDataEvent);
    }

    // �α� �޽��� ���
    public void OnPrintLogText(string log)
    {
        logMessageText.text = log;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {

            SceneManager.LoadScene("SelectLevel");

        }

    }

    // ��ũ ����Ʈ ������ �����
    public void OnRankCompleteData(string responseTextData)
    {
        string result = responseTextData.Trim();

        Debug.Log("resopnse data : " + result);

        // �ݹ����� �Ѱܹ��� JSON �����͸� ������ �����迭 ��ü�� ������
        JsonData data = JsonMapper.ToObject(result);

        // �޽��� ������ ����
        string message = (string)data["message"];

        logMessageText.text = message;

        // ��� ���� ���� ����
        int status = (int)data["status"];

        // ��ũ ��ȸ ���� ó��
        if (status == 4000)
        {
            Debug.Log("��ũ ����Ʈ �ε�");

            // ��ũ �迭 �������� ���� ������ �����Ͽ� ���� �����ϰ� �����
            for (int i = 0; i < data["data"].Count; i++)
            {
                int rank = (int)data["data"][i]["rank"];
                string nick = (string)data["data"][i]["nick"];
                string bestScore = (string)data["data"][i]["bestscore"];

                Debug.Log($"{rank}, {nick}, {bestScore}");

                // ��ũ ���� ǥ�� �� ����
                UserRankCell userRankCell = Instantiate(rankCellPrefab, rankScrollView).GetComponent<UserRankCell>();
                userRankCell.Init(rank.ToString(), nick, bestScore);
            }
        }
        else
        {
            // ��ũ ��ȸ ���� �޽��� ���
            logMessageText.text = (string)data["message"];
        }
    }
}
