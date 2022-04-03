using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Bigger();
    }

    private void Bigger()
    {
        LeanTween.scale(this.gameObject, new Vector2(1.5f, 1.5f), 1);
        Smaller();
    }

    private void Smaller()
    {
        LeanTween.scale(this.gameObject, new Vector2(1, 1), 1);
    }
}
