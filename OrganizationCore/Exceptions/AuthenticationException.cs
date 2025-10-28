namespace OrganizationCore.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message) { } 
    }

    public class EmailNotConfirmedException : Exception
    {
        public EmailNotConfirmedException(string message) : base(message) { }
    }

    public class  AccountLockedException : Exception
    {
        public AccountLockedException(string message) : base(message) { }
    }

    public class InvalidOperationException : Exception
    {
        public InvalidOperationException(string message) : base(message) { }
    }
}
