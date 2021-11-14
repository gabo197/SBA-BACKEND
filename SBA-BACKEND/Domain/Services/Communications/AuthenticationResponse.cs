﻿using SBA_BACKEND.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Domain.Services.Communications
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string Token { get; set; }
        public AuthenticationResponse(User user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            UserType = user.UserType;
            Token = token;
        }

        public AuthenticationResponse()
        {
        }
    }
}
