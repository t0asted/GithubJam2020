using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ThrusterController : MonoBehaviour
{
    [Space]
    [SerializeField]
    private Color m_JetpackColour = new Color(190, 120, 6, 255);
    [SerializeField]
    private Color m_JetpackOverchargeColour = new Color(102, 51, 153, 1);
    [Space]
    [SerializeField]
    private GameObject m_JetpackLeft = null;
    [SerializeField]
    private GameObject m_JetpackRight = null;
    [SerializeField]
    private GameObject m_ThrusterDownLeft = null;
    [SerializeField]
    private GameObject m_ThrusterDownRight = null;
    [SerializeField]
    private GameObject m_ThrusterLeftTop = null;
    [SerializeField]
    private GameObject m_ThrusterLeftBottom = null;
    [SerializeField]
    private GameObject m_ThrusterRightTop = null;
    [SerializeField]
    private GameObject m_ThrusterRightBottom = null;
    [SerializeField]
    private GameObject m_ThrusterForwardLeft = null;
    [SerializeField]
    private GameObject m_ThrusterForwardRight = null;
    [SerializeField]
    private GameObject m_ThrusterBackwardLeft = null;
    [SerializeField]
    private GameObject m_ThrusterBackwardRight = null;



    // Start is called before the first frame update
    private void Start()
    {
        GameObject[] thrusters = new GameObject[]{
            m_JetpackLeft, m_JetpackRight, m_ThrusterLeftTop, m_ThrusterLeftBottom,
            m_ThrusterRightTop, m_ThrusterRightBottom, m_ThrusterForwardLeft,
            m_ThrusterForwardRight, m_ThrusterBackwardLeft, m_ThrusterBackwardRight,
            m_ThrusterDownLeft, m_ThrusterDownRight
        };

        foreach (GameObject toSet in thrusters) toSet.SetActive(false);
    }

    public void SetThruster(Thruster thruster, bool power)
    {
        switch (thruster)
        {
            case Thruster.Jetpack:
                m_JetpackLeft.SetActive(power);
                m_JetpackRight.SetActive(power);
                break;
            case Thruster.Down:
                m_ThrusterDownLeft.SetActive(power);
                m_ThrusterDownRight.SetActive(power);
                break;
            case Thruster.Right:
                m_ThrusterLeftTop.SetActive(power);
                m_ThrusterLeftBottom.SetActive(power);
                break;
            case Thruster.Left:
                m_ThrusterRightTop.SetActive(power);
                m_ThrusterRightBottom.SetActive(power);
                break;
            case Thruster.Forward:
                m_ThrusterForwardLeft.SetActive(power);
                m_ThrusterForwardRight.SetActive(power);
                break;
            case Thruster.Backward:
                m_ThrusterBackwardLeft.SetActive(power);
                m_ThrusterBackwardRight.SetActive(power);
                break;
            case Thruster.RollRight:
                m_ThrusterBackwardLeft.SetActive(power);
                break;
            case Thruster.RollLeft:
                m_ThrusterBackwardRight.SetActive(power);
                break;
        }
    }

    public void SetJetpackSpeed(bool overcharge)
    {
        ParticleSystem.MainModule left = m_JetpackLeft.GetComponent<ParticleSystem>().main;
        ParticleSystem.MainModule right = m_JetpackRight.GetComponent<ParticleSystem>().main;

        if (overcharge)
        {
            left.startColor = m_JetpackOverchargeColour;
            right.startColor = m_JetpackOverchargeColour;
        }
        else
        {
            left.startColor = m_JetpackColour;
            right.startColor = m_JetpackColour;
        }
    }

    public void ClearThrust()
    {
        SetThruster(Thruster.Down, false);
        SetThruster(Thruster.Left, false);
        SetThruster(Thruster.Right, false);
        SetThruster(Thruster.Forward, false);
        SetThruster(Thruster.Backward, false);
    }

}

public enum Thruster
{
    Jetpack = 1,
    Down = 2,
    Forward = 3,
    Backward = 4,
    Right = 5,
    Left = 6,
    RollLeft = 7,
    RollRight = 8,
} 
