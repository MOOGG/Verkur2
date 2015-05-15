using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Veidibokin.Repositories
{
    /// <summary>
    /// Snemma í öllu ferlinu var þetta Repo búið til. Ákveðið var að gera ParentRepo sem
    /// ætti derived Repo (UserRepository). Ástæða þess var að geta sent inn hvaða tag sem er þegar unnið væri með
    /// repo-in. Hér að neðan eru föllin skilgreind en virknin er svo framkvæmd í UserRepository
    /// </summary>
    public interface ParentRepository<Tag>
    {
        IQueryable<Tag> GetAll();
        Tag GetById(int id);
        void Insert(Tag entity);
        void Delete(Tag entity);
    }
}
