using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonManagement.Application.Interface
{
    using PesonManagement.Application.ViewModel;

    public interface IFunctionService:IDisposable
    {
        void Add(FunctionViewModel function);

        List<FunctionViewModel> GetAll(string filter);

        IEnumerable<FunctionViewModel> GetAllWithParentId(Guid parentId);

        FunctionViewModel GetById(Guid id);

        void Update(FunctionViewModel function);

        void Delete(Guid id);

        void Save();

        bool CheckExistedId(Guid id);

        void UpdateParentId(Guid sourceId, Guid targetId, Dictionary<Guid, int> items);

        void ReOrder(Guid sourceId, Guid targetId);
    }
}
