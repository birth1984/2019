using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnAddListener : MonoBehaviour
{
    [SerializeField]
    private Button m_button;
    // Start is called before the first frame update
    void Start()
    {
        m_button.onClick.AddListener(OnClickButton);
    }

    public void OnClickButton()
    {
        Debug.Log("Button is clicked");
    }

    
}
