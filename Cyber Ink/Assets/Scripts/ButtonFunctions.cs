using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public void MouseHover()
    {
        AudioManager.Instance.PlayEffectsOneShot("Mouse Hover");
    }

    public void MouseClick()
    {
        AudioManager.Instance.PlayEffectsOneShot("Mouse Click");
    }
}
