using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class S_Interact : MonoBehaviour
{
    [SerializeField]
    private S_MachineBase MachineToInteract;
    public void Interact()
    {
        MachineToInteract.Interact();
    }
}
