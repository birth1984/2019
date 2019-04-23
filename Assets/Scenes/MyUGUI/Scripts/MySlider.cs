using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MySlider : MonoBehaviour
{
    [SerializeField]
    private Slider m_slider;
    // Start is called before the first frame update
    void Start()
    {
        m_slider.onValueChanged.AddListener(OnSlider);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSlider(float value)
    {
        Debug.Log("On Slider Change " + value.ToString());
    }
}
