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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenPanel();
            
        }
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
}
