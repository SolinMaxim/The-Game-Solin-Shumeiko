using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreen : MonoBehaviour
{
    private bool isFullScreen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            Screen.fullScreen = !Screen.fullScreen;
    }
}
