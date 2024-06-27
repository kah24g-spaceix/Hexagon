using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class TechTreeController : MonoBehaviour
{
    private IView<TechModel> m_view;
    private Coroutine m_routine;

    private IEnumerator DialogRoutine(DialogLine[] pData, Int32 pStartIndex = 0)
    {
        m_view.Show();

        for (Int32 i = pStartIndex; i < pData.Length; i++)
        {
            DialogLine line = pData[i];
            Sprite sprite = LoadSprite($"Sprites/{line.Sprite}");


            if (line.Options != null)
            {
                List<String> selects = new List<string>();
                foreach (var item in line.Options)
                {
                    selects.Add(item[0].Text);
                }
                m_view.Bind(new DialogModel(line.Text, selects.ToArray(), OnSelect));

                yield return new WaitUntil(() => m_doSkip);

                yield return StartCoroutine(DialogRoutine(line.Options[m_select], 1));

                yield break;
            }

            m_view.Bind(new DialogModel(line.Text, line.Talker, sprite, OnNext));
            yield return new WaitUntil(() => m_doSkip);
            m_doSkip = false;
        }

        m_view.Hide();
    }
}
