using Prasatec.Cu2Com;
using Prasatec.Raden;
using System;
using System.Windows.Forms;

namespace Prasatec.Experience
{
    public interface IController<M, C, B>
        where M : IModel
        where C : ICollection<M>
        where B : IControllerBuilder<M, C, B>
    {
        IConnection Connection { get; }

        B Create();
        B Duplicate(M Source);
        B Update();
        B Update(M Source);
        B Retrieve();
        B Retrieve(M Source);
        B Delete();
        B Delete(M Source);
        B Find();
        B Find(M Source);
        B Count();
        B Count(M Source);

        IQueryResult<M> ShowCreate(IWin32Window owner);
        void ShowViewer(IWin32Window owner, ulong ID);
        void ShowViewer(IWin32Window owner, B Source);
        void ShowViewer(IWin32Window owner, M Source);
        IQueryResult<M> ShowEditor(IWin32Window owner, ulong ID);
        IQueryResult<M> ShowEditor(IWin32Window owner, B Source);
        IQueryResult<M> ShowEditor(IWin32Window owner, M Source);
        void ShowCollection();
        void ShowCollection(B Source);
        void ShowCollection(C Source);
    }
}
