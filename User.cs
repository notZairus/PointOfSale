using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomshit
{
    public class User
    {
        public int UID;
        public string Fname;
        public string Lname;
        public byte[] Image;

        public User(int uid, string fname, string lname, byte[] img)
        {
            UID = uid;
            Fname = fname;
            Lname = lname;
            Image = img;
        }
    }
}
