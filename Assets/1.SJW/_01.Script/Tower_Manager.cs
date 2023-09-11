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


    //타워 생성 스크립트 towerZone = 생성될 마법진, towerTier = 생성될 타워 등급
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
            tower1.transform.tag = "Tower"; // 타워 태그 변경
            t_zone.Tower = tower1; //타워존에 타워 오브젝트 할당
            tower1.GetComponent<Twr_0Base>().TowerZone = t_zone.gameObject;
            tower1.GetComponent<Twr_0Base>().towerRank = towerTier-1;
            GameObject summoneffect = Instantiate(SummonEffect, towerZone.transform.position, Quaternion.Euler(Vector3.zero));
            Destroy(summoneffect, 3); // 이펙트는 3초뒤 삭제
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
            //업그래이드 매니저 호출-------------------
            UpGrade_Manager.Instance.AddTowerToList(tower1);
            //-------------------


            Outline charliner = tower1.AddComponent<Outline>(); //만든 타워에 외각선 추가
            charliner.OutlineColor = Color.red;
            charliner.OutlineWidth = 2;
            charliner.enabled = false;

            towerZone.SetActive(false);
        }
    }

    //타워 생성 스크립트와는 달리 towerZone이 아니라 판매될 타워 자체가 들어감
    //불값에선 판매면 true 조합이면 false
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

        if(issell) //판매면 골드 +
        {
            Data_Manager.instance.money1 += (towerinfo.towerRank+1)*75;
        }

        Ui_Manager.instance.UiRefresh();

    }

}
