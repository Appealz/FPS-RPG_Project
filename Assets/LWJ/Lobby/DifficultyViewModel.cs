using UnityEngine;

public class DifficultyViewModel : ViewModelBase
{
    DifficultyModel model;
    public Difficulty Difficulty
    {
        get => model.difficulty;
        private set
        {
            if(model.difficulty != value)
            {
                model.difficulty = value;
                OnPropertyChanged(nameof(Difficulty));
                OnPropertyChanged(nameof(difficultyText));
                Debug.Log(model.difficulty);
            }
        }
    }

    public string difficultyText => Difficulty.ToString();

    public DifficultyViewModel(DifficultyModel newModel)
    {
        model = newModel;
        Difficulty = model.difficulty;
    }

    public void ChangeDifficulty(int dir)
    {
        int index = (int)Difficulty;
        index += dir;

        if(index < (int)Difficulty.Easy)
        {
            index = (int)Difficulty.Easy;
        }
        if(index > (int)Difficulty.Hard)
        {
            index = (int)(Difficulty.Hard);
        }

        Difficulty = (Difficulty)index;
    }
}
