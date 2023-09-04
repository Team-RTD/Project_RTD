using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public float speed = 10.0f;
    public float scroll_speed = 10.0f;
    public Transform cameratar;

    private Camera _camera;
    private Vector3 _worldDefaltForward;
    float slerf = 0f;
    float timerun;
    public float duration = 2f;

    public float scrollSpeed = 5f; // ī�޶� �̵� �ӵ�
    public float edgeThreshold = 10f; // ȭ�� �����ڸ��� Ŀ�� ���� �Ÿ�


    public float  rotationSpeed = 30f; // ȸ�� �ӵ�
    private float targetRotation = 0f; // ��ǥ ȸ����

    private Vector3 targetPosition; // ��ǥ ��ġ


    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _worldDefaltForward = transform.forward;
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scroll_speed;

        if(_camera.fieldOfView <= 20.0f && scroll <0 )
        {
            _camera.fieldOfView = 20.0f;
        }
        else if(_camera.fieldOfView >= 80.0f && scroll > 0)
        {

            _camera.fieldOfView = 80.0f;
        }else
        {


            _camera.fieldOfView += scroll *2;

            _camera.transform.Rotate(scroll * 2 , 0, 0);
            _camera.transform.Translate(0, 0, scroll*1.3f, Space.World);
        }

        Vector3 mousePosition = Input.mousePosition;
        Vector3 moveDirection = Vector3.zero;
 

        // ���콺 Ŀ���� ȭ�� �����ڸ��� ������ �̵� ���� ����
        if ((mousePosition.x < edgeThreshold || Input.GetKey(KeyCode.A))&&transform.position.x>-11)
        {
            //moveDirection += Vector3.left;
            targetPosition -=  transform.right *0.1f *speed;
        }
        else if ((mousePosition.x > Screen.width - edgeThreshold || Input.GetKey(KeyCode.D))&&transform.position.x < 14)
        {
            // moveDirection += Vector3.right;
            targetPosition +=  transform.right * 0.1f * speed;
        }

        if ((mousePosition.y < edgeThreshold || Input.GetKey(KeyCode.S))&& transform.position.z > -22)
        {
            //moveDirection += Vector3.back;
            targetPosition -= transform.forward * 0.1f * speed;
        }
        else if ((mousePosition.y > Screen.height - edgeThreshold || Input.GetKey(KeyCode.W))&& transform.position.z < 5)
        {
            //moveDirection += Vector3.forward;
            targetPosition +=  transform.forward * 0.1f * speed;
        }

        // �̵� ������ �����Ǿ����� ī�޶� �̵�
        //if (moveDirection != Vector3.zero)
       // {
            //MoveCamera(moveDirection.normalized);
        // }




        if(Input.GetKey(KeyCode.Q))
        {

            // slerf -= Time.deltaTime*2;
            // _camera.transform.Rotate(0, slerf, 0, Space.World);
            targetRotation -= rotationSpeed * Time.deltaTime;
            // �ε巴�� ȸ����Ű�� ���� Lerp ���

        }
        else if(Input.GetKey(KeyCode.E))
        {

            //slerf += Time.deltaTime*2;
            //_camera.transform.Rotate(0, slerf, 0, Space.World);
            targetRotation += rotationSpeed * Time.deltaTime;
            // �ε巴�� ȸ����Ű�� ���� Lerp ���
  
        }
        else
        {
            if (slerf != 0 )
            {

                _camera.transform.Rotate(0, slerf, 0, Space.World);

                timerun += Time.deltaTime;
                slerf = Mathf.Lerp(slerf, 0, timerun / duration);
            }
            else
            {
                timerun = 0;
            }
             
        }

        transform.position =Vector3.Lerp(transform.position,targetPosition, 0.05f);
        // ī�޶� �̵� �������� �̵�

        float currentRotation = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation, Time.deltaTime *5);
        transform.eulerAngles = new Vector3(0f, currentRotation, 0f);

    }

    void MoveCamera(Vector3 moveDirection)
    {

        transform.Translate(moveDirection * scrollSpeed * Time.deltaTime);
    }
}




