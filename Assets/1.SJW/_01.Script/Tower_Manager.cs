using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Manager : MonoBehaviour
{
    public static Tower_Manager instance { get; private set; }

    public GameObject SummonEffect;
    public AudioClip SummonSound;

    public List<GameObject> Tower1;
    public List<GameObject> Tower2;
    public List<GameObject> Tower3;
    public List<GameObject> Tower4;
    public List<GameObject> Tower5;
    public List<GameObject> Tower6;

    public GameObject[] Towers;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Ÿ�� ���� ��ũ��Ʈ towerZone = ������ ������, towerTier = ������ Ÿ�� ���
    public void TowerInstance(GameObject towerZone,int towerTier)
    {
        TowerZone t_zone = towerZone.GetComponent<TowerZone>();
        if (!t_zone.towerOn)
        {
            Material mat = towerZone.GetComponent<Renderer>().material;
            t_zone.towerOn = true;
            mat.SetColor("_EmisColor", t_zone.summonZoneColor[towerTier]);

            int random = Random.Range(0,Towers.Length);

            // GameObject tower1 = Instantiate(test, hitObject.transform.position, Quaternion.Euler(Vector3.zero));
            GameObject tower1 = Instantiate(Towers[random], towerZone.transform.position, Quaternion.Euler(Vector3.zero));
            tower1.transform.tag = "Tower"; // Ÿ�� �±� ����
            t_zone.Tower = tower1; //Ÿ������ Ÿ�� ������Ʈ �Ҵ�
            tower1.GetComponent<Twr_0Base>().TowerZone = t_zone.gameObject;
            tower1.GetComponent<Twr_0Base>().towerRank = towerTier-1;
            GameObject summoneffect = Instantiate(SummonEffect, towerZone.transform.position, Quaternion.Euler(Vector3.zero));
            Destroy(summoneffect, 3); // ����Ʈ�� 3�ʵ� ����
            Sound_Manager.instance.EffectPlay(SummonSound);

            switch(tower1.GetComponent<Twr_0Base>().towerRank+1)
            {
                    case 1:
                    Tower1.Add(tower1);
                    break;
                    case 2:
                    Tower2.Add(tower1);
                    break;
                    case 3:
                    Tower3.Add(tower1);
                    break;
                    case 4:
                    Tower4.Add(tower1);
                    break;
                    case 5:
                    Tower5.Add(tower1);
                    break;
                    case 6:
                    Tower6.Add(tower1);
                    break;
            }
            //���׷��̵� �Ŵ��� ȣ��-------------------
            UpGrade_Manager.Instance.AddTowerToList(tower1);
            //-------------------


            Outline charliner = tower1.AddComponent<Outline>(); //���� Ÿ���� �ܰ��� �߰�
            charliner.OutlineColor = Color.red;
            charliner.OutlineWidth = 2;
            charliner.enabled = false;

            towerZone.SetActive(false);
        }
    }

    //Ÿ�� ���� ��ũ��Ʈ�ʹ� �޸� towerZone�� �ƴ϶� �Ǹŵ� Ÿ�� ��ü�� ��
    //�Ұ����� �ǸŸ� true �����̸� false
    public void TowerSell(GameObject tower,bool issell)
    {
        Twr_0Base towerinfo;
        TowerZone t_zone;

        towerinfo = tower.GetComponent<Twr_0Base>();
        t_zone = towerinfo.TowerZone.GetComponent<TowerZone>();
        Material mat = t_zone.gameObject.GetComponent<Renderer>().material;
        t_zone.towerOn = false;
        mat.SetColor("_EmisColor", t_zone.summonZoneColor[0]);


        tower.GetComponent<Twr_0Base>().StopAllCoroutines();

        t_zone.gameObject.SetActive(false);

        switch (tower.GetComponent<Twr_0Base>().towerRank + 1)
        {
            case 1:
                Tower1.Remove(tower);
                break;
            case 2:
                Tower2.Remove(tower);
                break;
            case 3:
                Tower3.Remove(tower);
                break;
            case 4:
                Tower4.Remove(tower);
                break;
            case 5:
                Tower5.Remove(tower);
                break;
            case 6:
                Tower6.Remove(tower);
                break;
        }
        Destroy(t_zone.Tower);

        if(issell) //�ǸŸ� ��� +
        {
            Data_Manager.instance.money1 += (towerinfo.towerRank+1)*75;
        }

        Ui_Manager.instance.UiRefresh();

    }

}
