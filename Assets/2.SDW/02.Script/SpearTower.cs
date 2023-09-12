using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTower : Twr_0Base
{
    public SpearThrowFalse spearObj;
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.InstanceThorwer;
        towerType = TowerType.Warrior;

        towerName = "투창 검투사";
        towerPrice = 100;
        towerAttackDamage = new float[6] { 80f, 200f, 550f, 1200f, 2600f, 6000f };
        towerAttackSpeed = new float[6] { 4f, 4f, 3f, 3f, 3f, 2f };
        towerAttackRange = new float[6] { 10f, 11f, 13f, 15f, 17f, 20f };

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerMaxTarget = new int[6] { 1, 1, 1, 1, 1, 1 };

        throwObjSpeed = 35f;

        towerRank = 0;
    }

    public override void Start()
    {
        spearObj = gameObject.GetComponentInChildren<SpearThrowFalse>();
    }
    public override void Update()
    {
        if (isCoolTime == true)
        {
            spearObj.gameObject.SetActive(false);
        }

        else if (isCoolTime == false)
        {
            spearObj.gameObject.SetActive(true);
        }

        if (isCoolTime == false)
        {
            Detecting();
        }
    }
}
