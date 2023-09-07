using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twr_TestTower : Twr_0Base
{
    
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.Shooter;

        towerName = "Test_A";
        towerPrice = 100;
        towerAttackDamage = 30f;
        towerAttackSpeed = 1f;
        towerAttackRange = 10f;

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerMaxTarget = 1;

    }
}
