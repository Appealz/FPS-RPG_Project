using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class LobbyView : MonoBehaviour
{
    LobbyViewModel viewModel;

    Button startBtn;
    Button optionBtn;
    Button exitBtn;
    Button leftBtn, rightBtn;
    Label difficultyLabel;

    private void Awake()
    {
        viewModel = new LobbyViewModel(new DifficultyModel());
        
        var root = GetComponent<UIDocument>().rootVisualElement;

        startBtn = root.Q<Button>("GameStart");
        optionBtn = root.Q<Button>("Option");
        exitBtn = root.Q<Button>("Exit");
        leftBtn = root.Q<Button>("Left");
        rightBtn = root.Q<Button>("Right");
        difficultyLabel = root.Q<Label>("Difficulty");

        startBtn.clicked += viewModel.StartGame;
        optionBtn.clicked += viewModel.OpenOption;
        exitBtn.clicked += viewModel.ExitGame;

        leftBtn.clicked += () => viewModel.DifficultyVM.ChangeDifficulty(-1);
        rightBtn.clicked += () => viewModel.DifficultyVM.ChangeDifficulty(1);

        viewModel.DifficultyVM.PropertyChanged += OnDifficultyChanged;

        difficultyLabel.text = viewModel.DifficultyVM.Difficulty.ToString();
    }

    private void OnDifficultyChanged(object sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(DifficultyViewModel.Difficulty))
        {
            difficultyLabel.text = viewModel.DifficultyVM.Difficulty.ToString();
        }
    }

    private void OnDestroy()
    {
        viewModel.Dispose();
    }
}
