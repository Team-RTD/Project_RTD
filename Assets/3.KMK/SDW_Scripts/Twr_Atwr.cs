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
<<<<<<<< HEAD:Assets/3.KMK/SDW_Scripts/Twr_Atwr.cs
        towerAttackRange = 3;
        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;
        towerMaxTarget = 1;
        towerBounceCount = 1;
        towerAttackType = TowerAttackType.Area;
========
        towerAttackRange = 20;

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerAttackType = TowerAttackType.Area;
        towerMaxTarget = 2;
        
        throwObjSpeed = 1f;

        areaDuration = 2.1f;
        areaAttDelay = 0.6f;
>>>>>>>> main:Assets/3.KMK/SDW_Scripts/Twr_TestTower.cs
    }
}
