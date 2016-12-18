using Prasatec.Raden;

namespace Prasatec.Cu2Com
{
    public interface IControllerBuilder<M, C, B> : IBuilder<M>
        where M : IModel
        where C : ICollection<M>
        where B : IControllerBuilder<M, C, B>
    {
        B Clone(M Source);

        IQueryResult<M> Execute();
        C GetCollection();

        B ID(ulong Value);
        B ID(M Value);
        B ID(ForeignKey<M> Value);
    }
}
