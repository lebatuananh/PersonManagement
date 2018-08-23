using AutoMapper.QueryableExtensions;
using PesonManagement.Application.Interface;
using PesonManagement.Application.ViewModel;
using PesonManagement.Data.Entity;
using PesonManagement.Data.Interface;
using PesonManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace PesonManagement.Application.Implementation
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person, Guid> _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IRepository<Person, Guid> personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<PersonViewModel> GetAll()
        {
            return _personRepository.FindAll(x => x.Status == Status.Active).ProjectTo<PersonViewModel>().ToList();
        }

        public void Add(PersonViewModel personViewModel)
        {
            var person = Mapper.Map<PersonViewModel, Person>(personViewModel);
            _personRepository.Add(person);
        }

        public void Update(PersonViewModel personViewModel)
        {
            var personOld = _personRepository.FindById(personViewModel.Id);
            var person = Mapper.Map<PersonViewModel, Person>(personViewModel);
            _personRepository.Update(person);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}