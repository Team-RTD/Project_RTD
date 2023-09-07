using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGrade_Manager : MonoBehaviour
{
    public static UpGrade_Manager instance;

    public float milleUG = 1.0f;
    public float archerUG = 1.0f;
    public float mageUG = 1.0f;

    public float upgradePercent = 0.0f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public void UpGradeTower(Twr_TestTower towerAttackDamage)
    {
        towerAttackDamage.UpGrade();
    }
}
