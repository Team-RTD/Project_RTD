using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Twr_0Base;

public class BowTower : Twr_0Base
{
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.InstanceThorwer;
        towerType = TowerType.Archer;

        towerName = "BowTower";
        towerPrice = 100;
        towerAttackDamage = new float[6] { 50f, 100f, 200f, 400f, 800f, 800f };
        towerAttackSpeed = new float[6] { 2f, 2f, 1.7f, 1.3f, 1f, 0.5f };
        towerAttackRange = new float[6] { 10f, 10f, 10f, 10f, 10f, 10f };

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerMaxTarget = new int[6] { 1, 1, 1, 1, 1, 2 };

        throwObjSpeed = 25f;

        towerRank = 0;
    }
}
