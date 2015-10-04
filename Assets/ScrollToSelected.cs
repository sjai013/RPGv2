using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*

Replace all code with this pseudo-code
Assuming scroll rect items have pivot on Top and expand downwards

float scrollPercentage = itempos / (containerHeight - panelHeight);
float normalizedPosition = 1 - scrollPercentage - (scrollPercentage / containerHeight);

    */

[RequireComponent(typeof(ScrollRect))]
public class ScrollToSelected : MonoBehaviour
{

    [SerializeField] private float scrollSpeed = 10f;
    [SerializeField] private float offset;

    ScrollRect m_ScrollRect;
    RectTransform m_RectTransform;
    RectTransform m_ContentRectTransform;
    RectTransform m_SelectedRectTransform;
    
    void Awake()
    {
        m_ScrollRect = GetComponent<ScrollRect>();
        m_RectTransform = GetComponent<RectTransform>();
        m_ContentRectTransform = m_ScrollRect.content;
    }

    void Update()
    {
        UpdateScrollToSelected();
    }

    void UpdateScrollToSelected()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        if (selected == null)
        {
            return;
        }
        if (selected.transform.parent != m_ContentRectTransform.transform)
        {
            return;
        }

        m_SelectedRectTransform = selected.GetComponent<RectTransform>();
        float curScrollPosition = m_ScrollRect.verticalNormalizedPosition;
        float curYSelectedPosition = m_SelectedRectTransform.anchoredPosition.y;
        float topViewY = m_ContentRectTransform.anchoredPosition.y;

        float scrollDifference = curYSelectedPosition + topViewY;
        float viewArea = m_RectTransform.rect.height - m_ContentRectTransform.rect.height;

        float newScrollVal = curScrollPosition;


        // Selected item is above the currently selected one - move scrollbar up
        if (scrollDifference > 0)
        {

            newScrollVal = Mathf.Abs((scrollDifference + offset) / viewArea) + curScrollPosition;         
        }

        // Selected item is below the currently selected one - move scrollbar down
        if (scrollDifference < viewArea)
        {
            newScrollVal = 1 - Mathf.Abs((scrollDifference - offset) / viewArea) + curScrollPosition;
        }

        newScrollVal = newScrollVal < 0 ? 0 : newScrollVal;
        newScrollVal = newScrollVal > 1 ? 1 : newScrollVal;
        m_ScrollRect.verticalNormalizedPosition = Mathf.Lerp(m_ScrollRect.verticalNormalizedPosition, newScrollVal, scrollSpeed * Time.unscaledDeltaTime);

    }

    void UpdateScrollToSelected2()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        if (selected == null)
        {
            return;
        }
        if (selected.transform.parent != m_ContentRectTransform.transform)
        {
            return;
        }

        m_SelectedRectTransform = selected.GetComponent<RectTransform>();
        var curScrollPosition = m_ScrollRect.verticalNormalizedPosition;
        var curViewport = new Rect(m_ContentRectTransform.anchoredPosition,m_RectTransform.rect.size);
        var selectedRect = new Rect(m_SelectedRectTransform.anchoredPosition,m_SelectedRectTransform.rect.size);
        var selectedYOffset = - (Mathf.Abs(curViewport.y) - Mathf.Abs(selectedRect.y));
        var maxOffset = Mathf.Abs(curViewport.y) + Mathf.Abs(curViewport.height) - Mathf.Abs(selectedYOffset);
        var scrollAmount = 0f;
        var scrollRatio = 1 / (m_ContentRectTransform.rect.height - curViewport.height);
        //Scroll upwards
        if (selectedYOffset < 0)
        {
            scrollAmount = -selectedYOffset;
        }
        else if (selectedYOffset > maxOffset)
        {
            scrollAmount = selectedYOffset - maxOffset;
        }

        var scrollPosition = scrollAmount*scrollRatio;

        //m_ScrollRect.verticalNormalizedPosition = Mathf.Lerp(m_ScrollRect.verticalNormalizedPosition, scrollAmount * scrollRatio - curScrollPosition, scrollSpeed * Time.unscaledDeltaTime);
        //Debug.Log(scrollAmount + " " + scrollPosition + " " + curScrollPosition);
        Debug.Log(selectedYOffset + " " + maxOffset);
        //Check whether selected button is within the current view

    }


    
}
