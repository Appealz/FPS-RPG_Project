using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : DestroySingleton<LoadingManager>
{
    private Image progressBar;

    protected override void DoAwake()
    {
        var progress = MyUtility.GetChildrenTrans(transform, "Progress");
        if(progress != null)
        {
            if (!progress.TryGetComponent<Image>(out progressBar))
                Debug.Log("LoadingManager.cs - DoAwake() - Find ProgressBar Image");
            else
                progressBar.fillAmount = 0f;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private async void Start()
    {
        var token = this.GetCancellationTokenOnDestroy();
        await LoadScene(token);
    }

    private async UniTask LoadScene(CancellationToken token)
    {
        string nextScene = SceneLoadManager.GetNextScene();

        var op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        var minDisplay = UniTask.Delay(500, cancellationToken: token);

        while(!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress);
            if(progressBar != null)
                progressBar.fillAmount = progress;

            // todo 추후에 텍스트같은거

            if(op.progress >= 0.9f)
            {
                await minDisplay;
                op.allowSceneActivation = true;
            }
            await UniTask.Yield(token);
        }
    }
}
