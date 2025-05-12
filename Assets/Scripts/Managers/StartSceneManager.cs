using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;

public class StartSceneManager : MonoBehaviour
{
    // 아이디 입력필드
    [SerializeField] private InputField idInputField;
    // 비번 입력 필드
    [SerializeField] private InputField pwInputField;

    // 로그 메시지 텍스트
    [SerializeField] private Text logMessageText;

    // 로그인 관련 유니티 이벤트
    NetworkLogTextEvent logTextEvent = new NetworkLogTextEvent();
    NetworkResponseDataEvent responseLoginDataEvent = new NetworkResponseDataEvent();

    private void OnEnable()
    {
        // 이벤트 연결
        logTextEvent.AddListener(OnPrintLogText);
        responseLoginDataEvent.AddListener(OnLoginCompleteData);
    }

    private void OnDisable()
    {
        // 이벤트 연결 해제
        logTextEvent.RemoveListener(OnPrintLogText);
        responseLoginDataEvent.RemoveListener(OnLoginCompleteData);
    }

    public void StartButtonClick()
    {
        string userId = idInputField.text.Trim();

        if (userId.Length < 4)
        {
            logMessageText.text = "아이디는 최소 4자 이상입니다.";
            return;
        }

        string userPw = pwInputField.text.Trim();

        if (userPw.Length <= 0)
        {
            logMessageText.text = "비밀번호를 입력해야 합니다.";
            return;
        }

        // 유저 로그인 수행
        NetAPIManager.instance.UserLogin(userId, userPw, logTextEvent, responseLoginDataEvent);
    }

    // 로그 메시지 출력
    public void OnPrintLogText(string log)
    {
        logMessageText.text = log;
    }

    public void OnLoginCompleteData(string responseTextData)
    {
        string result = responseTextData.Trim();

        Debug.Log("resopnse data : " + result);

        // 콜백으로 넘겨받은 JSON 데이터를 가지고 연관배열 객체를 생성함
        JsonData data = JsonMapper.ToObject(result);

        // 메시지 데이터 추출
        string message = (string)data["message"];

        // 로그인 완료 메시지 출력
        logMessageText.text = message;

        // 결과 상태 정보 추출
        int status = (int)data["status"];

        // 로그인에 성공했다면
        if (status == 2000)
        {
            // 유저 아이디와 닉네임 데이터를 추출함
            string userId = (string)data["data"]["user_data"]["id"];
            string userNick = (string)data["data"]["user_data"]["nick"];

            logMessageText.text = $"{userId} 아이디 유저가 로그인을 완료했습니다.";

            // 유저 아이디와 닉네임을 PlayerPrefs에 저장함
            PlayerPrefs.SetString("USER_ID", userId);
            PlayerPrefs.SetString("USER_NICK", userNick);
            PlayerPrefs.Save();

            // 레벨 선택씬으로 이동
            SceneManager.LoadScene("SelectLevel");
        }
        else
        {
            // 회원 로그인 실패 메시지 출력
            logMessageText.text = (string)data["message"];
        }
    }

    public void JoinButtonClick()
    {
        SceneManager.LoadScene("JoinScene");
    }
    

}
