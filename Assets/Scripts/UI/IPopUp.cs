using UnityEngine;
using UnityEngine.EventSystems;
public interface IPopUp
{
    void ShowPopup(EventSystem eventSystem, GameObject newFirstSelected);
}
