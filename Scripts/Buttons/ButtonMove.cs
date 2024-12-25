using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMove : MonoBehaviour
{
    public PlatformMoveing platformMover;

    public void OnButtonPress()
    {
        if (platformMover != null)
        {
            platformMover.StartMovingDown();
        }
    }
}
