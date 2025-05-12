using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static int point = 0;
    [SerializeField] Text scroeText;

    [SerializeField] private Text playerNameText;

    NetworkResponseDataEvent responseScoreUpdateDataEvent = new NetworkResponseDataEvent();

    private void OnEnable()
    {
        // �̺�Ʈ ����
        responseScoreUpdateDataEvent.AddListener(OnUserScoreCompleteData);
    }

    private void OnDisable()
    {
        // �̺�Ʈ ���� ����
        responseScoreUpdateDataEvent.RemoveListener(OnUserScoreCompleteData);
    }

    private void Awake()
    {        
        point = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        string playerName = PlayerPrefs.GetString("USER_NICK", "�÷��̾�");
        playerNameText.text = playerName;
    }

    // Update is called once per frame
    void Update()
    {

        Destroy(GameObject.Find("CrushEffect(Clone)"), 0.3f);

        scroeText.text = point.ToString();
    }

    public void GameOver(bool isDead, int num)
    {
        if (isDead && num == 0)
        {
            string id = PlayerPrefs.GetString("USER_ID");
            int score = point;

            // ������ ���� ������ ������Ʈ ��û��
            NetAPIManager.instance.UserScoreUpdate(id, score.ToString(), null, responseScoreUpdateDataEvent);

            // ��������� �̵�
            SceneManager.LoadScene("EndScene");
        }

        if (isDead && num == 1)
        {
            string id = PlayerPrefs.GetString("USER_ID");
            int score = point;

            // ������ ���� ������ ������Ʈ ��û��
            NetAPIManager.instance.UserScoreUpdate(id, score.ToString(), null, responseScoreUpdateDataEvent);

            // ��������� �̵�
            SceneManager.LoadScene("ClearScene");
        }

    }

    public void OnUserScoreCompleteData(string responseTextData)
    {
        string result = responseTextData.Trim();

        Debug.Log("resopnse data : " + result);

        // �ݹ����� �Ѱܹ��� JSON �����͸� ������ �����迭 ��ü�� ������
        JsonData data = JsonMapper.ToObject(result);

        // �޽��� ������ ����
        string message = (string)data["message"];

        // ��� ���� ���� ����
        int status = (int)data["status"];

        // �α��ο� �����ߴٸ�
        if (status == 3000)
        {
            // ���� ���̵�� �г��� �����͸� ������
            string userId = (string)data["data"]["id"];

            Debug.Log("���� ���� ������Ʈ : " + userId);

            // ���Ӿ����� �̵�
            SceneManager.LoadScene("EndScene");
        }
        else
        {
            Debug.Log("���� ��� ���� �߻� : " + message);
        }
    }


}
