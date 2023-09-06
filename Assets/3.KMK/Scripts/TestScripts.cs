using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //attackRange로 변경
    public float TowerAttackRange = 5f; // 타워 감지 반경
    //attackSpeed로 변경
    public float TowerAttackSpeed = 2f; // 공격 간격

    private float lastAttackTime;

    private void Start()
    {
        lastAttackTime = Time.time; // 초기화
    }

    private void Update()
    {
        // 일정 간격으로 공격 실행
        if (Time.time - lastAttackTime >= TowerAttackSpeed)
        {
            AttackArea();
            lastAttackTime = Time.time; // 마지막 공격 시간 업데이트
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
                // 적이라면 DamagedAction 실행
                enemy.DamagedAction(10); // 데미지 량은 필요에 따라 설정
            }
        }
    }
}
