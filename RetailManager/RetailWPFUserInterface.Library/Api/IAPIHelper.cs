﻿using RetailWPFUserInterface.Library.Model;
using System.Threading.Tasks;

namespace RetailWPFUserInterface.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}