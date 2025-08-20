using UnityEngine;

public class LobbyViewModel : ViewModelBase
{
    public DifficultyViewModel DifficultyVM;

    public LobbyViewModel(DifficultyModel difficultyModel)
    {
        DifficultyVM = new DifficultyViewModel(difficultyModel);
    }

    public void StartGame()
    {

    }

    public void OpenOption()
    {

    }

    public void ExitGame()
    {

    }

    public override void Dispose()
    {
        DifficultyVM.Dispose();
        base.Dispose();
    }
}
