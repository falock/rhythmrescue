using UnityEngine;
using UnityEngine.EventSystems; 

public class MoveFriendList : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
{
    public bool isDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
    }
}
