using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeanTweenBiggerSmaller : MonoBehaviour
{
    private void Start()
    {
        UIAnimate();
    }

    private void UIAnimate()
    {
        //LeanTween.scale(this.gameObject, new Vector3(1, 1, 1), 1f).setEase(LeanTweenType.easeInExpo).setDelay(0.1f);
        LeanTween.scale(this.gameObject, new Vector3(1.1f, 1.1f, 1.1f), 1f).setLoopType(LeanTweenType.pingPong).setDelay(1.2f);
    }
}
