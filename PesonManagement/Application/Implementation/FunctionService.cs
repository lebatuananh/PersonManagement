using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonManagement.Application.Implementation
{
    using global::AutoMapper;
    using global::AutoMapper.QueryableExtensions;

    using Microsoft.EntityFrameworkCore;

    using PesonManagement.Application.Interface;
    using PesonManagement.Application.ViewModel;
    using PesonManagement.Data.Entity;
    using PesonManagement.Data.Interface;

    public class FunctionService:IFunctionService
    {
        private readonly IRepository<Function, Guid> _functionRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public FunctionService(IRepository<Function, Guid> functionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._functionRepository = functionRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public void Add(FunctionViewModel functionVm)
        {
            var model = this._mapper.Map<Function>(functionVm);
            this._functionRepository.Add(model);
        }

        public List<FunctionViewModel> GetAll(string filter)
        {
           var query=this._functionRepository.FindAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }

            return query.OrderBy(x => x.ParentId).ProjectTo<FunctionViewModel>().ToList();

        }

        public IEnumerable<FunctionViewModel> GetAllWithParentId(Guid parentId)
        {
            return this._functionRepository.FindAll(x => x.ParentId == parentId).ProjectTo<FunctionViewModel>().ToList();
        }

        public FunctionViewModel GetById(Guid id)
        {
            var model = this._functionRepository.FindSingle(x => x.Id == id);
            return this._mapper.Map<FunctionViewModel>(model);
        }

        public void Update(FunctionViewModel functionVm)
        {
            var functionNew = this._functionRepository.FindById(functionVm.Id);
            var function = this._mapper.Map<Function>(functionVm);
            this._functionRepository.Update(function);

        }

        public void Delete(Guid id)
        {
            this._functionRepository.Remove(id);
        }

        public void Save()
        {
            this._unitOfWork.Commit();
        }

        public bool CheckExistedId(Guid id)
        {
           return this._functionRepository.FindById(id) != null;
        }

        public void UpdateParentId(Guid sourceId, Guid targetId, Dictionary<Guid, int> items)
        {
            //Update parent id for source
            var category = this._functionRepository.FindById(sourceId);
            category.ParentId = targetId;
            this._functionRepository.Update(category);

            //Get all sibling
            var sibling = this._functionRepository.FindAll(x => items.ContainsKey(x.Id));
            foreach (var child in sibling)
            {
                child.SortOrder = items[child.Id];
                this._functionRepository.Update(child);
            }


        }

        public void ReOrder(Guid sourceId, Guid targetId)
        {
            var source = this._functionRepository.FindById(sourceId);
            var target = this._functionRepository.FindById(targetId);
            int tempOrder = source.SortOrder;

            source.SortOrder = target.SortOrder;
            target.SortOrder = tempOrder;

            this._functionRepository.Update(source);
            this._functionRepository.Update(target);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
