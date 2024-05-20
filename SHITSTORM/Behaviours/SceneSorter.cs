using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
//using EditorCoroutines.Editor;
#endif

public class SceneSorter : MonoBehaviour
{
//#if UNITY_EDITOR
//    [SerializeField] Transform p_T;
//    [SerializeField] string p_name;
//    [SerializeField] uint iters;
//    [SerializeField] bool process;

//    //----------------------------------------------------------------------------------------------------------

//    [ContextMenu(nameof(OnValidate))]
//    private void OnValidate()
//    {
//        if (process)
//            StartCoroutine(ESort(), gameObject);

//        IEnumerator ESort()
//        {
//            p_T = new GameObject(p_name).transform;
//            p_T.SetParent(transform, false);
//            p_T.SetAsFirstSibling();

//            float time = Time.realtimeSinceStartup;
//            iters = 0;

//            for (int T_i = 1; T_i < transform.childCount;)
//            {
//                iters++;

//                Transform T = transform.GetChild(T_i);

//                if (T.name.StartsWith(p_name))
//                    T.SetParent(p_T, true);
//                else
//                    T_i++;

//                p_T.SortChildren();

//                float rTime = Time.realtimeSinceStartup;

//                if (rTime - time > .1f)
//                {
//                    time += .1f;
//                    yield return null;
//                }
//            }

//            process = false;
//        }
//    }
//#endif

    //----------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        Destroy(this);
    }
}