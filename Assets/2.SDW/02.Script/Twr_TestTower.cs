using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twr_TestTower : Twr_0Base
{
    
    public override void TowerInfo()
    {
        towerName = "A_Tower";
        towerPrice = 100;
        towerAttackDamage = 10;
        towerAttackSpeed = 2;
        towerAttackRange = 20;

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerAttackType = TowerAttackType.Area;
        towerMaxTarget = 2;
        
        throwObjSpeed = 1f;

        areaDuration = 2.1f;
        areaAttDelay = 0.6f;
    }
}
