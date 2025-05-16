using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject questLineGameObject;


    public void Interact()
    {
        Debug.Log("Interact");
        questLineGameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
    }


    public string GetInteractText()
    {
        return interactText;
    }

}
