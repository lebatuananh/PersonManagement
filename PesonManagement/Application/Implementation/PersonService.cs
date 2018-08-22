using AutoMapper.QueryableExtensions;
using PesonManagement.Application.Interface;
using PesonManagement.Application.ViewModel;
using PesonManagement.Data.Entity;
using PesonManagement.Data.Interface;
using PesonManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PesonManagement.Application.Implementation
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person, Guid> _personRepository;

        public PersonService(IRepository<Person, Guid> personRepository)
        {
            _personRepository = personRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<PersonViewModel> GetAll()
        {
            return _personRepository.FindAll(x => x.Status == Status.Active).ProjectTo<PersonViewModel>().ToList();
        }
    }
}