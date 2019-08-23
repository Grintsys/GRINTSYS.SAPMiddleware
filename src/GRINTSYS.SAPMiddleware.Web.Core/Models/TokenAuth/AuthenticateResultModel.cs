﻿namespace GRINTSYS.SAPMiddleware.Models.TokenAuth
{
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public long UserId { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public int SalesPersonId { get; set; }

        public int CollectId { get; set; }
    }
}
