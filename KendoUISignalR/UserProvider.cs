using Microsoft.AspNet.SignalR;

namespace KendoUISignalR
{
    public class UserProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            var user = "1";

            user = "2";

            return user;
        }
    }
}