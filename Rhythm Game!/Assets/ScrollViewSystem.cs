using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSystem : MonoBehaviour
{
    public ScrollRect scrollRect;

    [SerializeField] private MoveFriendList rightButton;
    [SerializeField] private MoveFriendList leftButton;

    private float scrollSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftButton != null)
        {
            if (leftButton.isDown)
            {
                ScrollLeft();
            }
        }

        if (rightButton != null)
        {
            if (rightButton.isDown)
            {
                ScrollRight();
            }
        }
    }

    private void ScrollLeft()
    {
        if (scrollRect != null)
        {
            if(scrollRect.horizontalNormalizedPosition >= 0f)
            {
                scrollRect.horizontalNormalizedPosition -= scrollSpeed;
            }
        }
    }

    private void ScrollRight()
    {
        if (scrollRect != null)
        {
            if (scrollRect.horizontalNormalizedPosition <= 1f)
            {
                scrollRect.horizontalNormalizedPosition += scrollSpeed;
            }
        }
    }
}
