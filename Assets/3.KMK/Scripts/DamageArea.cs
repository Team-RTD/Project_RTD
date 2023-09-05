using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : Twr_Atwr
{
    public GameObject pTower;
    public void OnTriggerEnter(Collider others)
    {
        // 콜라이더에 들어온 오브젝트의 Enemy 컴포넌트를 찾습니다.
        Enemy enemy = others.GetComponent<Enemy>();
        print("test");
        if (enemy != null)
        {
            // Enemy 컴포넌트의 DamagedAction 함수를 호출합니다.
            enemy.DamagedAction(towerAttackDamage);
            transform.position = pTower.transform.position;
            print("꺼집니다");
            //gameObject.SetActive(false);
        }
    }
}
