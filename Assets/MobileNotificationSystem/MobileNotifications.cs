using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class MobileNotifications : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //The android notification channel to send messages with.
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notifications Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);


        //The notification that is going to be sent.
        var notification = new AndroidNotification();
        notification.Title = "Your pet is dying!";
        notification.Text = "Your pet has been neglected for too long and is on the verge od dying.";
        notification.FireTime = System.DateTime.Now.AddSeconds(10);


        //Send the notification
        AndroidNotificationCenter.SendNotification(notification, "channel_id");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
