using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class MainView : MonoBehaviour
{
    MainViewModel viewModel;
    Label classLabel;
    Button leftBtn;
    Button rightBtn;
    Button itemBtn;
    Button shopBtn;
    Button perkBtn;
    Button startBtn;

    private void Awake()
    {
        viewModel = new MainViewModel();
        var root = GetComponent<UIDocument>().rootVisualElement;
        classLabel = root.Q<Label>("ClassLabel");
        leftBtn = root.Q<Button>("LeftBtn");
        rightBtn = root.Q<Button>("RightBtn");
        itemBtn = root.Q<Button>("ItemBtn");
        shopBtn = root.Q<Button>("ShopBtn");
        perkBtn = root.Q<Button>("PerkBtn");
        startBtn = root.Q<Button>("GameStartBtn");

        leftBtn.clicked += () => viewModel.classVM.ChangeClass(-1);
        rightBtn.clicked += () => viewModel.classVM.ChangeClass(1);
        itemBtn.clicked += () => viewModel.OpenItem();
        shopBtn.clicked += () => viewModel.OpenShop();
        perkBtn.clicked += () => viewModel.OpenPerk();
        startBtn.clicked += () => viewModel.StartGame();

        viewModel.classVM.PropertyChanged += OnClassPropertyChanged;
        classLabel.text = viewModel.classVM.GetSelectedClassName();
    }

    private void Start()
    {
        
    }

    private void OnClassPropertyChanged(object sender, PropertyChangedEventArgs evt)
    {
        if(evt.PropertyName == nameof(viewModel.classVM.GetSelectedClassName))
        {
            classLabel.text = viewModel.classVM.GetSelectedClassName();
        }
    }

    private void RefreshUI()
    {
        
    }
}
