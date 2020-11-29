using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private UnityEvent OnUnInteract;
    public bool Interactable;
    public bool Interacting;

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

        if(UIToolTip.activeInHierarchy != Interactable && !Interacting)
        {
            UIToolTip.SetActive(Interactable);
        }
    }

    public void Interact()
    {
        Interacting = true;
        OnInteract.Invoke();
    }

    public void UnInteract()
    {
        Interacting = false;
        OnUnInteract.Invoke();
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
        if(MachineData.CanLevelUp)
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