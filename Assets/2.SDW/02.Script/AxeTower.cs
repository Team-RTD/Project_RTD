using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTower : Twr_0Base
{
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.Area;

        towerName = "AxeTower";
        towerPrice = 100;
        towerAttackDamage = 80f;
        towerAttackSpeed = 3f;
        towerAttackRange = 3f;

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerMaxTarget = 1;

        areaDuration = 0.6f;
        areaAttDelay = 1f;
        areaToTarget = false;

    }
}
