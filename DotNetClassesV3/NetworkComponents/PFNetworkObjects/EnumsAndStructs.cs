namespace PFNetworkObjects
{
#pragma warning disable 1591

    public enum enEmailTasksStorageType
    {
        Unknown = 0,
        XMLFiles = 1,
        Database = 2    //Database storage type not implemented in this copy of PFNetworkObjects
    }

    public enum enEmailSendResult
    {
        Unknown = 0,
        Success = 1,
        Failed = 2
    }

    public enum enEmailFailedReason
    {
        NoError = 0,
        SmtpException = 1,
        SmtpRecipientsException = 2,
        InvalidOperationsException = 3,
        ArgumentNullException = 4,
        GeneralError = 5,
        Unknown = 99
    }

    public struct stEmailSendResult
    {
        public enEmailSendResult emailSendResult;
        public enEmailFailedReason emailFailedReason;
        public string failureMessages;

        public stEmailSendResult(enEmailSendResult sendResult, enEmailFailedReason failedReason)
        {
            this.emailSendResult = sendResult;
            this.emailFailedReason = failedReason;
            this.failureMessages = string.Empty;
        }
    }

    public struct stEmailArchiveEntry
    {
        public System.Guid id;
        public int numRetries;
        public System.DateTime firstSendAttempt;
        public System.DateTime lastSendAttempt;
        public stEmailSendResult sendResult;
        public PFEmailMessage emailMessage;

        public stEmailArchiveEntry(stEmailSendResult result, PFEmailMessage email)
        {
            this.id = System.Guid.NewGuid();
            this.numRetries = 0;
            this.firstSendAttempt = System.DateTime.MinValue;
            this.lastSendAttempt = System.DateTime.MinValue;
            this.sendResult = result;
            this.emailMessage = email;
        }
    }

#pragma warning restore 1591
}//end namespace
