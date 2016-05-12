namespace PFNetworkObjects
{
    /// <summary>
    /// Enum for type of storage to use for email tasks. (e.g. resend queue)
    /// </summary>
    public enum enEmailTasksStorageType
    {
#pragma warning disable 1591
        Unknown = 0,
        XMLFiles = 1,
        Database = 2    //Database storage type not implemented in this copy of PFNetworkObjects
#pragma warning restore 1591
    }

    /// <summary>
    /// Enum of email send results.
    /// </summary>
    public enum enEmailSendResult
    {
#pragma warning disable 1591
        Unknown = 0,
        Success = 1,
        Failed = 2
#pragma warning restore 1591
    }

    /// <summary>
    /// Enum of email failure reasons.
    /// </summary>
    public enum enEmailFailedReason
    {
#pragma warning disable 1591
        NoError = 0,
        SmtpException = 1,
        SmtpRecipientsException = 2,
        InvalidOperationsException = 3,
        ArgumentNullException = 4,
        GeneralError = 5,
        Unknown = 99
#pragma warning restore 1591
    }

    /// <summary>
    /// Struct that encapsulates information from an eamil send operation.
    /// </summary>
    public struct stEmailSendResult
    {
#pragma warning disable 1591
        public enEmailSendResult emailSendResult;
        public enEmailFailedReason emailFailedReason;
        public string failureMessages;

        public stEmailSendResult(enEmailSendResult sendResult, enEmailFailedReason failedReason)
        {
            this.emailSendResult = sendResult;
            this.emailFailedReason = failedReason;
            this.failureMessages = string.Empty;
        }
#pragma warning restore 1591
    }

    /// <summary>
    /// Struct that encapsulates information for an email archive entry.
    /// </summary>
    public struct stEmailArchiveEntry
    {
#pragma warning disable 1591
        public System.Guid id;
        public int numRetries;
        public System.DateTime firstSendAttempt;
        public System.DateTime lastSendAttempt;
        public stEmailSendResult sendResult;
        public PFEmailMessage emailMessage;
        /// <summary>
        /// Construct for struct.
        /// </summary>
        /// <param name="result">Email result information.</param>
        /// <param name="email">Email message object.</param>
        public stEmailArchiveEntry(stEmailSendResult result, PFEmailMessage email)
        {
            this.id = System.Guid.NewGuid();
            this.numRetries = 0;
            this.firstSendAttempt = System.DateTime.MinValue;
            this.lastSendAttempt = System.DateTime.MinValue;
            this.sendResult = result;
            this.emailMessage = email;
        }
#pragma warning restore 1591
    }

}//end namespace
