using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransitionButton : MonoBehaviour
{
    [SerializeField] private string transitionSceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;

        entry.callback.AddListener((data) =>
        {
            SceneTransitionManager.Instance.OnLoadScendClicked(transitionSceneName);
        });

        trigger.triggers.Add(entry);
    }
}
