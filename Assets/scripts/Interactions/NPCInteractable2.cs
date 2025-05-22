using UnityEngine;

public class NPCInteractable2 : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject blacksmithGameObject;
    public GameObject Player;
    public GameObject interactObject;
    public float range;
    public bool isInteractable1 = false;

    public void Interact()
    {
        Debug.Log("Interact");
        blacksmithGameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        isInteractable1 = true;
    }
    
    public void Update()
    {
        inter();
    }
    public void inter()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= range && isInteractable1 == false)
        {

            interactObject.SetActive(true);

        }
        else
        {
            interactObject.SetActive(false);
        }
    }
    public void clear()
    {
        isInteractable1 = false;
    }
    
    public string GetInteractText()
    {
        return interactText;
    }
}
