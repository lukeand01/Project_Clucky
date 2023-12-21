using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class ButtonEvent : ButtonBase
{
    public UnityEvent unityEvent;
    public bool HasShortDelay;
    GameObject holder;
    Vector3 holderOriginalPos;
    bool inProcess = false;
    private void Awake()
    {
        holder = transform.GetChild(0).gameObject;
        holderOriginalPos = holder.transform.localPosition;  
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        


        if (HasShortDelay)
        {
            if (!inProcess)
            {
                holder.transform.position += new Vector3(0, -5, 0);
                Invoke(nameof(ReturnHolder), 0.1f);
                Invoke(nameof(CallEvent), 0.2f);
                inProcess = true;
            }
           
        }
        else
        {
            unityEvent?.Invoke();
        }

        
    }

    void CallEvent()
    {
        unityEvent?.Invoke();  
        inProcess = false;  
    }

    void ReturnHolder()
    {
        holder.transform.localPosition = holderOriginalPos;
    }
       
    IEnumerator DownProcess()
    {
        yield return null;
    }
    
    IEnumerator UpProcess() 
    {
        yield return null;
    }

   
}
