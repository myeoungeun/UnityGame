using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float minY = -7f;

    void Start()
    {
        Jump();
    }

    void Jump(){ //코인이 점프했다가 떨어짐(좌우상 랜덤값만큼 움직임)
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

        float randomJumpForce = Random.Range(4f, 8f);
        Vector2 jumpVelocity = Vector2.up * randomJumpForce;
        jumpVelocity.x = Random.Range(-2f, 2f);
        rigidBody.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    void Update()
    {
        if (transform.position.y < minY){ //화면 밖으로 나가면 삭제
            Destroy(gameObject);
        }
    }
}