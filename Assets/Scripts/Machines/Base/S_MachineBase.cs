using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class S_MachineBase : MonoBehaviour
{
    public S_GameController ref_GameController = null;
    [SerializeField]
    public CL_Machine MachineData = new CL_Machine();
    [SerializeField]
    public bool MachineRunning = true;
    [SerializeField]
    public TextMeshProUGUI TextToShowContent;
    [SerializeField]
    private GameObject UIToolTip;
    [SerializeField]
    private UnityEvent OnInteract;

    private bool Interactable;

    private void Start()
    {
        if (GameObject.Find("_GameController") != null)
        {
            ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
        }
        else
        {
            Debug.Log("No Game controller!");
        }
    }

    private void Update()
    {
        //Temp
        if (TextToShowContent != null)
        {
            TextToShowContent.text = MachineRunning ? "Machine running" : "Machine is off";
        }

        if(UIToolTip.activeInHierarchy != Interactable)
        {
            UIToolTip.SetActive(Interactable);
        }
    }

    public void Interact()
    {
        OnInteract.Invoke();
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
        if (MachineData.Level == 1)
            return ref_GameController.GameData.Storage.HasResource(new CL_Resource(Enum_Items.Upgrade_Pack_1, 1));
        else if (MachineData.Level == 2)
            return ref_GameController.GameData.Storage.HasResource(new CL_Resource(Enum_Items.Upgrade_Pack_2, 1));

        return MachineData.Level >= 3;
    }

}