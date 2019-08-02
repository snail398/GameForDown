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
}
