namespace HakunaMatata.Models.ViewModels
{
    public class VM_Login
    {
        public string LoginName { get; set; }

        public string Password { get; set; }

        public bool IsRememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}

