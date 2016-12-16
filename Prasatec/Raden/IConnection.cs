using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Prasatec.Raden
{
    public interface IConnection
    {
        uint ActiveUserId { get; set; }

        IQueryResult<M> Create<M>(M data) where M : IModel;
        IQueryResult<M> Retrieve<M>(IQuery query) where M : IModel;
        IQueryResult<M> Update<M>(IQuery query, M data) where M : IModel;
        IQueryResult<M> Delete<M>(IQuery query) where M : IModel;
        IQueryResult<M> Count<M>(IQuery query) where M : IModel;
        IQueryResult<M> List<M>(IQuery query) where M : IModel;

        IDatabaseBackup Backup(params Type[] Models);
        IDatabaseRestore Restore(IDatabaseBackup Backup);
        IResult Compare(IDatabaseStructure Structure);
        IResult Install(IDatabaseStructure Structure);
    }
}
