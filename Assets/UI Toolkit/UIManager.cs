using UnityEngine;

public class UIManager : MonoBehaviour
{
    CalculateView view;
    CalculateViewModel viewModel;
    Calculator calculator;

    private void Awake()
    {
        view = FindAnyObjectByType<CalculateView>();
        calculator = new Calculator();
        viewModel = new CalculateViewModel(calculator);
        view.Init(viewModel);
    }
}
