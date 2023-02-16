using System.Threading.Tasks;

namespace HSNP.Interface
{
    public interface IPushNotifications
    {
        bool IsRegistered { get; }

        void OpenSettings();

        Task<bool> RegisterForNotifications();
    }
}