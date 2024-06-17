using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : Singleton<DialogManager>
{
    [SerializeField] GameObject dialogPanel;
    [SerializeField] Image npcIcon;
    [SerializeField] TextMeshProUGUI npcName;
    [SerializeField] TextMeshProUGUI npcDialog;

    Queue<string> dialogQueue = new Queue<string>();

    public NPCInteraction NPCSelected { get; set; }

    bool dialogStarted;
    PlayerActions playerActions;

    public static event Action<InteractionType> OnExtraInteraction;

    private void Start()
    {
        playerActions.Dialog.Interact.performed += ctx => HandleDialog();

    }

    public override void Awake()
    {
        base.Awake();
        playerActions = new PlayerActions();
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    void LoadDialogFromNPC()
    {
        if(NPCSelected.DialogToShow.Chat.Length <= 0)
            return;

        foreach(string line in NPCSelected.DialogToShow.Chat)
        {
            dialogQueue.Enqueue(line);
        }
    }

    void HandleDialog()
    {
        if (NPCSelected == null) return;

        if (!dialogStarted)
        {
            dialogPanel.SetActive(true);
            LoadDialogFromNPC();

            npcIcon.sprite = NPCSelected.DialogToShow.Icon;
            npcName.text = NPCSelected.DialogToShow.Name;
            npcDialog.text = NPCSelected.DialogToShow.Greeting;
            dialogStarted = true;
        }
        else
        {
            if (dialogQueue.Count <= 0)
            {
                CloseDialogPanel();
                OnExtraInteraction?.Invoke(NPCSelected.DialogToShow.Type);
                return;
            }
            else
                npcDialog.text = dialogQueue.Dequeue();
        }
    }

    public void CloseDialogPanel()
    {
        dialogPanel.SetActive(false);
        dialogStarted = false;
        dialogQueue.Clear();
    }

}