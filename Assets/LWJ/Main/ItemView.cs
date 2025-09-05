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
    Label nameLabel;
    Label damageLabel;
    Label descriptionLabel;
    Button nextBtn;
    Button prevBtn;


    private void Awake()
    {
        
    }

    public void BindViewModel(ItemViewModel newModel)
    {
        viewModel = newModel;

        var root = GetComponent<UIDocument>().rootVisualElement;
        mainBtn = root.Q<Button>("Main");
        subBtn = root.Q<Button>("Sub");
        revolverBtn = root.Q<Button>("Revolver");
        knifeBtn = root.Q<Button>("Knife");
        healkitBtn = root.Q<Button>("Healkit");
        nameLabel = root.Q<Label>("Name");
        damageLabel = root.Q<Label>("Damage");
        descriptionLabel = root.Q<Label>("Des");
        nextBtn = root.Q<Button>("NextBtn");
        prevBtn = root.Q<Button>("PrevBtn");


        foreach (itemSlotType type in Enum.GetValues(typeof(itemSlotType)))
        {
            var btn = root.Q<Button>(type.ToString());
            if (btn != null)
            {
                var capturedSlot = type;
                btn.clicked += async () => await viewModel.ShowItem(capturedSlot);
            }
        }

        viewModel.PropertyChanged += OnChangeItem;
        prevBtn.clicked += () => viewModel.ShowNextItem(-1);
        nextBtn.clicked += () => viewModel.ShowNextItem(1);
    }

    private void OnChangeItem(object sender, PropertyChangedEventArgs evt)
    {
        //if(evt.PropertyName == nameof(ItemViewModel.SelectItem))
        //{
        //    Debug.Log($"{viewModel.SelectItem} º±≈√");

        //}

        if (evt.PropertyName == nameof(ItemViewModel.ItemName))
        {
            nameLabel.text = $"Name : {viewModel.ItemName}";
            damageLabel.text = $"Damage : {viewModel.itemDamage}";
            descriptionLabel.text = $"{viewModel.itemDescription}";
        }
    }

}
