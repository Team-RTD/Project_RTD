using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : Twr_Atwr
{
    public GameObject pTower;
    public void OnTriggerEnter(Collider others)
    {
        // �ݶ��̴��� ���� ������Ʈ�� Enemy ������Ʈ�� ã���ϴ�.
        Enemy enemy = others.GetComponent<Enemy>();
        print("test");
        if (enemy != null)
        {
            // Enemy ������Ʈ�� DamagedAction �Լ��� ȣ���մϴ�.
            enemy.DamagedAction(towerAttackDamage);
            transform.position = pTower.transform.position;
            print("�����ϴ�");
            //gameObject.SetActive(false);
        }
    }
}
