using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] AudioClip _hoverSound;
    [SerializeField] AudioClip _clickSound;
    [SerializeField] float _hoverScaleIncrease = 1.1f;
    [SerializeField] float _clickScaleIncrease = 1.3f;
    [SerializeField] float _tweenEffectDuration = 0.1f;

    public void OnPointerDown(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector2.one * _clickScaleIncrease;
        LeanTween.scale(gameObject, Vector2.one, _tweenEffectDuration).setIgnoreTimeScale(true);
        AudioManager.Instance.PlayAudio(_clickSound, AudioManager.SoundType.SFX, 1f, false);
    }

    public void OnPointerEnter(PointerEventData eventDate)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector2.one * _hoverScaleIncrease, _tweenEffectDuration).setIgnoreTimeScale(true);
        AudioManager.Instance.PlayAudio(_hoverSound, AudioManager.SoundType.SFX, 1f, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector2.one, _tweenEffectDuration).setIgnoreTimeScale(true);
    }
}
