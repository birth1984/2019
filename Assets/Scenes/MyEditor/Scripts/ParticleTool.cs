using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;


// Unity3D研究院编辑器之获取粒子准确数量（三十）
//https://www.xuanyusong.com/archives/4496

public class ParticleTool : MonoBehaviour
{
    private ParticleSystem[] m_particleSystem;
    private MethodInfo m_calculateEffectUIDataMethord;
    private int m_particleCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_particleSystem = GetComponentsInChildren<ParticleSystem>();
        m_calculateEffectUIDataMethord = typeof(ParticleSystem).GetMethod("CalculateEffectUIData", BindingFlags.Instance | BindingFlags.NonPublic);
    }

    // Update is called once per frame
    void Update()
    {
        m_particleCount = 0;
        foreach(ParticleSystem ps in m_particleSystem)
        {
            int count = 0;
            object[] invokeArgs = new object[] { count, 0.0f, Mathf.Infinity };
            m_calculateEffectUIDataMethord.Invoke(ps, invokeArgs);
            count = (int)invokeArgs[0];
            m_particleCount += count;
        }
    }

    private void OnGUI()
    {
        Debug.Log("m_particleCount:" + m_particleCount);
        GUILayout.Label(string.Format("<size=50>m_particleCount:{0}</size>", m_particleCount));
    }
}
