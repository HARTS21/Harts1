using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.DataModel
{
    public interface IHeartsDataModel
    {
        IList<User> Users { get; }
        IList<Approver> Approvers { get; }
        IList<Category> Categories { get; }

        int AddCategory(Category category);
        int AddApprovers(Approver approver);

        bool UpdateCategory(Dictionary<string, object> dataToUpdate);
       
    }
}
