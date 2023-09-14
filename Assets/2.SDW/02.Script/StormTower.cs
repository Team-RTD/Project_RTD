using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTower : Twr_0Base

{
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.Area;
        towerType = TowerType.Mage;

        towerName = "번개술사";
        towerPrice = 100;
        towerAttackDamage = new float[6] { 25f, 60f, 140f, 320f, 800f, 1000f };
        towerAttackSpeed = new float[6] { 5f, 5f, 5f, 5f, 5f, 2f };
        towerAttackRange = new float[6] { 7f, 7f, 7f, 7f, 7f, 15f };

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerMaxTarget = new int[6] { 1, 1, 1, 1, 1, 3 };

        areaDuration = 3f;
        areaAttDelay = 0.251f;

        towerRank = 0;

    }
}
