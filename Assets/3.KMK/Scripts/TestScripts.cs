using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //attackRange�� ����
    public float TowerAttackRange = 5f; // Ÿ�� ���� �ݰ�
    //attackSpeed�� ����
    public float TowerAttackSpeed = 2f; // ���� ����

    private float lastAttackTime;

    private void Start()
    {
        lastAttackTime = Time.time; // �ʱ�ȭ
    }

    private void Update()
    {
        // ���� �������� ���� ����
        if (Time.time - lastAttackTime >= TowerAttackSpeed)
        {
            AttackArea();
            lastAttackTime = Time.time; // ������ ���� �ð� ������Ʈ
        }
    }

    private void AttackArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, TowerAttackRange);

        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                // ���̶�� DamagedAction ����
                enemy.DamagedAction(10); // ������ ���� �ʿ信 ���� ����
            }
        }
    }
}
