using UnityEngine;
using UnityEngine.SceneManagement;
public class LobbyViewModel : ViewModelBase
{
    public DifficultyViewModel DifficultyVM;

    public LobbyViewModel(DifficultyModel difficultyModel)
    {
        DifficultyVM = new DifficultyViewModel(difficultyModel);
    }

    public void StartGame()
    {
        if (ContextManager.Instance.GetPlayGameContext() == null)
            ContextManager.Instance.InitPlayGameContext(); // 직접 초기화 메소드 만들어둠

        ContextManager.Instance.GetPlayGameContext().difficulty = DifficultyVM.Difficulty;

        SceneManager.LoadScene("MainSample");
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
