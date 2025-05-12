using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;

public class StartSceneManager : MonoBehaviour
{
    // ���̵� �Է��ʵ�
    [SerializeField] private InputField idInputField;
    // ��� �Է� �ʵ�
    [SerializeField] private InputField pwInputField;

    // �α� �޽��� �ؽ�Ʈ
    [SerializeField] private Text logMessageText;

    // �α��� ���� ����Ƽ �̺�Ʈ
    NetworkLogTextEvent logTextEvent = new NetworkLogTextEvent();
    NetworkResponseDataEvent responseLoginDataEvent = new NetworkResponseDataEvent();

    private void OnEnable()
    {
        // �̺�Ʈ ����
        logTextEvent.AddListener(OnPrintLogText);
        responseLoginDataEvent.AddListener(OnLoginCompleteData);
    }

    private void OnDisable()
    {
        // �̺�Ʈ ���� ����
        logTextEvent.RemoveListener(OnPrintLogText);
        responseLoginDataEvent.RemoveListener(OnLoginCompleteData);
    }

    public void StartButtonClick()
    {
        string userId = idInputField.text.Trim();

        if (userId.Length < 4)
        {
            logMessageText.text = "���̵�� �ּ� 4�� �̻��Դϴ�.";
            return;
        }

        string userPw = pwInputField.text.Trim();

        if (userPw.Length <= 0)
        {
            logMessageText.text = "��й�ȣ�� �Է��ؾ� �մϴ�.";
            return;
        }

        // ���� �α��� ����
        NetAPIManager.instance.UserLogin(userId, userPw, logTextEvent, responseLoginDataEvent);
    }

    // �α� �޽��� ���
    public void OnPrintLogText(string log)
    {
        logMessageText.text = log;
    }

    public void OnLoginCompleteData(string responseTextData)
    {
        string result = responseTextData.Trim();

        Debug.Log("resopnse data : " + result);

        // �ݹ����� �Ѱܹ��� JSON �����͸� ������ �����迭 ��ü�� ������
        JsonData data = JsonMapper.ToObject(result);

        // �޽��� ������ ����
        string message = (string)data["message"];

        // �α��� �Ϸ� �޽��� ���
        logMessageText.text = message;

        // ��� ���� ���� ����
        int status = (int)data["status"];

        // �α��ο� �����ߴٸ�
        if (status == 2000)
        {
            // ���� ���̵�� �г��� �����͸� ������
            string userId = (string)data["data"]["user_data"]["id"];
            string userNick = (string)data["data"]["user_data"]["nick"];

            logMessageText.text = $"{userId} ���̵� ������ �α����� �Ϸ��߽��ϴ�.";

            // ���� ���̵�� �г����� PlayerPrefs�� ������
            PlayerPrefs.SetString("USER_ID", userId);
            PlayerPrefs.SetString("USER_NICK", userNick);
            PlayerPrefs.Save();

            // ���� ���þ����� �̵�
            SceneManager.LoadScene("SelectLevel");
        }
        else
        {
            // ȸ�� �α��� ���� �޽��� ���
            logMessageText.text = (string)data["message"];
        }
    }

    public void JoinButtonClick()
    {
        SceneManager.LoadScene("JoinScene");
    }
    

}
