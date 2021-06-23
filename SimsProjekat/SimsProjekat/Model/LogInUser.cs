using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Model
{
    public class LogInUser : Entity
    {
        private string loginUsername;
        private string loginPassword;

        public string LoginUsername
        {
            get { return loginUsername; }
            set { loginUsername = value; }
        }
        
        public string LoginPassword
        {
            get { return loginPassword; }
            set { loginPassword = value; }
        }
    }
}
