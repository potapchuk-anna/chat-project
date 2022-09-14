using ChatProject.Models;
using System.Security.Claims;

namespace ChatProject.Services
{
    public class CurrentLoginUser: ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentLoginUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private long _userId;
        public long UserId
        {
            get { return _userId; }
            private set
            {
                _userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
        }
    }
}
