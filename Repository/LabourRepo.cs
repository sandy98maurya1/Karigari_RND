using Contracts;
using Contracts.DataContracts;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class LabourRepo : ILabour
    {
        private readonly ILabourData _Labour;

        public LabourRepo(ILabourData Labour)
        {
            _Labour = Labour;
        }

        public bool CreateLabour(Labour model)
        {
            return _Labour.CreateLabour(model);
        }

        public bool DeleteLabour(int userId)
        {
            return _Labour.DeleteLabour(userId);
        }

        public List<Labour> GetAllLabours()
        {
            return _Labour.GetAllLabours();
        }

        public Labour GetLabourByContact(string Contact)
        {
            return _Labour.GetLabourByContact(Contact);
        }

        public Labour GetLabourById(int Id)
        {
            return _Labour.GetLabourById(Id);
        }

        public List<Labour> GetLaboursByContact(string Contact)
        {
            return _Labour.GetLaboursByContact(Contact);
        }

        public bool UpdateLabour(Labour model)
        {
            return _Labour.UpdateLabour(model);
        }
    }

    public static class ExtensionMethods
    {
        public static IEnumerable<Labour> WithoutPasswords(this IEnumerable<Labour> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static Labour WithoutPassword(this Labour user)
        {
            user.Password = null;
            return user;
        }
    }
}
