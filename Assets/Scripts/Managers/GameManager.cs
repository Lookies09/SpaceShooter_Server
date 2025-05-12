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
        // 이벤트 연결
        responseScoreUpdateDataEvent.AddListener(OnUserScoreCompleteData);
    }

    private void OnDisable()
    {
        // 이벤트 연결 해제
        responseScoreUpdateDataEvent.RemoveListener(OnUserScoreCompleteData);
    }

    private void Awake()
    {        
        point = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        string playerName = PlayerPrefs.GetString("USER_NICK", "플레이어");
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

            // 서버에 유저 점수를 업데이트 요청함
            NetAPIManager.instance.UserScoreUpdate(id, score.ToString(), null, responseScoreUpdateDataEvent);

            // 엔드씬으로 이동
            SceneManager.LoadScene("EndScene");
        }

        if (isDead && num == 1)
        {
            string id = PlayerPrefs.GetString("USER_ID");
            int score = point;

            // 서버에 유저 점수를 업데이트 요청함
            NetAPIManager.instance.UserScoreUpdate(id, score.ToString(), null, responseScoreUpdateDataEvent);

            // 엔드씬으로 이동
            SceneManager.LoadScene("ClearScene");
        }

    }

    public void OnUserScoreCompleteData(string responseTextData)
    {
        string result = responseTextData.Trim();

        Debug.Log("resopnse data : " + result);

        // 콜백으로 넘겨받은 JSON 데이터를 가지고 연관배열 객체를 생성함
        JsonData data = JsonMapper.ToObject(result);

        // 메시지 데이터 추출
        string message = (string)data["message"];

        // 결과 상태 정보 추출
        int status = (int)data["status"];

        // 로그인에 성공했다면
        if (status == 3000)
        {
            // 유저 아이디와 닉네임 데이터를 추출함
            string userId = (string)data["data"]["id"];

            Debug.Log("유저 점수 업데이트 : " + userId);

            // 게임씬으로 이동
            SceneManager.LoadScene("EndScene");
        }
        else
        {
            Debug.Log("서버 통신 오류 발생 : " + message);
        }
    }


}
