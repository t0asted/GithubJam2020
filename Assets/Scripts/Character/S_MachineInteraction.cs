using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class S_MachineInteraction : MonoBehaviour
{
    [SerializeField]
    private float interactionRadius = 1.0f;
    [SerializeField]
    private S_CharacterController m_CharacterController;
    public S_MachineBase MachineFound = null;
    public bool Interacting = false;

    private void Update()
    {
        if (MachineFound != null)
        {
            MachineFound.Interactable = true;
            if (Input.GetKeyDown("e"))
            {
                Interact(!Interacting);
            }
        }
    }

    public void Interact(bool interactPass)
    {
        if(interactPass != Interacting)
        {
            m_CharacterController.SetInteracting(interactPass);
            Interacting = interactPass;

            if (interactPass)
            {
                MachineFound.Interact();
            }
            else
            {
                MachineFound.UnInteract();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<S_MachineBase>() != null)
        {
            MachineFound = other.gameObject.GetComponent<S_MachineBase>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(MachineFound != null)
        {
            if (other.gameObject.GetComponent<S_MachineBase>() == MachineFound)
            {
                Interact(false);
                MachineFound.Interactable = false;
                MachineFound = null;
            }
        }
    }

}
