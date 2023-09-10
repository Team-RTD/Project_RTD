using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twr_TestTower : Twr_0Base
{
    
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.Shooter;
        towerType = TowerType.Mage;

        towerName = "���߼���";
        towerPrice = 100;
        towerAttackDamage = 200f;
        towerAttackSpeed = 1f;
        towerAttackRange = 10f;

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerMaxTarget = 1;

        //���׷��̵� �ܰ迡 ���� ���ݷ� ����-----------------
        damage = towerAttackDamage + towerAttackDamage * UpGrade_Manager.Instance.upgradePercent * 0.01f;
        //-----------------------------------------------

    }
}
