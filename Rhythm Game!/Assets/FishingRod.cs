using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public GameObject hook;
    public GameObject rodTip;

    public LineRenderer lineRenderer;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.SetPosition(0, rodTip.transform.position);
        lineRenderer.SetPosition(1, hook.transform.position);
        anim = gameObject.GetComponent<Animator>();
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        lineRenderer.SetPosition(0, rodTip.transform.position);
        lineRenderer.SetPosition(1, hook.transform.position);
    }
}
