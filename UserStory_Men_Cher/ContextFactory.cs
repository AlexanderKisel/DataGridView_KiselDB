using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.Design;

namespace DataGridView_Kisel
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        => new Context(Json.Option());
    }
}
