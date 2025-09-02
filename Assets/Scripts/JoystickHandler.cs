using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class JoysticHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEngine.UI.Image _joystickBackground;
    [SerializeField] private UnityEngine.UI.Image _joystick;
    [SerializeField] private UnityEngine.UI.Image _joystickArena;
    private Vector2 _joystickBackgroundStartPosition;
    protected Vector2 _inputVector;
    void Start()
    {
        _joystickBackgroundStartPosition = _joystickBackground.rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joysticPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform,
        eventData.position, null, out joysticPosition))
        {
            joysticPosition.x = (joysticPosition.x * 2 / _joystickBackground.rectTransform.sizeDelta.x);
            joysticPosition.y = (joysticPosition.y * 2 / _joystickBackground.rectTransform.sizeDelta.y);
            _inputVector = new Vector2(joysticPosition.x, joysticPosition.y);
            _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector;
            _joystick.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackground.rectTransform.sizeDelta.x / 2),
            _inputVector.y * _joystickBackground.rectTransform.sizeDelta.y / 2);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 joystickBackgrounPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArena.rectTransform, eventData.position,
        null, out joystickBackgrounPosition))
        {
            _joystickBackground.rectTransform.anchoredPosition = new Vector2(joystickBackgrounPosition.x, joystickBackgrounPosition.y);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickBackground.rectTransform.anchoredPosition = _joystickBackgroundStartPosition;
        _inputVector = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
    }
}
