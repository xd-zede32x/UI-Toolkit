using UnityEngine;
using UnityEngine.UIElements;

public class CustomSlider : MonoBehaviour
{
    private VisualElement m_Root;
    private VisualElement m_Slider;
    private VisualElement m_Dragger;
    private VisualElement m_Bar;
    private VisualElement m_NewDragger;
    private VisualElement m_Bubble;

    [SerializeField] private Color _colorA;
    [SerializeField] private Color _colorB;

    private void Start()
    {
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_Slider = m_Root.Q<Slider>("MySlider");
        m_Dragger = m_Root.Q<VisualElement>("unity-dragger");

        AddElements();

        m_Slider.RegisterCallback<ChangeEvent<float>>(SliderValueChanged);

        m_Slider.RegisterCallback<GeometryChangedEvent>(SliderInit);
    }

    private void AddElements()
    {
        m_Bar = new VisualElement();
        m_Dragger.Add(m_Bar);
        m_Bar.name = "Bar";
        m_Bar.AddToClassList("bar");

        m_NewDragger = new VisualElement();
        m_Slider.Add(m_NewDragger);
        m_NewDragger.name = "NewDragger";
        m_NewDragger.AddToClassList("newDragger");
        m_NewDragger.pickingMode = PickingMode.Ignore;

        m_Bubble = new VisualElement();
        m_Slider.Add(m_Bubble);
        m_Bubble.name = "Bubble";
        m_Bubble.AddToClassList("bubble");
        m_Bubble.pickingMode = PickingMode.Ignore;
    }

    private void SliderValueChanged(ChangeEvent<float> value)
    {
        Vector2 offset = new Vector2((m_NewDragger.layout.width - m_Dragger.layout.width) / 2 - 5f, (m_NewDragger.layout.height - m_Dragger.layout.height) / 2 - 5f);
        Vector2 offset_Bubble = new Vector2((m_Bubble.layout.width - m_Dragger.layout.width) / 2 - 4f, (m_Bubble.layout.height - m_Dragger.layout.height) / 2 + 120f);
        Vector2 position = m_Dragger.parent.LocalToWorld(m_Dragger.transform.position);
        position = m_NewDragger.parent.WorldToLocal(position);

        m_NewDragger.transform.position = position-offset;
        m_Bubble.transform.position = position-offset_Bubble;

        float v = Mathf.Round(value.newValue);
        m_Bar.style.backgroundColor = Color.Lerp(_colorA, _colorB, v / 100f);
        m_Bubble.style.unityBackgroundImageTintColor = Color.Lerp(_colorA, _colorB, v / 100f);
    }

    private void SliderInit(GeometryChangedEvent evt)
    {
        Vector2 offset = new Vector2((m_NewDragger.layout.width - m_Dragger.layout.width) / 2 - 5f, (m_NewDragger.layout.height - m_Dragger.layout.height) / 2 - 5f);
        Vector2 offset_Bubble = new Vector2((m_Bubble.layout.width - m_Dragger.layout.width) / 2 - 4f, (m_Bubble.layout.height - m_Dragger.layout.height) / 2 + 120f);
        Vector2 position = m_Dragger.parent.LocalToWorld(m_Dragger.transform.position);
        position = m_NewDragger.parent.WorldToLocal(position);

        m_NewDragger.transform.position = position - offset;
        m_Bubble.transform.position = position - offset_Bubble;

        m_Bar.style.backgroundColor = _colorA;
        m_Bubble.style.unityBackgroundImageTintColor = _colorA;
    }

    private void Update()
    {
        
    }
}