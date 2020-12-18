#region Using

using NLib.Reflection;

#endregion

namespace DMT.Models
{
    public class SCWChangePassword
    {
        [PropertyMapName("staffId")]
        public string staffId { get; set; }

        [PropertyMapName("password")]
        public string password { get; set; }

        [PropertyMapName("newPassword")]
        public string newPassword { get; set; }

        [PropertyMapName("confirmNewPassword")]
        public string confirmNewPassword { get; set; }
    }

    public class SCWChangePasswordResult
    {
        public SCWStatus status { get; set; }
    }
}
