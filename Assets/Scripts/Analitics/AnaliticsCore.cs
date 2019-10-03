using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class AnaliticsCore
{
    private DependencyStatus _dependencyStatus = DependencyStatus.UnavailableOther;
    protected bool _firebaseInitialized = false;
    private Amplitude _amplitude;

    public AnaliticsCore()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            _dependencyStatus = task.Result;
            if (_dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                /*
                * Debug.LogError("Could not resolve all Firebase dependencies: " + _dependencyStatus);
                */        
            }
        });

        _amplitude = Amplitude.getInstance();
        _amplitude.trackSessionEvents(true);
        _amplitude.init("94a37eb01d2b2e09b2e836dcd7b44ad0");
    }


    void InitializeFirebase()
    {
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

        // Set the user's sign up method.
        FirebaseAnalytics.SetUserProperty(
          FirebaseAnalytics.UserPropertySignUpMethod,
          "Google");
        // Set the user ID.
        FirebaseAnalytics.SetUserId("uber_user_510");
        // Set default session duration values.
        FirebaseAnalytics.SetMinimumSessionDuration(new TimeSpan(0, 0, 10));
        FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
        _firebaseInitialized = true;
    }

    public void SendRunTutorialPassed()
    {
        if (_firebaseInitialized)
            FirebaseAnalytics.LogEvent("run_tutorial_passed");
        Analytics.CustomEvent("run_tutorial_passed");
        _amplitude.logEvent("run_tutorial_passed");
    }

    public void SendTryRollback()
    {
        if (_firebaseInitialized)
            FirebaseAnalytics.LogEvent("try_rollback");
        Analytics.CustomEvent("try_rollback");
        _amplitude.logEvent("try_rollback");
    }

    public void SendRollback()
    {
        if (_firebaseInitialized)
            FirebaseAnalytics.LogEvent("rollback");
        Analytics.CustomEvent("rollback");
        _amplitude.logEvent("rollback");
    }

    public void SendStoryTutorialPassed()
    {
        if (_firebaseInitialized)
            FirebaseAnalytics.LogEvent("story_tutorial_passed");
        Analytics.CustomEvent("story_tutorial_passed");
        _amplitude.logEvent("story_tutorial_passed");
    }

    public void SendCurrentPassage(string passage)
    {
        string prefix = "passage_";
        if (_firebaseInitialized)
            FirebaseAnalytics.LogEvent(prefix + passage);
        Analytics.CustomEvent(prefix + passage);
        _amplitude.logEvent(prefix + passage);
    }

    public void SendRunTime(TimeSpan runTimeSpan)
    {
        string prefix = "run_time_sec_";
        string timeEventStr;
        int sec = (int) Math.Round(runTimeSpan.TotalSeconds);
        if (sec <= 120)
            timeEventStr = prefix + sec.ToString();
        else if (sec > 120 && sec <= 150)
            timeEventStr = prefix + "135";
        else if (sec > 150 && sec <= 180)
            timeEventStr = prefix + "165";
        else if (sec > 180 && sec <= 210)
            timeEventStr = prefix + "195";
        else if (sec > 210 && sec <= 240)
            timeEventStr = prefix + "225";
        else if (sec > 240 && sec <= 270)
            timeEventStr = prefix + "265";
        else if (sec > 270 && sec <= 300)
            timeEventStr = prefix + "285";
        else
            timeEventStr = prefix + "310";
        if (_firebaseInitialized)
            FirebaseAnalytics.LogEvent(timeEventStr);
        Analytics.CustomEvent(timeEventStr);
        _amplitude.logEvent(timeEventStr);
    }

    public void SendCurrentMoneyCount(int count)    
    {
        string prefix = "current_money_count_";
        string curMoneyEventStr;
        int mod = count % 10;
        int x = count - mod + (mod < 5 ? 0 : 10);
        curMoneyEventStr = prefix + x.ToString();
        if (_firebaseInitialized)
            FirebaseAnalytics.LogEvent(curMoneyEventStr);
        Analytics.CustomEvent(curMoneyEventStr);
        _amplitude.logEvent(curMoneyEventStr);
    }

    public void SendCommonMoneyCount(int count)
    {
        string prefix = "common_money_count_";
        string comMoneyEventStr;
        int mod = count % 10;
        int x = count - mod + (mod < 5 ? 0 : 10);
        comMoneyEventStr = prefix + x.ToString();
        if (_firebaseInitialized)
            FirebaseAnalytics.LogEvent(comMoneyEventStr);
        Analytics.CustomEvent(comMoneyEventStr);
        _amplitude.logEvent(comMoneyEventStr);
    }
}
