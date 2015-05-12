using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Veidibokin.Repositories
{
    public interface ParentRepository<Tag>
    {
        IQueryable<Tag> GetAll();
        Tag GetById(int id);
        void Insert(Tag entity);
        void Delete(Tag entity);
    }
}
