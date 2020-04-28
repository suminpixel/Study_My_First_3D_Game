﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Tank
{
    public float speed = 10.0f;
    Player player; //포함 관계 : Nested Prefeb(중첩된 프리펫)
}
class Player {

}
public class PlayerController : MonoBehaviour
{   
    [SerializeField]
    float _speed = 10.0f;
    void Start()
    {
        //Input Manager 구독
        Managers.Input.KeyAction -= OnKeyboard; //다른 곳에서 구독하고 있는 경우를 방지하기 위해 우선 -+
        Managers.Input.KeyAction += OnKeyboard;

        Tank tank1 = new Tank();
    }

    float _yAngle = 0.0f;
    
    void Update(){
        //업데이트 문에서 직접 키입력을 받는경우 어디서 키입력이 왔는지 모를 수 있다. 따라서 InputManager 로 기능을 별도 분리하였다.
    }
    void OnKeyboard()
    {
        _yAngle += Time.deltaTime * _speed;
      
        //Rotate 방법
            //1. 절대 회전 값으로 회전
            //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

            //2. 특정 축 기준으로 회전 +- delta 
            //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

            //3. 쿼터니언 사용 : x, y, z, w
            // 쿼터니언이란 ? 3차원 그래픽에서 회전을 표현할 때 행렬대신 사용하는 수학적 개념. 4차원 복소수 공간의 벡터로서 q = <w, z, y, z> 로 나타낼 수 있다. 행렬연산에 비해 속도가 빠르고 차지하는 메모리의양이 적도 오류 확률이 낮다.
            // transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));
        
        if(Input.GetKey(KeyCode.W)){
            //transform.rotation = Quaternion.LookRotation(Vector3.forward);

            //Slerp(from, to , 중간값 단위 포인트 0~1.0f 사이)
            transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if(Input.GetKey(KeyCode.S)){
            transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if(Input.GetKey(KeyCode.A)){
            transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if(Input.GetKey(KeyCode.D)){
            transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }
}
