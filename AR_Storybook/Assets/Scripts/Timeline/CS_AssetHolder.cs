using ATXK.EventSystem;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(ES_EventListener))]
public class CS_AssetHolder : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    [SerializeField] CS_CutsceneAsset cutscene;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.playableAsset = cutscene.CurrentTimeline;
    }

    public void NextTimeline()
    {
        director.playableAsset = cutscene.NextTimeline();
        director.Play();
    }
}
