using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Twr_0Base;

public class BowTower : Twr_0Base
{
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.InstanceThorwer;
        towerBrood = TowerBrood.Bowman;

        towerName = "BowTower";
        towerPrice = 100;
        towerAttackDamage = 50f;
        towerAttackSpeed = 1.5f;
        towerAttackRange = 10f;

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerMaxTarget = 1;

        throwObjSpeed = 4f;

    }
}
