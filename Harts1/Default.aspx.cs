using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.DataModel;
//using System.Data.Ent

namespace Harts1
{
    public partial class _Default : System.Web.UI.Page
    {
        private IHeartsDataModel _hartsDataModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            var dateTime = DateTime.UtcNow;
            _hartsDataModel = new HeartsDataModel();
            //var  dataToAdd = new Category { CategoryName = "First1", CreatedBy = 1, CreatedOn = dateTime, ModifiedOn = dateTime };
            //_hartsDataModel.AddCategory(dataToAdd);
            AddCategory();
            
        }
        private void AddCategory()
        {  
           
        }
    }
}
