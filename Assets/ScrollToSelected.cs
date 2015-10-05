using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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
    
}
