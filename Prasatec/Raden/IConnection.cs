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

        IDatabaseBackup Backup<T1>();
        IDatabaseBackup Backup<T1, T2>();
        IDatabaseBackup Backup<T1, T2, T3>();
        IDatabaseBackup Backup<T1, T2, T3, T4>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>();
        IDatabaseBackup Backup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>();

        IDatabaseRestore Restore(IDatabaseBackup Backup);
        IResult Compare(IDatabaseStructure Structure);
        IResult Install(IDatabaseStructure Structure);
    }
}
