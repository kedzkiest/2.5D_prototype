using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class EraserBehaviour : MonoBehaviour
{
    [Header("Eraser behaviour flag:\n" +
            "0 to keep rotating in CCW\n" +
            "1 to keep rotating in CW")]
    public int mode;

    // Start is called before the first frame update
    void Start()
    {
        MakeMove();
    }

    void MakeMove()
    {
        Sequence sequence = DOTween.Sequence();

        if (mode == 0)
        {
            sequence.Append(transform.DOLocalRotate(Vector3.forward * 90, 1f)
                .SetRelative(true)
            );
        }

        if (mode == 1)
        {
            sequence.Append(transform.DOLocalRotate(Vector3.forward * -90, 1f)
                .SetRelative(true)
            );
        }

        sequence.AppendInterval(3f);

        sequence.SetLoops(-1, LoopType.Incremental);

        sequence.Play();
    }
}
