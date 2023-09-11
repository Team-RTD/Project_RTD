using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGrade_Manager : MonoBehaviour
{
    public static UpGrade_Manager instance;

    public float upgradePercent = 0.0f;
    public int upgrade_rank = 0;
    //타워 종족별로 구분해서 리스트 저장.
    //타워의 종족은 추후 작성하는 것으로 한다.
    //public List<GameObject> TowerType1 = new List<GameObject>();


    public static UpGrade_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public void Awake()
    {
        if (instance == null)
        {
            Debug.Log("ManagerInstance");
            instance = this;
        }
    }
    public float GetUpgradePercentage()
    {
        return upgradePercent;
    }
    public void Upgrade()
    {
        Debug.Log("Upgrade!");
        upgradePercent += 10.0f;
        upgrade_rank++;
        foreach (GameObject obj in TowerType1)
        {
            obj.GetComponent<Twr_0Base>().TowerInfo();
        }

        Ui_Manager.instance.UiRefresh();
    }

    public void AddTowerToList(GameObject tower)
    {
        TowerType1.Add(tower);
    }
}
