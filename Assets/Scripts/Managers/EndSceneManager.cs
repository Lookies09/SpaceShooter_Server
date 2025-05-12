using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    // 로그 메시지 텍스트
    [SerializeField] private Text logMessageText;

    [SerializeField] private Transform rankScrollView;
    [SerializeField] private GameObject rankCellPrefab;

    // 랭킹 통신 응답 관련 유니티 이벤트
    NetworkLogTextEvent logTextEvent = new NetworkLogTextEvent();
    NetworkResponseDataEvent responseRankDataEvent = new NetworkResponseDataEvent();

    private void OnEnable()
    {
        // 이벤트 연결
        logTextEvent.AddListener(OnPrintLogText);
        responseRankDataEvent.AddListener(OnRankCompleteData);
    }

    private void OnDisable()
    {
        // 이벤트 연결 해제
        logTextEvent.RemoveListener(OnPrintLogText);
        responseRankDataEvent.RemoveListener(OnRankCompleteData);
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score : " + GameManager.point.ToString();

        // 유저 랭킹을 조회함
        NetAPIManager.instance.UserRankList(logTextEvent, responseRankDataEvent);
    }

    // 로그 메시지 출력
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

    // 랭크 리스트 정보를 출력함
    public void OnRankCompleteData(string responseTextData)
    {
        string result = responseTextData.Trim();

        Debug.Log("resopnse data : " + result);

        // 콜백으로 넘겨받은 JSON 데이터를 가지고 연관배열 객체를 생성함
        JsonData data = JsonMapper.ToObject(result);

        // 메시지 데이터 추출
        string message = (string)data["message"];

        logMessageText.text = message;

        // 결과 상태 정보 추출
        int status = (int)data["status"];

        // 랭크 조회 성공 처리
        if (status == 4000)
        {
            Debug.Log("랭크 리스트 로드");

            // 랭크 배열 정보에서 유저 정보를 추출하여 셀을 생성하고 출력함
            for (int i = 0; i < data["data"].Count; i++)
            {
                int rank = (int)data["data"][i]["rank"];
                string nick = (string)data["data"][i]["nick"];
                string bestScore = (string)data["data"][i]["bestscore"];

                Debug.Log($"{rank}, {nick}, {bestScore}");

                // 랭크 정보 표시 셀 생성
                UserRankCell userRankCell = Instantiate(rankCellPrefab, rankScrollView).GetComponent<UserRankCell>();
                userRankCell.Init(rank.ToString(), nick, bestScore);
            }
        }
        else
        {
            // 랭크 조회 실패 메시지 출력
            logMessageText.text = (string)data["message"];
        }
    }
}
