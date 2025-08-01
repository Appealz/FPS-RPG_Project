using UnityEngine;
using UnityEngine.InputSystem;

public class TestUIManager : DestroySingleton<TestUIManager>
{
    private Transform shopUICanvas;
    public Transform ShopUICanvas
    {
        get
        {
            if(shopUICanvas == null)
            {
                shopUICanvas = GameObject.Find("ShopCanvas").transform;
            }
            return shopUICanvas;
        }
    }
    private IShopUI shopUI;
    private ShopUIPresenter shopUIPresenter;

    public void InitTestUI()
    {
        if (!TryGetComponent<IShopUI>(out shopUI))
            Debug.Log("TestUIManager.cs - InitTestUI() - IShopUI Can't Find");
        else
        {
            shopUI.Init(ShopUICanvas);
            shopUIPresenter = new ShopUIPresenter(shopUI);
        }

        GameManager.OnGameUpdate += TestUpdate;
    }

    private void TestUpdate()
    {
        if(Keyboard.current.vKey.IsPressed())
        {
            EventBus_ShopIsOn.Publish(new ShopIsOnEvent(true));
            ShopManager.Instance.ShopUpdate();
        }
    }

    private void OnDisable()
    {
        shopUIPresenter.DisableUI();
        GameManager.OnGameUpdate -= TestUpdate;
    }
}
