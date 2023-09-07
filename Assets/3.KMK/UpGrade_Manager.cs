using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGrade_Manager : MonoBehaviour
{
    public static UpGrade_Manager instance;

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
