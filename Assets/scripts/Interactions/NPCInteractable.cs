using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject questLineGameObject;
    public GameObject Player;
    public GameObject interactObject;
    public float range;
    public bool isInteractable = false;

    public void Interact()
    {
        Debug.Log("Interact");
        questLineGameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        isInteractable=true;
    }
    public void Update()
    {
        inter();
    }
    public void inter()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= range && isInteractable == false)
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
        isInteractable = false;
    }
    public string GetInteractText()
    {
        return interactText;
    }

}
