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
        towerAttackDamage += ((towerAttackDamage / 100) * upgradePercent);
        towerAttackSpeed = 1;
        towerAttackRange = 20;

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerAttackType = TowerAttackType.Shooter;
        towerMaxTarget = 2;
        
        throwObjSpeed = 1f;

        areaDuration = 2.1f;
        areaAttDelay = 0.6f;

        upgradePercent = 0;        
        towerRank= 1;


    }
    public void UpGrade()
    {
        upgradePercent = upgradePercent + 1f;        
    }



}
