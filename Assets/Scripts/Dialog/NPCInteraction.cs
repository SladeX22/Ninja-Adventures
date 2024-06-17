using TMPro;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] Dialog dialogToShow;
    [SerializeField] GameObject interactionBox;

    public Dialog DialogToShow => dialogToShow;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            DialogManager.i.NPCSelected = this;
            DialogManager.i.CloseDialogPanel();
            interactionBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            DialogManager.i.NPCSelected = null;
            DialogManager.i.CloseDialogPanel();
            interactionBox.SetActive(false);
        }
    }


}