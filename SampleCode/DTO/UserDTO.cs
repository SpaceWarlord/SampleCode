using System;
using System.Collections.Generic;
using System.Text;

namespace SampleCode.DTO
{
    public class UserDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public UserDTO(int id, string username)
        {
            Id = id;
            Username = username;
        }
    }
}
