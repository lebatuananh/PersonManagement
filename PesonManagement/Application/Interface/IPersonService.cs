using PesonManagement.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace PesonManagement.Application.Interface
{
    public interface IPersonService:IDisposable
    {
        List<PersonViewModel> GetAll();
    }
}