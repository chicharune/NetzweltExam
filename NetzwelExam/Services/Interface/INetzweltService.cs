using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetzweltExam.Controllers;
using NetzweltExam.Models;

namespace NetzweltExam.Services
{
    public interface INetzweltService
    {
        Task<UserModel> GetUser(LoginModel model);
        Task<DataModel> GetTerritory();
    }
}
