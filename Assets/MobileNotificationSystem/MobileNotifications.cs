using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class MobileNotifications : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Make sure that there are no duplicate messages.
        AndroidNotificationCenter.CancelAllDisplayedNotifications();


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
        notification.Title = "Husk at drikke vand!";
        notification.Text = "Drik vand løbende.";
        notification.FireTime = System.DateTime.Now.AddSeconds(10);
        //^ can be changed to something like this: notification.FireTime = System.DateTime.Now.AddHours(24);
       
        //Change up the icons in the notification android message system
        notification.SmallIcon ="icon_small";
        notification.LargeIcon ="icon_large";



        //Send the notification
        var id =  AndroidNotificationCenter.SendNotification(notification, "channel_id");

        //If the notification script has already been requested, cancel it and reschedule another message.
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
