using System;
using System.Collections.Generic;
using System.Text;

namespace fuel_consumption_tracker.Shared.Authentication
{
    public class UserInfo
    {
        public bool IsAuthenticated { get; set; }
        public string? UserName { get; set; }
        public Dictionary<string, string> ExposedClaims { get; set; } = new Dictionary<string, string>();
    }
}
