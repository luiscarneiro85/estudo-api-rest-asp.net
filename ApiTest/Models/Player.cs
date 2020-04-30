using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiTest.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Psn { get; set; }

        public Player()
        {

        }

        public Player(string name, string email, string psn)
        {
            this.Name = name;
            this.Email = email;
            this.Psn = psn;
        }
    }
}