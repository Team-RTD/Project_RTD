using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

//목표1: monster만났을 때 해당 몬스터 파괴하고 체력1 감소하고 몬스터 수 감소
//필요속성1: 플레이어 체력,


//목표2: 파괴 이펙트 생성
//필요속성2: 파괴 이펙트

public class DestroyZone : MonoBehaviour
{
    //필요속성2: 파괴 이펙트
    ParticleSystem particleSystem0;
    public GameObject destroyEffect0;


    // Start is called before the first frame update
    void Start()
    {
        //destroyEffect0.transform.position = transform.position;
        //particleSystem0 = destroyEffect0.GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider others)
    {
        
        if (others.gameObject.tag == "Monster")
        {
            //목표2: 파괴 이펙트 생성
            GameObject deadeffect = Instantiate(destroyEffect0, others.transform.position, Quaternion.identity);   
            Destroy(deadeffect, 2f);
            //목표1: monster만났을 때 해당 몬스터 파괴하고 체력1 감소하고 몬스터 수 감소
            StageManager.instance.monsterCount--;
            Destroy(others.gameObject);
            if (others.gameObject.name == "Monster9(Clone)" || others.gameObject.name == "Monster19(Clone)" || others.gameObject.name == "Monster29(Clone)" || others.gameObject.name == "Monster39(Clone)" || others.gameObject.name == "Monster49(Clone)")
            {
                Data_Manager.instance.curHp-=25;
            }
            else
            {
                Data_Manager.instance.curHp--;
            }
            Ui_Manager.instance.UiRefresh();
        }
    }
}
