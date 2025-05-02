using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    [SerializeField]    //객체가 private지만 유니티에서 설정 건드릴 수 있음
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f; //미사일 쏘는 시간 간격 조절용
    private float lastShotTime = 0f;

    void Update()
    {
        //키보드로 이동(수직가능)
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f);
        // transform.position += moveTo * moveSpeed * Time.deltaTime;

        //키보드로 이동(좌우만 가능)
        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        // if (Input.GetKey(KeyCode.LeftArrow)){
        //     transform.position -= moveTo;
        // } else if (Input.GetKey(KeyCode.RightArrow)){
        //     transform.position += moveTo;
        // }

        //마우스로 캐릭터 이동
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //카메라 기준 좌표값 뜨도록 함
        //Debug.Log(mousePos); //좌표값 확인
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f); //마우스 좌표 최대 최소값
        transform.position = new Vector3(toX, transform.position.y, transform.position.z); //플레이어의 y z좌표는 그대로 씀

        if(GameManager.instance.isGameOver == false){
        Shoot();}
    }

    void Shoot(){ //미사일 무한 발사
        //'현재시간 - 마지막으로 미사일 쏜 시간'이 '0.05초'보다 커졌을 때 미사일 다시 발사
        if(Time.time - lastShotTime > shootInterval) {
        Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
        lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) { //충돌 처리
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss"){ //적에 닿으면 사망
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        } else if (other.gameObject.tag == "Coin"){ //코인에 닿으면 코인+1 함수 호출, 코인 사라짐
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade() {
        weaponIndex += 1;
        if (weaponIndex >= weapons.Length){
            weaponIndex = weapons.Length - 1;
        }
    }
}
