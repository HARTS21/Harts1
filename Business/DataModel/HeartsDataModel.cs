using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.DataModel
{
    public class HeartsDataModel : IHeartsDataModel
    {
        private readonly HARTS1Entities _dbHeartsEntities;
        private List<User> _users;
        private List<Approver> _approvers;
        private List<Category> _categories;

        public HeartsDataModel()
        {
            _dbHeartsEntities = new HARTS1Entities();
        }
        public IList<User> Users
        {
            get { return _users ?? (_users = _dbHeartsEntities.Users.ToList()); }
        }

        public IList<Approver> Approvers
        {
            get { return _approvers ?? (_approvers = _dbHeartsEntities.Approvers.ToList()); }
        }

        public IList<Category> Categories
        {
            get { return _categories ?? (_categories = _dbHeartsEntities.Categories.ToList()); }
        }


        public int AddCategory(Category category)
        {
            _dbHeartsEntities.Categories.AddObject(category);
            _dbHeartsEntities.SaveChanges();
            _dbHeartsEntities.AcceptAllChanges();
            if (Categories.FirstOrDefault(x => x.Id == category.Id) == default(Category))
            {
                Categories.Add(category);
            } 
            return category.Id;
        }
        public int AddApprovers(Approver approver)
        {
            _dbHeartsEntities.Approvers.AddObject(approver);
            _dbHeartsEntities.SaveChanges();
            _dbHeartsEntities.AcceptAllChanges();
            if (Approvers.FirstOrDefault(x => x.Id == approver.Id) == default(Approver))
            {
                Approvers.Add(approver);
            }  
            return approver.Id;
        }
        public bool UpdateCategory(Dictionary<string, object> dataToUpdate)
        {
            var result = false;
            var id = Convert.ToInt32(dataToUpdate["Id"]);
            var categoryToUpdate = _dbHeartsEntities.Categories.FirstOrDefault(x => x.Id == id);
            if (categoryToUpdate != default(Category))
            {
                result = UpdateInternal(dataToUpdate, categoryToUpdate);
                if (result)
                {
                    Categories.Remove(Categories.First(x => x.Id == id));
                    Categories.Add(categoryToUpdate);
                }
            }
            return result;
        }

        protected bool UpdateInternal(Dictionary<string, object> dataToUpdate, object entityInDb)
        {
            var parameterNames = dataToUpdate.Keys;
            var propertiesToSet = entityInDb.GetType()
                .GetProperties()
                .Where(pi => parameterNames.Contains(pi.Name));

            foreach (var propertyInfo in propertiesToSet)
            {
                try
                {
                    propertyInfo.SetValue(entityInDb,
                                      propertyInfo.PropertyType == typeof(bool)
                                          ? Convert.ToBoolean(
                                              Convert.ToInt16(dataToUpdate[propertyInfo.Name]))
                                          : Convert.ChangeType(dataToUpdate[propertyInfo.Name],
                                                               propertyInfo.PropertyType), null);
                }
                catch (Exception ex)
                {
                    ex = null;
                }

            }
            var result = _dbHeartsEntities.SaveChanges() != 0;
            return result;
        }
    }
}
