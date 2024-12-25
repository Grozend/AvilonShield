using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveing : MonoBehaviour
{
    public float moveSpeed = 2f; // Скорость движения
    public float lowerLimit = -5f; // Нижний предел
    private bool isMoving = false;

    void Update()
    {
        MovePlatformDown();
    }

    public void StartMovingDown()
    {
        isMoving = true; // Начинаем движение
    }

    void MovePlatformDown()
    {
        if (isMoving)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;

            if (transform.position.y <= lowerLimit)
            {
                isMoving = false; // Останавливаем движение при достижении нижнего предела
            }
        }
    }
}
