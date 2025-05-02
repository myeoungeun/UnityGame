using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;

    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f}; //화면 기준 적 5개 위치

    [SerializeField]
    private float spawnInterval = 1.5f;

    void Start()
    {
       StartEnemyRoutine();
    }

    void StartEnemyRoutine(){
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine() { //적 생성 중단
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine(){
        yield return new WaitForSeconds(3f);

        float moveSpeed = 5f;
        int spawnCount = 0;
        int enemyIndex = 0;

        while(true) { //적 무한 생성
            foreach(float posX in arrPosX){ //배열 값을 하나씩 꺼내서 posX에 넣음
                SpawnEnemy(posX, enemyIndex, moveSpeed);
            }

            spawnCount++;
            if(spawnCount % 10 == 0) {  //몹 생성이 10번째, 20, 30.. 이 되면
                enemyIndex += 1; //enemyIndex가 커지면서 더 강한 몹이 등장
                moveSpeed += 2; //적 내려오는 속도 빨라짐
            }

            if (enemyIndex >= enemies.Length){ //적 최대등급 등장하는것보다 값이 커지면 보스 등장
                SpawnBoss();
                enemyIndex = 0;
                moveSpeed = 5f;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed){
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        if (Random.Range(0,5) == 0){ //0, 1, 2, 3, 4, -> 각 20%. 20% 확률로 한 단계 높은 적이 나옴
            index += 1;
        }

        if (index >= enemies.Length){ //index가 아무리 커져도 enemies 길이를 넘어가지 않음. 에러 방지용
            index = enemies.Length - 1;
        }

        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }

    void SpawnBoss(){
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}
