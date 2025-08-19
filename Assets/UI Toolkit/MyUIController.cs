using UnityEngine;
using UnityEngine.UIElements;

public class MyUIController : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Label label = root.Q<Label>("myLabel");
        Button button = root.Q<Button>("myButton");

        button.clicked += () => { label.text = "Button Clicked!"; };
    }
}
