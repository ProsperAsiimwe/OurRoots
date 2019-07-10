﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OurRoots.Domain.Entities;

namespace OurRoots.WebUI.Models.Users
{
    public class UserModel
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}