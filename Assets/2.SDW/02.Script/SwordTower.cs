using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTower : Twr_0Base
{
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.Shooter;
        towerType = TowerType.Warrior;

        towerName = "±â»ç";
        towerPrice = 100;
        towerAttackDamage = new float[6] { 70f, 150f, 350f, 800f, 1700f, 3500f };
        towerAttackSpeed = new float[6] { 2f, 2f, 1.5f, 1.5f, 1f, 0.7f };
        towerAttackRange = new float[6] { 3f, 3f, 3f, 3f, 4f, 5f };

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerMaxTarget = new int[6] { 1, 1, 1, 1, 1, 1 };

        towerRank = 0;
    }
}
