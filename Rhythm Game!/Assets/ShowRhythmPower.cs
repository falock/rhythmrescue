using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRhythmPower : MonoBehaviour
{
    public float rhythmPower = 0;
    private Slider slider;
    public float fillSpeed = 0.5f;
    
    private void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update()
    {
        if (slider.value < rhythmPower)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
    }
}
