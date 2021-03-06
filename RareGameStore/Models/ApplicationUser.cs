﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RareGameStore.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
            this.GameCart = new GameCart();
        }

        public ApplicationUser(string username) : base(username)
        {
            this.GameCart = new GameCart();
        }

        public GameCart GameCart { get; set; }
        public int GameCartID { get; set; }
    }
}
