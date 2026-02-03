using UnityEngine;

public interface IInteractable
{
    void Interact(Inventory inventory);
    void CancelInteract() { }
    string GetPromptName();
    bool CanInteract() => true;
    bool IsLocked() => false;
}
