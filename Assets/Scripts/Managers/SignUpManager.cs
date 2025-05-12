using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SignUpManager : MonoBehaviour
{
    // ���̵� �Է°�
    [SerializeField] private InputField idInputField;
    
    // �г��� �Է°�
    [SerializeField] private InputField nickInputField;

    // ��й�ȣ �Է°�
    [SerializeField] private InputField pwInputField;

    // ��й�ȣ ���Է�
    [SerializeField] private InputField repwInputField;

    // �α� �޽��� �ؽ�Ʈ
    [SerializeField] private Text logMessageText;

    // ȸ�� ���� ���� ����Ƽ �̺�Ʈ
    NetworkLogTextEvent logTextEvent = new NetworkLogTextEvent();
    NetworkResponseDataEvent responseJoinDataEvent = new NetworkResponseDataEvent();

    private void OnEnable()
    {
        // �α� ��� �̺�Ʈ ����
        logTextEvent.AddListener(OnPrintLogText);
        // ȸ������ �Ϸ� �ݹ� �̺�Ʈ ����
        responseJoinDataEvent.AddListener(OnJoinCompleteData);
    }

    private void OnDisable()
    {
        // �α� ��� �̺�Ʈ ���� ����
        logTextEvent.RemoveListener(OnPrintLogText);
        // ȸ������ �Ϸ� �ݹ� �̺�Ʈ ���� ����
        responseJoinDataEvent.RemoveListener(OnJoinCompleteData);
    }

    // ȸ�� ���� �Ϸ� �̺�Ʈ �ݹ� �޼ҵ�
    public void OnJoinCompleteData(string responseTextData)
    {
        // ���� JSON ������
        string result = responseTextData.Trim();

        Debug.Log("resopnse data : " + result);

        // LitJson�� �̿��� ��ųʸ�/�迭 ��ü ����
        JsonData data = JsonMapper.ToObject(result);

        // �޽��� �� ����
        string message = (string)data["message"];

        // �α��� �Ϸ� �޽��� ���
        logMessageText.text = message;

        // ���� ��� ���� ���� ���
        int status = (int)data["status"];

        // ȸ�������� ���� ó�� �Ǿ��ٸ�
        if (status == 1000)
        {
            // {"status":1000,"error":false,"message":"ȸ�� ������ �Ϸ��Ͽ����ϴ�.","validate":null,"data":{"uid":11}}

            int userId = (int)data["data"]["uid"]; // ���� ���̵� ����
            logMessageText.text = $"{userId} ���̵� ���� ȸ�������� �Ϸ�Ǿ����ϴ�.";
            Debug.Log(logMessageText.text);

            // �α��� ������ �̵�
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            // ȸ�� �α��� ���� �޽��� ���
            logMessageText.text = (string)data["message"];
        }
    }

    public void OnPrintLogText(string log)
    {
        logMessageText.text = log;
    }





    public void OnJoinButtonClick()
    {
        // ���� ���� �Է°��� ������
        string userId = idInputField.text.Trim();
        string userNick = nickInputField.text.Trim();
        string userPw = pwInputField.text.Trim();
        string userRePw = repwInputField.text.Trim();

        if (userId.Length < 4)
        {
            logMessageText.text = "���̵�� �ּ� 4�� �̻��Դϴ�.";
            return;
        }

        if (userNick.Length < 4)
        {
            logMessageText.text = "�г����� �ּ� 4�� �̻��Դϴ�.";
            return;
        }

        if (userPw.Length <= 0)
        {
            logMessageText.text = "��й�ȣ�� �Է��ؾ� �մϴ�.";
            return;
        }

        if (!userRePw.Equals(userPw))
        {
            logMessageText.text = "��й�ȣ Ȯ�� ������ Ʋ���ϴ�.";
            return;
        }

        // ������ ���� ȸ�� ������ ��û��
        NetAPIManager.instance.UserJoin(userId, userPw, userNick, logTextEvent, responseJoinDataEvent);
    }

    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}
