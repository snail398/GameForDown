using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System;

public class AnaliticsCore
{
    private DependencyStatus _dependencyStatus = DependencyStatus.UnavailableOther;
    protected bool _firebaseInitialized = false;

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
    }

    public void SendStoryTutorialPassed()
    {
        if (_firebaseInitialized)
            FirebaseAnalytics.LogEvent("story_tutorial_passed");
    }

    public void SendCurrentPassage(string passage)
    {
        if (_firebaseInitialized)
            FirebaseAnalytics.LogEvent(passage);
    }

    public void SendRunTime(TimeSpan runTimeSpan)
    {
        if (_firebaseInitialized)
            FirebaseAnalytics.LogEvent("run_time", "seconds", runTimeSpan.TotalSeconds);
    }

    public void SendCurrentMoneyCount(int count)
    {
        if (_firebaseInitialized)
            FirebaseAnalytics.LogEvent("current_money_count", "cur_count",count);
    }

    public void SendCommonMoneyCount(int count)
    {
        if (_firebaseInitialized)
            FirebaseAnalytics.LogEvent("common_money_count", "com_count", count);
    }
}
