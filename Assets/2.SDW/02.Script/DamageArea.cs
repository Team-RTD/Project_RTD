using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : Twr_Atwr
{
    public GameObject pTower;
    public void OnTriggerEnter(Collider other)
    {
        // �ݶ��̴��� ���� ������Ʈ�� Enemy ������Ʈ�� ã���ϴ�.
        MonsterMove enemy = other.GetComponent<MonsterMove>();

        if (enemy != null)
        {
            // Enemy ������Ʈ�� DamagedAction �Լ��� ȣ���մϴ�.
            enemy.DamagedAction(towerAttackDamage);
            transform.position = pTower.transform.position;
            print("�����ϴ�.");
            gameObject.SetActive(false);
        }
    }
}
