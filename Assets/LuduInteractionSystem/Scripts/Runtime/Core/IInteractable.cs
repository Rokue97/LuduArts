using UnityEngine;

public interface IInteractable
{
    void Interact(Inventory inventory);

    string GetPromptName();
    bool CanInteract() => true;
}
