using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveing : MonoBehaviour
{
    public float moveSpeed = 2f; // �������� ��������
    public float lowerLimit = -5f; // ������ ������
    private bool isMoving = false;

    void Update()
    {
        MovePlatformDown();
    }

    public void StartMovingDown()
    {
        isMoving = true; // �������� ��������
    }

    void MovePlatformDown()
    {
        if (isMoving)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;

            if (transform.position.y <= lowerLimit)
            {
                isMoving = false; // ������������� �������� ��� ���������� ������� �������
            }
        }
    }
}
