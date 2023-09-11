using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twr_TestTower : Twr_0Base
{
    
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.Shooter;
        towerType = TowerType.Mage;

        towerName = "폭발술사";
        towerPrice = 100;
        float[] towerAttackDamage = { 30f, 40f, 80f, 100f, 150f, 200f };
        float[] towerAttackSpeed = { 1f, 1f, 1f, 1f, 1f, 1f };
        float[] towerAttackRange = { 10f, 10f, 10f, 10f, 10f, 25f };

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        float[] towerMaxTarget = { 1, 2, 3, 4, 5, 8 };

        //업그래이드 단계에 따른 공격력 계산식-----------------
        damage = towerAttackDamage + towerAttackDamage * UpGrade_Manager.Instance.upgradePercent * 0.01f;
        //-----------------------------------------------

    }
}
