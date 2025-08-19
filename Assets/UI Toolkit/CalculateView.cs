using UnityEngine;
using UnityEngine.UIElements;

public class CalculateView : MonoBehaviour
{
    private CalculateViewModel viewModel;

    private FloatField amount1;
    private FloatField amount2;
    private Label result;
    private Button plusBtn;
    private Button minusBtn;
    private Button multiplyBtn;
    private Button divideBtn;

    UIDocument doc;
    VisualElement root;

    public void Init(CalculateViewModel newViewModel)
    {
        viewModel = newViewModel;
    }

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        amount1 = root.Q<FloatField>("Amount1");
        amount2 = root.Q<FloatField>("Amount2");
        result = root.Q<Label>("Result");
        plusBtn = root.Q<Button>("Plus");
        minusBtn = root.Q<Button>("Minus");
        multiplyBtn = root.Q<Button>("Multiply");
        divideBtn = root.Q<Button>("Divide");

        Bind();
    }

    private void Bind()
    {
        if (amount1 == null || amount2 == null || result == null || plusBtn == null || minusBtn == null || multiplyBtn == null || divideBtn == null)
        {
            Debug.Log("Bind Fail");
            return;
        }

        amount1.RegisterValueChangedCallback(evt => { viewModel.amount_1 = evt.newValue; });
        amount2.RegisterValueChangedCallback(evt => { viewModel.amount_2 = evt.newValue; });

        plusBtn.clicked += viewModel.DoPlus;
        minusBtn.clicked += viewModel.DoMinus;
        multiplyBtn.clicked += viewModel.DoMultiply;
        divideBtn.clicked += viewModel.DoDivide;

        viewModel.OnResultChanged += OnViewModelResultChange;

        OnViewModelResultChange(viewModel.result);
    }

    private void OnViewModelResultChange(float newResult)
    {
        if (this.result == null)
            return;

        if(float.IsNaN(newResult))
        {
            result.text = "Divide by 0";
        }
        else
        {
            result.text = newResult.ToString();
        }
    }
}
