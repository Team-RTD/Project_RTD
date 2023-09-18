using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpGrade_Manager : MonoBehaviour
{
    public static UpGrade_Manager instance;

    public float upgradePercent = 0.0f;
    public int warrior_upgrade_rank = 0;
    public int mage_upgrade_rank = 0;
    public int archer_upgrade_rank = 0;

    public int warrior_upgrade_price = 20;
    public int mage_upgrade_price = 20;
    public int archer_upgrade_price = 20;
    //타워 종족별로 구분해서 리스트 저장.
    //타워의 종족은 추후 작성하는 것으로 한다.
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
    

    public void WarriorUpgrade()
    {
        if(Data_Manager.instance.money2 <= warrior_upgrade_price)
        {
            int rand = Random.Range(12, 16);
            Sound_Manager.instance.NarPlay(rand);
            Ui_Manager.instance.state.text = "재화 부족!";

            return;
        }
        else
        {
            int rand = Random.Range(7, 9);
            Sound_Manager.instance.NarPlay(rand);
            Data_Manager.instance.money2 -= warrior_upgrade_price;
            Debug.Log("Warrior Upgrade!");
            warrior_upgrade_rank++;
            warriorUpgrade += +12.5f;
            warrior_upgrade_price += 4;

        }


        Ui_Manager.instance.UiRefresh();
    }

    public void MageUpgrade()
    {
        if (Data_Manager.instance.money2 <= mage_upgrade_price)
        {
            int rand = Random.Range(12, 16);
            Sound_Manager.instance.NarPlay(rand);
            Ui_Manager.instance.state.text = "재화 부족!";

            return;
        }
        else
        {
            int rand = Random.Range(7, 9);
            Sound_Manager.instance.NarPlay(rand);
            Data_Manager.instance.money2 -= mage_upgrade_price;
            Debug.Log("Mage Upgrade!");
            mage_upgrade_rank++;
            mageUpgrade += +12.5f;
            mage_upgrade_price += 4;
            Ui_Manager.instance.UiRefresh();
        }
    }

    public void ArcherUpgrade()
    {
            if (Data_Manager.instance.money2 <= archer_upgrade_price)
        {
            int rand = Random.Range(12, 16);
            Sound_Manager.instance.NarPlay(rand);
            Ui_Manager.instance.state.text = "재화 부족!";

                return;
            }
            else
            {
            int rand = Random.Range(7, 9);
            Sound_Manager.instance.NarPlay(rand);
            Data_Manager.instance.money2 -= archer_upgrade_price;
            Debug.Log("archer Upgrade!");
                archer_upgrade_rank++;
                archerUpgrade += +12.5f;
                archer_upgrade_price += 4;
                Ui_Manager.instance.UiRefresh();
            }
    }
}
