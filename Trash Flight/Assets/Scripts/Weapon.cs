using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    public float damage = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f); //weapon = gameObject. 1초 뒤에 사라짐
        //화면 밖으로 나갔는데도 계속 미사일 나가는거 방지용
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
