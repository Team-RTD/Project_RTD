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
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            // Enemy ������Ʈ�� DamagedAction �Լ��� ȣ���մϴ�.
            enemy.DamagedAction(towerAttackDamage);
            transform.position = pTower.transform.position;
            gameObject.SetActive(false);
        }
    }
}
