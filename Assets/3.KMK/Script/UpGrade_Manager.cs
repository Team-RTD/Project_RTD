using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpGrade_Manager : MonoBehaviour
{
    public static UpGrade_Manager instance;

    public float upgradePercent = 0.0f;
    public int upgrade_rank = 0;
    //Ÿ�� �������� �����ؼ� ����Ʈ ����.
    //Ÿ���� ������ ���� �ۼ��ϴ� ������ �Ѵ�.
    public float warriorUpgrade = 0;
    public float mageUpgrade = 0;
    public float archerUpgrade = 0;

    public  enum UpgradeType
    {        
        W,
        M,
        A
    }

    public UpgradeType upgradeType = UpgradeType.W;


    public void Awake()
    {
        if (instance == null)
        {
            Debug.Log("ManagerInstance");
            instance = this;
        }
    }
    

    public void Upgrade()
    {
        Debug.Log("Upgrade!");        
        upgrade_rank++;

 

                switch (upgradeType)
                {
                    case UpgradeType.W:
                        warriorUpgrade += +10.0f;

                        break;

                    case UpgradeType.M:
                        mageUpgrade += +10.0f;

                        break;

                    case UpgradeType.A:
                        archerUpgrade += +10.0f;

                        break;
                }



        Ui_Manager.instance.UiRefresh();
    }    
}
