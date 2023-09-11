using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twr_TestTower : Twr_0Base
{
    
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.Shooter;
        towerType = TowerType.Mage;

        towerName = "Æø¹ß¼ú»ç";
        towerPrice = 100;
        towerAttackDamage = new float[6] { 30f, 40f, 80f, 100f, 150f, 200f };
        towerAttackSpeed = new float[6] { 1f, 1f, 1f, 1f, 1f, 1f };
        towerAttackRange = new float[6] { 10f, 10f, 10f, 10f, 10f, 25f };

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerRank = 0;
        towerMaxTarget = new int[6] { 1, 2, 3, 4, 5, 8 };

    }
}
