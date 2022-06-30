using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }

    // Update is called once per frame
   /* void Update()
    {
        if (this.gameObject.name == "Button 1")
        {
            if (Input.GetButton("Button 1"))
            {
                spriteRenderer.color = Color.black;
                //Debug.Log("pressing key!");
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
        if (gameObject.name == "Button 2")
        {
            if (Input.GetButton("Button 2"))
            {
                spriteRenderer.color = Color.black;
                //Debug.Log("pressing key!");
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
        if (gameObject.name == "Button 3")
        {
            if (Input.GetButton("Button 3"))
            {
                spriteRenderer.color = Color.black;
                //Debug.Log("pressing key!");
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
        if (gameObject.name == "Button 4")
        {
            if (Input.GetButton("Button 4"))
            {
                //spriteRenderer.color = Color.black;
                //Debug.Log("pressing key!");
            }
            else
            {
                //spriteRenderer.color = Color.white;
            }
        }
    }
   */
}
