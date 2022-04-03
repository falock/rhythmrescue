using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningCD : MonoBehaviour
{
    public bool isCD;
    private float velocity = 4f;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isCD && RhythmManager.instance.isPlaying)
        {
            this.gameObject.transform.Rotate(new Vector3(0, 0, -360) * Time.deltaTime);
            //LeanTween.rotate(this.gameObject, new Vector3(0, 0, 360), velocity);
        }
        if (!isCD && RhythmManager.instance.teamHealth / RhythmManager.instance.originalTeamHealth < 0.3f && !LeanTween.isTweening())
        {
            LeanTween.cancel(gameObject);
            transform.localScale = Vector3.one;
            LeanTween.scale(gameObject, Vector3.one * 1.2f, 1f).setEasePunch();
            //LeanTween.value(gameObject, 0, 1, 1).setEasePunch().setOnUpdate();

        }
    }
}
