using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twr_TestTower : Twr_0Base
{
    
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.Shooter;
        towerType = TowerType.Mage;

        towerName = "폭발술사";
        towerPrice = 100;
        towerAttackDamage = 200f;
        towerAttackSpeed = 1f;
        towerAttackRange = 10f;

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerMaxTarget = 1;

        //업그래이드 단계에 따른 공격력 계산식-----------------
        damage = towerAttackDamage + towerAttackDamage * UpGrade_Manager.Instance.upgradePercent * 0.01f;
        //-----------------------------------------------

    }
}
