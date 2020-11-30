using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CharacterUpgrade : MonoBehaviour
{
    [SerializeField]
    private bool m_JetpackUpgrade = false;
    [SerializeField]
    private bool m_ThrusterUpgrade = false;

    private S_GameController GC = null;

    private void Start()
    {
        if (GameObject.Find("_GameController") != null)
        {
            GC = GameObject.Find("_GameController").GetComponent<S_GameController>();
        }
        else Debug.Log("No Game controller!");
    }

    public bool HasJetpackUpgrade()
    {
        Debug.Log("Jetpack called");
        if (m_JetpackUpgrade) return true;
        if (GC.GameData.Storage.HasResource(new CL_Resource(Enum_Items.Upgrade_Jetpack, 1))) m_JetpackUpgrade = true;
        return m_JetpackUpgrade;
    }

    public bool HasThrusterUpgrade()
    {
        if (m_ThrusterUpgrade) return true;
        if (GC.GameData.Storage.HasResource(new CL_Resource(Enum_Items.Upgrade_Thrusters, 1))) m_ThrusterUpgrade = true;
        return m_ThrusterUpgrade;
    }
}
