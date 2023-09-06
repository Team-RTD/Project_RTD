using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    ParticleSystem particleSystem;
    public GameObject destroyEffect;
    public List<GameObject> destroyList= new List<GameObject>();
    public int destroyNum;

    // Start is called before the first frame update
    void Start()
    {
        destroyEffect.transform.position = transform.position;
        particleSystem = destroyEffect.GetComponent<ParticleSystem>();
        destroyNum = 0;
        destroyEffect.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider others)
    {
        if (others.gameObject.tag == "Monster")
        {
<<<<<<< HEAD
            Destroy(others.gameObject);
            StartCoroutine(DestroyEffect());
            Data_Manager.instance.curHp--;
            Ui_Manager.instance.UiRefresh();

=======
            //��ǥ2: �ı� ����Ʈ ����
            GameObject deadeffect = Instantiate(destroyEffect0, others.transform.position, Quaternion.identity);   
            Destroy(deadeffect, 2f);
            //��ǥ1: monster������ �� �ش� ���� �ı��ϰ� ü��1 �����ϰ� ���� �� ����
            StageManager.instance.monsterCount--;
            Destroy(others.gameObject);
            if (others.gameObject.name == "Monster9(Clone)" || others.gameObject.name == "Monster19(Clone)")
            {
                Data_Manager.instance.curHp-=25;
            }
            else
            {
                Data_Manager.instance.curHp--;
            }
            Ui_Manager.instance.UiRefresh();
>>>>>>> main
        }
    }

    IEnumerator DestroyEffect()
    {
        destroyNum++;
        if (destroyNum == 4)
        {
            destroyNum = 0;
        }
       
        particleSystem.Play();
        destroyEffect.SetActive(true);
        
        destroyEffect.SetActive(false);

        yield return null;
    }
}
