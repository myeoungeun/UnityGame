using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private float moveSpeed = 10f;
    private float minY = -7f;

    [SerializeField]
    private float hp = 1f;

    public void SetMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed;
    }

    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < minY){ //화면 밖으로 나가면 삭제
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) { //미사일에 맞으면 hp 깎임
     if(other.gameObject.tag == "Weapon"){
        Weapon weapon = other.gameObject.GetComponent<Weapon>();
        hp -= weapon.damage;
        if (hp <= 0){
            if(gameObject.tag == "Boss"){
                GameManager.instance.SetGameClear();
            }
            Destroy(gameObject);
            Instantiate(coin, transform.position, Quaternion.identity); //적 삭제되면서 코인 드랍
        }
        Destroy(other.gameObject);
     }
    }
}