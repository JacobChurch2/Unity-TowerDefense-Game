using UnityEngine;
using UnityEngine.EventSystems;
public interface IPopUp {}

public static class PopupExtensions
{
    public static void ShowPopup(this IPopUp popup, EventSystem eventSystem, GameObject newFirstSelected)
    {
        if (eventSystem != null && newFirstSelected != null)
        {
            eventSystem.SetSelectedGameObject(newFirstSelected);
        }
    }
}
