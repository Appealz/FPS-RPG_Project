using System;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemView : MonoBehaviour
{
    ItemViewModel viewModel;
    Button mainBtn;
    Button subBtn;
    Button revolverBtn;
    Button knifeBtn;
    Button healkitBtn;

    private void Awake()
    {
        viewModel = new ItemViewModel(new ItemModel());

        var root = GetComponent<UIDocument>().rootVisualElement;
        mainBtn = root.Q<Button>("Main");
        subBtn = root.Q<Button>("Sub");
        revolverBtn = root.Q<Button>("Revolver");
        knifeBtn = root.Q<Button>("Knife");
        healkitBtn = root.Q<Button>("Healkit");

        foreach(itemSlotType type in Enum.GetValues(typeof(itemSlotType)))
        {
            var btn = root.Q<Button>(type.ToString());
            if (btn != null)
            {
                var capturedSlot = type;
                btn.clicked += async () => await viewModel.ShowItem(capturedSlot);
            }
        }

        viewModel.PropertyChanged += OnChangeItem;
    }

    private void OnChangeItem(object sender, PropertyChangedEventArgs evt)
    {
        if(evt.PropertyName == nameof(ItemViewModel.SelectItem))
        {
            Debug.Log($"{viewModel.SelectItem} º±≈√");
        }
    }


}
