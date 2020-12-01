using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_HUDController : MonoBehaviour
{
    public TextMeshProUGUI AluminiumCount;
    public TextMeshProUGUI IronCount;
    public TextMeshProUGUI TitaniumCount;

    public GameObject ResourcePanel;
    public List<GameObject> UpgradeIcons;
    public Animator animator;

    [Header("Jetpack Meter")]
    [SerializeField]
    private Image m_JetpackOuterMeter = null;
    [SerializeField]
    private Image m_JetpackMeter = null;
    [SerializeField]
    private Sprite m_OverchargeSourceImage = null;

    [Header("ToolTips")]
    [SerializeField]
    private GameObject m_Tooltip = null;
    [SerializeField]
    private TextMeshProUGUI m_TooltipTitle = null;
    [SerializeField]
    private TextMeshProUGUI m_TooltipDesc = null;


    [Header("Timer")]
    [SerializeField]
    private TextMeshProUGUI TimerText = null;

    private List<Tooltips> TooltipsSent;



    private S_GameController ref_GameController = null;
    private S_CharacterController CharacterController = null;


    // Start is called before the first frame update
    void Start()
    {

        if (GameObject.Find("_GameController") != null)
        {
            ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
            CharacterController = ref_GameController.CharacterController;
            ref_GameController.SetHUDScript(this);
        }
        else
            Debug.Log("Level spawner : Did not find gamecontroller");

        TooltipsSent = new List<Tooltips>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory")) OpenPanel();
        UpdateJetpack();
        UpdateTimer();
    }

    public void OpenPanel()
    {
        if(ResourcePanel != null)
        {
            
            if(animator != null)
            {
                bool isOpen = animator.GetBool("Open");
                animator.SetBool("Open", !isOpen);
                Debug.Log("I pressed");
            }
        }
    }

    private void UpdateJetpack()
    {
        if (CharacterController)
        {
            float[] JetpackArgs = CharacterController.GetJetpackRemaining();
            if (JetpackArgs[0] == 1) m_JetpackMeter.sprite = m_OverchargeSourceImage;
            m_JetpackMeter.fillAmount = JetpackArgs[1] / JetpackArgs[2];
        }
    }

    private void UpdateTimer()
    {
        if (ref_GameController) TimerText.text = ref_GameController.GetTime();
    }

    public void TriggerTooltip (Tooltips tt)
    {
        if (TooltipsSent.Contains(tt)) return;
        
        switch (tt) {
            case Tooltips.JetpackUpgrade:
                HelperTooltip(
                    "NEED JETPACK UPGRADE",
                    "Your Jetpack does not work away from the ship. An upgrade " +
                    "is required, found in the upgrade fabricator.",
                    200
                );
                break;
        }
        TooltipsSent.Add(tt);
    }

    private void HelperTooltip(string title, string description, float height=200)
    {
        // Set details Correctly
        m_Tooltip.GetComponent<RectTransform>().sizeDelta = new Vector2(500, height);
        m_TooltipTitle.text = title;
        m_TooltipDesc.text = description;
        m_Tooltip.GetComponent<Animator>().SetTrigger("TriggerTooltip");
    }

}

public enum Tooltips
{
    JetpackUpgrade = 1
}
