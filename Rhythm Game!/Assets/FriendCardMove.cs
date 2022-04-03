using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendCardMove : MonoBehaviour
{
    public void Move(float x)
    {
        LeanTween.moveX(this.gameObject, x, 0.2f);
    }
}
