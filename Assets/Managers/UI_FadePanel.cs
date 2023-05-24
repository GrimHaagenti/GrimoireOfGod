using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_FadePanel : MonoBehaviour
{
    [SerializeField] Image panel;

    ///EVENTS
    public UnityEvent OnFadeInBegin;
    public UnityEvent OnFadeOutBegin;
    public UnityEvent OnFadeInComplete;
    public UnityEvent OnFadeOutComplete;

    // Start is called before the first frame update

    public void BeginFadeOut()
    {
        OnFadeOutBegin?.Invoke();
        StartCoroutine(PanelFadeOut());
    }
    
    public void BeginFadeIn() 
    {
        OnFadeInBegin?.Invoke();
        StartCoroutine(PanelFadeIn());
    }

    void FadeOutComplete()
    {
        OnFadeOutComplete?.Invoke();
        StopCoroutine(PanelFadeOut());

    }
    void FadeInComplete()
    {
        OnFadeInComplete?.Invoke();
        StopCoroutine(PanelFadeIn());
    }

    IEnumerator PanelFadeIn()
    {
        float alpha = 0;
        float target = 1;

        while (alpha < target)
        {
            alpha += 0.02f;
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alpha);
            yield return null;
        }
        FadeInComplete();
        yield return null;
    }
    IEnumerator PanelFadeOut()
    {
        float alpha = 1;
        float target = 0;

        while (alpha > target)
        {
            alpha -= 0.02f;
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alpha);
            yield return null;
        }
        FadeOutComplete();
        yield return null;
    }

}
