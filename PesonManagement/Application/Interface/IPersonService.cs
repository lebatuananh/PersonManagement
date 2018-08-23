using PesonManagement.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace PesonManagement.Application.Interface
{
    public interface IPersonService:IDisposable
    {
        List<PersonViewModel> GetAll();
        void Add(PersonViewModel personViewModel);
        void Update(PersonViewModel personViewModel);
        void Save();
    }
}