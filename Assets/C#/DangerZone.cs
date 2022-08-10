using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sequence = DG.Tweening.Sequence;

public class DangerZone : MonoBehaviour
{
    public GameObject player;

    public DangerManager dangerManager;

    public float censorDistance;

    public Image dangerSign;

    public float dangerTime;
    public float dangerCoolTime;
    
    private float elapsedTime;
    private bool _isDanger;
    private bool isCalledOnce;
    
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        isCalledOnce = false;
        dangerSign.enabled = false;
        dangerSign.DOFade(1.0f, 0.25f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        SwitchDangerMode();
    }

    void SwitchDangerMode()
    {
        if (_isDanger && elapsedTime >= dangerCoolTime)
        {
            _isDanger = false;
            dangerManager.isDanger = false;
            isCalledOnce = false;
        }
        
        if (Vector3.Distance(transform.position, player.transform.position) <= censorDistance &&
            !isCalledOnce && elapsedTime >= dangerTime)
        {
            isCalledOnce = true;
            _isDanger = true;
            Invoke(nameof(EnableDangerMode), 2.0f);

            dangerSign.enabled = true;
            Invoke(nameof(DisableDangerSign), 2.0f);
            
            Invoke(nameof(DisableDangerMode), dangerTime);
        }
    }

    void EnableDangerMode()
    {
        dangerManager.isDanger = true;
    }
    void DisableDangerMode()
    {
        elapsedTime = 0;
        dangerManager.isDanger = false;
    }

    void DisableDangerSign()
    {
        dangerSign.enabled = false;
    }
}
