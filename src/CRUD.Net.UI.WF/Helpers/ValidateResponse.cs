using CRUD.Net.Domain.Notifications;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRUD.Net.UI.WF.Helpers
{
    public class ValidateResponse
    {
        private readonly INotifier _notifier;
        public ValidateResponse()
        {
            _notifier = (INotifier)Program.ServiceProvider.GetService(typeof(INotifier));
        }

        public bool CustomResponse(string msgSuccess = "")
        {
            if (!IsOperationValid())
            {
                StringBuilder message = _notifier.GetNotifications().Aggregate(
                            new StringBuilder(),
                            (sb, s) => sb.AppendLine(s.Message)
                        );
                MessageBox.Show(message.ToString(), "Erro ao salvar.");
                _notifier.ClearNotifications();
                return false;
            }
            if (msgSuccess != "")
            {
                MessageBox.Show(msgSuccess);
            }            
            return true;
        }

        private bool IsOperationValid()
        {

            return !_notifier.HasNotifications();
        }
    }
}
