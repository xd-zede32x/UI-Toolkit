using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private VisualElement _bottomContainer;
    private Button _openButton;
    private Button _closeButton;
    private VisualElement _bottomSheet;
    private VisualElement _scrim;
    private VisualElement _boy;
    private VisualElement _girl;

    private Label _message;


    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _bottomContainer = root.Q<VisualElement>("Charter-Botton");

        _openButton = root.Q<Button>("Open_Button");
        _closeButton = root.Q<Button>("Exit_Button");

        _bottomSheet = root.Q<VisualElement>("ButtonScreen");
        _scrim = root.Q<VisualElement>("Scrim");

        _boy = root.Q<VisualElement>("PlayerImages");
        _girl = root.Q<VisualElement>("ImageGirl");

        _message = root.Q<Label>("Message");

        _bottomContainer.style.display = DisplayStyle.None;

        _openButton.RegisterCallback<ClickEvent>(OnOpenButtonClicked);
        _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClicked);

        Invoke("AnimationBoy", 0.1f); 
        _bottomSheet.RegisterCallback<TransitionEndEvent>(OnBottomSheetDown);
    }

    private void OnOpenButtonClicked(ClickEvent click)
    {
        _bottomContainer.style.display = DisplayStyle.Flex;

        _bottomSheet.AddToClassList("buttons--up");
        _scrim.AddToClassList("sceenShift");

        AnimationGirl();
    }

    private void OnCloseButtonClicked(ClickEvent click)
    {
        _bottomSheet.RemoveFromClassList("buttons--up");
        _scrim.RemoveFromClassList("sceenShift");
    }

    private void AnimationBoy()
    {
        _boy.RemoveFromClassList("image--boy--inier");
    }

    private void AnimationGirl()
    {
        _girl.ToggleInClassList("image--girl--up");

        _girl.RegisterCallback<TransitionEndEvent>
        (
            evnt => _girl.ToggleInClassList("image--girl--up")
        );

        _message.text = string.Empty;
        string m = "\"C# Game Development – The Complete Beginner’s Guide.\"";
        DOTween.To(() => _message.text, x => _message.text = x, m, 3f).SetEase(Ease.Linear);
    }

    private void OnBottomSheetDown(TransitionEndEvent evnt)
    {
        if (!_bottomSheet.ClassListContains("buttons--up"))
        {
            _bottomContainer.style.display = DisplayStyle.None;
        }
    }
}