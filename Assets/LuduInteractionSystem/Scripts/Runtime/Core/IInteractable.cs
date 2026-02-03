using UnityEngine;

public interface IInteractable
{
    void Interact(Inventory inventory);
    void CancelInteract() { }
    string GetPrompt(string keyName);
    bool CanInteract() => true;
    bool IsLocked() => false;
    string GetLockedPrompt() => "";
}
