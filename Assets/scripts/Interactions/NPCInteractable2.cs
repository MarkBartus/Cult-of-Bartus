using UnityEngine;

public class NPCInteractable2 : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject blacksmithGameObject;


    public void Interact()
    {
        Debug.Log("Interact");
        blacksmithGameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
    }


    public string GetInteractText()
    {
        return interactText;
    }
}
