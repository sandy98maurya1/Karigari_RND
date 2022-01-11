using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ILabour
    {
        public List<Labour> GetAllLabours();
        public bool CreateLabour(Labour model);
        public Labour GetLabourByContact(string Contact);
        public bool DeleteLabour( int userId);
        public bool UpdateLabour(Labour model);
        public Labour GetLabourById(int Id);
        //public List<Labour> GetLaboursByContact(string Contact);
    }
}
