using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twr_Atwr : Twr_0Base
{
    
    public override void TowerInfo()
    {
        towerNumber = "A_Tower";
        towerPrice = 100;
        towerAttackDamage = 10;
        towerAttackSpeed = 2;
        towerAttackRange = 3;
        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;
        towerMaxTarget = 1;
        towerBounceCount = 1;
        towerAttackType = TowerAttackType.Area;
    }
}
