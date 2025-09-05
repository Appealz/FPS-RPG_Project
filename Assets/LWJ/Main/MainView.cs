using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class MainView : MonoBehaviour
{
    MainViewModel viewModel;
    ItemView ItemView;
    Label classLabel;
    Button leftBtn;
    Button rightBtn;
    Button itemBtn;
    Button shopBtn;
    Button perkBtn;
    Button startBtn;    
    VisualElement item;
    VisualElement background;
    VisualElement shop;

    private void Awake()
    {
        viewModel = new MainViewModel();
        ItemView = GetComponent<ItemView>();
        ItemView.BindViewModel(viewModel.itemVM);
        var root = GetComponent<UIDocument>().rootVisualElement;
        classLabel = root.Q<Label>("ClassLabel");
        leftBtn = root.Q<Button>("LeftBtn");
        rightBtn = root.Q<Button>("RightBtn");
        itemBtn = root.Q<Button>("ItemBtn");
        shopBtn = root.Q<Button>("ShopBtn");
        perkBtn = root.Q<Button>("PerkBtn");
        startBtn = root.Q<Button>("GameStartBtn");
        item = root.Q<VisualElement>("VisualElement_Item");
        item.style.display = DisplayStyle.None;
        shop = root.Q<VisualElement>("VisualElement_Shop");
        shop.style.display = DisplayStyle.None;
        background = root.Q<VisualElement>("VisualElement_Background");

        leftBtn.clicked += () => viewModel.classVM.ChangeClass(-1);
        rightBtn.clicked += () => viewModel.classVM.ChangeClass(1);
        itemBtn.clicked += () => viewModel.OpenItem();
        shopBtn.clicked += () => viewModel.OpenShop();
        perkBtn.clicked += () => viewModel.OpenPerk();
        startBtn.clicked += () => viewModel.StartGame();

        viewModel.classVM.PropertyChanged += OnClassPropertyChanged;
        viewModel.PropertyChanged += OnChangeItemPopUp;
        viewModel.PropertyChanged += OnChangeShopPopUp;
        classLabel.text = viewModel.classVM.SelectedClass;

        background.RegisterCallback<ClickEvent>(evt =>
        {
            if (viewModel.IsOpenItemPopUp || viewModel.IsOpenShopPopUp)
            {
                viewModel.ClosePopUp();
            }
        });
    }

    private void Start()
    {
        
    }

    private void OnClassPropertyChanged(object sender, PropertyChangedEventArgs evt)
    {
        if(evt.PropertyName == nameof(ClassViewModel.SelectedClass))
        {
            classLabel.text = viewModel.classVM.SelectedClass;
        }
    }

    private void OnChangeItemPopUp(object sender, PropertyChangedEventArgs evt)
    {
        if(evt.PropertyName == nameof(MainViewModel.IsOpenItemPopUp))
        {
            item.style.display = viewModel.IsOpenItemPopUp ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }

    private void OnChangeShopPopUp(object sender, PropertyChangedEventArgs evt)
    {
        if(evt.PropertyName == nameof(MainViewModel.IsOpenShopPopUp))
        {
            shop.style.display = viewModel.IsOpenShopPopUp ? DisplayStyle.Flex : DisplayStyle.None;
        } 
    }

    private void OnDestroy()
    {
        if (viewModel != null)
        {
            viewModel.classVM.PropertyChanged -= OnClassPropertyChanged;
            viewModel.classVM.PropertyChanged -= OnChangeItemPopUp;
            viewModel.classVM.PropertyChanged -= OnChangeShopPopUp;
        }
    }
}
