using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject gameClearPanel;

    private int coin = 0;

    [HideInInspector]
    public bool isGameOver = false;

    [HideInInspector]
    public bool isGameClear = false;

    void Awake(){
        if (instance == null){ //싱글톤
            instance = this;
        }
    }

    public void IncreaseCoin() { //코인값 증가
        coin += 1;
        text.SetText(coin.ToString());

        if (coin % 20 == 0){ //코인 20, 40, 60개 먹으면
            Player player = FindObjectOfType<Player>(); //플레이어 클래스 호출하고
            if (player != null){} //0이 아니면 무기 업그레이드
            player.Upgrade();
        }
    }

    public void SetGameOver(){
        if (isGameOver || isGameClear) return; //중복 호출 방지
        isGameOver = true;

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if(enemySpawner != null){
            enemySpawner.StopEnemyRoutine();
        }

        Invoke("ShowGameOverPanel", 1f);
    }

    void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
    }


    public void SetGameClear(){
        if (isGameOver || isGameClear) return;
        isGameClear = true;

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if(enemySpawner != null){
            enemySpawner.StopEnemyRoutine();
        }

        Invoke("ShowGameClearPanel", 1f);
    }

    void ShowGameClearPanel(){
        gameClearPanel.SetActive(true);
    }

    public void PlayAgain(){
        SceneManager.LoadScene("SampleScene");
    }
}