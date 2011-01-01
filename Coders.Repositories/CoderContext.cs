using System.Data.EntityClient;
using System.Data.Objects;

namespace Coders.Repositories
{
	public partial class CoderContext : ObjectContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CoderContext"/> class.
		/// </summary>
		public CoderContext()
			: base("name=CodersEntities", "CodersEntities")
		{
			OnContextCreated();
		}

		public CoderContext(string connectionString)
			: base(connectionString, "CodersEntities")
        {
            OnContextCreated();
        }
    
		public CoderContext(EntityConnection connection)
			: base(connection, "CodersEntities")
        {
            OnContextCreated();
        }

		partial void OnContextCreated();
	}
}