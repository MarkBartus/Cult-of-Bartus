using System.Collections.Generic;
using UnityEngine;

public class PlayerIneract : MonoBehaviour
{

    public NPCInteractable NPC1;
    public NPCInteractable2 NPC2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact();
                }

                if (collider.TryGetComponent(out NPCInteractable2 npcInteractable2))
                {
                    npcInteractable2.Interact();
                    
                }
            }

            
        }
    }

    public NPCInteractable GetInteractableObject()
    {
        List<NPCInteractable> npcInteractablesList = new List<NPCInteractable>();
        float interactRange = 3f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable npcInteractable))
            {
                npcInteractablesList.Add(npcInteractable);
            }
        }

        NPCInteractable closestNPCInteractable = null;
        foreach (NPCInteractable npcInteractable in npcInteractablesList)
        {
            if (closestNPCInteractable == null)
            {
                closestNPCInteractable = npcInteractable;
            }
            else
            {
                if (Vector3.Distance(transform.position, npcInteractable.transform.position) <
                    Vector3.Distance(transform.position, closestNPCInteractable.transform.position))
                {
                    closestNPCInteractable = npcInteractable;
                }
            }
        }

        return closestNPCInteractable;

    }


    public NPCInteractable2 GetInteractableObject2()
    {
        List<NPCInteractable2> npcInteractablesList = new List<NPCInteractable2>();
        float interactRange = 3f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable2 npcInteractable2))
            {
                npcInteractablesList.Add(npcInteractable2);
            }
        }

        NPCInteractable2 closestNPCInteractable = null;
        foreach (NPCInteractable2 npcInteractable2 in npcInteractablesList)
        {
            if (closestNPCInteractable == null)
            {
                closestNPCInteractable = npcInteractable2;
            }
            else
            {
                if (Vector3.Distance(transform.position, npcInteractable2.transform.position) <
                    Vector3.Distance(transform.position, closestNPCInteractable.transform.position))
                {
                    closestNPCInteractable = npcInteractable2;
                }
            }
        }

        return closestNPCInteractable;

    }

}
