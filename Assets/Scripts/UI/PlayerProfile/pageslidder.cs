using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class pageslidder : MonoBehaviour
{
    private int curPage = 0;

    private int numOfpages = 2;

    public List<RectTransform> pagesInd = new List<RectTransform>();

    public RectTransform pageHolder;

    private List<RectTransform> pages = new List<RectTransform>();

    private void Awake()
    {
        foreach (RectTransform page in pageHolder)
        {
            pages.Add(page);
        }

        numOfpages = pages.Count;
    }

    public void moveToPageOnLeft()
    {
        if (curPage == 0)
        {
            showNewpage(numOfpages-1);
            changePageInd(numOfpages - 1);
            curPage = numOfpages-1;
        }
        else
        {
            showNewpage(curPage-1);
            changePageInd(curPage - 1);
            curPage = curPage - 1;
        }
    }

    public void moveToPageOnRight()
    {
        if (curPage == (numOfpages-1))
        {
            showNewpage(0);
            changePageInd(0);
            curPage = 0;
        }
        else
        {
            showNewpage(curPage + 1);
            changePageInd(curPage + 1);
            curPage = curPage + 1;
        }
    }

    private void showNewpage(int newPage)
    {
        pages[curPage].gameObject.SetActive(false);
        pages[newPage].gameObject.SetActive(true);
    }

    private void changePageInd(int newPage)
    {
        pagesInd[curPage].gameObject.SetActive(false);
        pagesInd[newPage].gameObject.SetActive(true);
    }
}
