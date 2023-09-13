using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

//��ǥ1: monster������ �� �ش� ���� �ı��ϰ� ü��1 �����ϰ� ���� �� ����
//�ʿ�Ӽ�1: �÷��̾� ü��,


//��ǥ2: �ı� ����Ʈ ����
//�ʿ�Ӽ�2: �ı� ����Ʈ

public class DestroyZone : MonoBehaviour
{
    //�ʿ�Ӽ�2: �ı� ����Ʈ
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
            //��ǥ2: �ı� ����Ʈ ����
            GameObject deadeffect = Instantiate(destroyEffect0, others.transform.position, Quaternion.identity);   
            Destroy(deadeffect, 2f);
            //��ǥ1: monster������ �� �ش� ���� �ı��ϰ� ü��1 �����ϰ� ���� �� ����
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
