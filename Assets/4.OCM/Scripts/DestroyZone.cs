using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    ParticleSystem particleSystem0;
    ParticleSystem particleSystem1;
    ParticleSystem particleSystem2;
    ParticleSystem particleSystem3;

    public GameObject destroyEffect0;
    public GameObject destroyEffect1;
    public GameObject destroyEffect2;
    public GameObject destroyEffect3;

    public int destroyNum;

    // Start is called before the first frame update
    void Start()
    {
        destroyEffect0.transform.position = transform.position;
        particleSystem0 = destroyEffect0.GetComponent<ParticleSystem>();

        destroyEffect1.transform.position = transform.position;
        particleSystem1 = destroyEffect1.GetComponent<ParticleSystem>();

        destroyEffect2.transform.position = transform.position;
        particleSystem2 = destroyEffect2.GetComponent<ParticleSystem>();

        destroyEffect3.transform.position = transform.position;
        particleSystem3 = destroyEffect3.GetComponent<ParticleSystem>();

        destroyNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider others)
    {
        if (others.gameObject.tag == "Monster")
        {
            switch (destroyNum)
            {
                case 0:
                    particleSystem0.Play();
                    break;
                case 1:
                    particleSystem1.Play();
                    break;
                case 2:
                    particleSystem2.Play();
                    break;
                case 3:
                    particleSystem3.Play();
                    break;
            }
            StageManager.instance.monsterCount--;
            Destroy(others.gameObject);
            Data_Manager.instance.curHp--;
            Ui_Manager.instance.UiRefresh();
            destroyNum++;
            if (destroyNum == 4)
            {
                destroyNum = 0;
            }
        }
    }
}
