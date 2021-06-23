using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Model
{
    
    public class User : Entity
    {
        public string userId;
        public string email;
        public string password;
        public string name;
        public string lastName;
        public string phoneNumber;
        public string userType;


        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string UserType
        {
            get { return userType; }
            set { userType = value; }
        }


        public override string ToString()
        {
            return Name + " " + LastName;
        }
    }
}
