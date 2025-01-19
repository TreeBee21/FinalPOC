using Microsoft.MixedReality.QR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class QrTracker : MonoBehaviour
{
    public UnityEvent<string> QrSelected = new();

    [SerializeField] UnityEvent<string> WebCamDenied = new();
    [SerializeField] UnityEvent WebCamGranted = new();

    [SerializeField] QrMarker markerTemplate;
    public bool IsTracking { get; private set; } = false;
    public bool IsSupported { get; private set; } = false;
    public bool IsInitialized { get; private set; } = false;

    private List<QrMarker> markers = new();

    private const float QrCheckInterval = 0.5f;

    private float qrCheckTimer = 0;
    private float initializationDelay = 1;
    private float selectTimer = 0;
    private float selectCooldown = 1;
    private bool selectOnCooldown = false;

    private string currentData = string.Empty;
    private string bufferedData = string.Empty;

    private QRCodeWatcher qrTracker = null;
    private QRCodeWatcherAccessStatus accessStatus = QRCodeWatcherAccessStatus.UserPromptRequired;

    private void Start()
    {
        StartTracking();
    }

    private void OnEnable()
    {
        if (!IsInitialized) { return; }
        qrTracker.Start();
        IsTracking = true;
    }

    private void OnDisable()
    {
        if (IsTracking)
        { 
            IsTracking = false;
            qrTracker.Stop();
        }
    }

    private async void StartTracking()
    {
        IsSupported = QRCodeWatcher.IsSupported();
        if (!IsSupported)
        {
            WebCamDenied.Invoke("web cam access is not supported");
            return;
        }
        accessStatus = await QRCodeWatcher.RequestAccessAsync();
    }

    private void Update()
    {
        if (qrTracker == null && accessStatus == QRCodeWatcherAccessStatus.Allowed && (!IsInitialized))
        {
            
            Initialize();
        }
        if (selectOnCooldown)
        {
            selectTimer += Time.deltaTime;
            if (selectTimer >= selectCooldown)
            {
                ReEnableSelect();
            }
        }
        if (IsTracking && IsInitialized)
        {
            qrCheckTimer += Time.deltaTime;
            if (qrCheckTimer >= QrCheckInterval)
            {
                CheckForNewCodes();
            }
        }
    }

    private void Initialize()
    {
        qrTracker = new();
        qrTracker.Start();
        IsInitialized = true;
        WebCamGranted.Invoke();
        StartCoroutine(AttachMarkersWithDelay(initializationDelay));
    }

    private IEnumerator AttachMarkersWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        AttachMarkers();
        IsTracking = true;
        yield break;
    }

    private void CheckForNewCodes()
    {
        List<QRCode> newCodes = new();
        foreach (QRCode code in qrTracker.GetList())
        {
            bool isNew = true;
            foreach (QrMarker qrMarker in markers)
            {
                if (qrMarker.Code.Data == code.Data)
                {
                    isNew = false;
                    break;
                }
            }
            if (isNew) { newCodes.Add(code); }
        }
        if (newCodes.Count > 0)
        {
            
            AttachMarkers(newCodes);
        }
    }

    private void AttachMarkers() => AttachMarkers(qrTracker.GetList());

    private void AttachMarkers(IReadOnlyList<QRCode> codes)
    {
        try
        {
            
            foreach (QRCode code in codes)
            {
                
                AttachMarker(code);
            }
        }
        catch (Exception e) 
        {
            
        }
    }

    private void AttachMarker(QRCode code)
    {
        try 
        { 
            QrMarker newMarker = Instantiate(markerTemplate);
            newMarker.AttachMarker(code);
            markers.Add(newMarker);
            newMarker.Selected.AddListener(SelectQr);
            newMarker.gameObject.SetActive(true);
        } 
        catch (Exception e) 
        {
           
        }
    }

    public void SelectQr(string data) 
    {
        if (selectOnCooldown)
        { 
            bufferedData = data;
            return;
        }
        else if (data != currentData)
        {
            if (selectOnCooldown) { selectTimer = 0; }
            QrSelected.Invoke(data);
            bufferedData = data;
            currentData = data;
            selectOnCooldown = true;
        }
    }

    private void ReEnableSelect()
    {
        selectOnCooldown = false;
        selectTimer = 0;
        SelectQr(bufferedData);
    }

}
