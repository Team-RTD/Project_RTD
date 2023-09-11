using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRank2Tower : Twr_0Base
{
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.Area;
        towerType = TowerType.Warrior;

        towerName = "테스트용";
        towerPrice = 100;
        towerAttackDamage = 80f;
        towerAttackSpeed = 3f;
        towerAttackRange = 3f;

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerMaxTarget = 2;

        areaDuration = 0.6f;
        areaAttDelay = 1f;
        areaToTarget = false;

        damage = towerAttackDamage + towerAttackDamage * UpGrade_Manager.Instance.upgradePercent * 0.01f; ;

    }
}
