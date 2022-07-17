using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject[] pages;
    private int currentPage;

    private void OnEnable()
    {
        currentPage = 0;
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        pages[currentPage].SetActive(true);
    }

    public void NextPage()
    {
        
        if (currentPage >= pages.Length - 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            pages[currentPage].SetActive(false);
            currentPage++;
            pages[currentPage].SetActive(true);
        }
    }
}
