using System.Collections.Generic;

namespace CRUD.Net.Domain.Notifications
{
    public interface INotifier
    {

        bool HasNotifications();

        IList<Notification> GetNotifications();

        void Handle(Notification notification);
        void ClearNotifications();

    }
}
