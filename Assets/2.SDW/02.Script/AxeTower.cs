using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTower : Twr_0Base
{
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.Area;
        towerType = TowerType.Warrior;

        towerName = "±¤Àü»ç";
        towerPrice = 100;
        float[] towerAttackDamage = { 100f, 200f, 400f, 600f, 800f, 1600f };
        float[] towerAttackSpeed = { 4f,4f,4f,4f,4f,1.5f};
        float[] towerAttackRange = { 3f, 3f, 3f, 3f, 3f, 3f };

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        float[] towerMaxTarget = { 1, 1, 1, 2, 3, 3 };

        areaDuration = 0.6f;
        areaAttDelay = 1f;
        areaToTarget = false;

    }
}
