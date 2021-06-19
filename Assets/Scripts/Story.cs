using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Story : MonoBehaviour
{
    public TextMeshProUGUI text;
    private List<string> storyProgress = new List<string>();
    public string[] story;
    public int currentPage = 0;
    private int maxPage = 0;

    private void Start()
    {
        storyProgress.Add("");
        storyProgress[maxPage] = story[0] + "\n\n";
        text.text = storyProgress[0];
    }
    public void UpdateStory(int i)
    {
        UpdateText();
        storyProgress[maxPage] += story[i] + "\n\n";               
    }

    private void UpdateText()
    {
        CheckLength();
        text.text = storyProgress[currentPage];
    }

    private void ShowText()
    {
        text.text = storyProgress[currentPage];
    }
    private void CheckLength()
    {
        if(storyProgress[maxPage].Length >= 500)
        {
            storyProgress.Add("");
            maxPage++;
        }
    }

    public void NextPage()
    {
        if(currentPage >= maxPage)
        {
            currentPage = maxPage;
        }
        else
        {
            currentPage++;
        }

        ShowText();
    }
    public void PreviousPage()
    {
        if (currentPage <= 0)
        {
            currentPage = 0;
        }
        else
        {
            currentPage--;
        }

        ShowText();
    }

}
