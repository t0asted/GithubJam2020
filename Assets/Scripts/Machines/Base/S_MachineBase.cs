using TMPro;
using UnityEngine;

public class S_MachineBase : MonoBehaviour
{
    protected S_GameController ref_GameController = null;

    [SerializeField]
    public CL_Machine MachineData = new CL_Machine();
    [SerializeField]
    private GameObject UIToolTip;

    [SerializeField]
    public TextMeshProUGUI TextToShowContent;
    [SerializeField]
    private S_OpenCloseUIScene UIMachine;
    [SerializeField]
    private S_OpenCloseUIScene UILighting;

    public bool Interactable = false;
    public bool Interacting = false;
    public bool MachineRunning = false;
    public bool Placed = false;

    public string Message = "";

    public virtual void Start()
    {
        if (GameObject.Find("_GameController") != null)
        {
            ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
        }
        else
        {
            Debug.Log("No Game controller!");
        }

        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }
    }

    public virtual void Update()
    {
        transform.GetComponent<Collider>().enabled = Placed;
        if (Placed)
        {
            if (UIToolTip != null)
            {
                if (UIToolTip.activeInHierarchy != Interactable && !Interacting)
                {
                    UIToolTip.SetActive(Interactable);
                }
            }
        }
    }

    public void Place()
    {
        Placed = true;
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = true;
        }
    }

    public void Interact()
    {
        Interacting = true;
        UIMachine.OpenScene();
        UILighting.OpenScene();
    }

    public void UnInteract()
    {
        Interacting = false;
        UIMachine.CloseScene();
        UILighting.CloseScene();
    }

    public void ToggleRunning()
    {
        MachineRunning = !MachineRunning;
    }

    public bool LevelUp()
    {
        if (CanLevelUp())
        {
            if (MachineData.Level == 1)
                ref_GameController.GameData.Storage.TakeResources(new CL_Resource(Enum_Items.Upgrade_Pack_1, 1));
            else if (MachineData.Level == 2)
                ref_GameController.GameData.Storage.TakeResources(new CL_Resource(Enum_Items.Upgrade_Pack_2, 1));
            MachineData.LevelUp();
            return true;
        }
        return false;
    }

    public bool CanLevelUp()
    {
        if (MachineData.CanLevelUp)
        {
            if (MachineData.Level == 1)
                return ref_GameController.GameData.Storage.HasResource(new CL_Resource(Enum_Items.Upgrade_Pack_1, 1));
            else if (MachineData.Level == 2)
                return ref_GameController.GameData.Storage.HasResource(new CL_Resource(Enum_Items.Upgrade_Pack_2, 1));
            return MachineData.Level >= 3;
        }
        return false;
    }

}