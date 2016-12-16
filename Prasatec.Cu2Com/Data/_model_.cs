using Prasatec.Raden;
using System;

namespace Prasatec.Cu2Com.Data
{
    public abstract class Model : IModel, IDisposable
    {
        public Model()
        {

        }

        #region " IModel "
        public int ID { get; internal set; }

        [ColumnProperty("createdAt")]
        [ColumnAutoValue(ColumnAutoValues.CreatedStamp)]
        [ValueTypeTimestamp()]
        public Timestamp CreatedAt { get; internal protected set; }

        [ColumnProperty("createdBy")]
        [ColumnAutoValue(ColumnAutoValues.CreatedUser)]
        [ColumnIndex(ColumnIndexTypes.Indexed)]
        [ValueTypeForeignKey()]
        public ForeignKey<User> CreatedBy { get; internal protected set; }

        [ColumnProperty("modifiedAt")]
        [ColumnAutoValue(ColumnAutoValues.ModifiedStamp)]
        [ValueTypeTimestamp()]
        public Timestamp ModifiedAt { get; internal protected set; }

        [ColumnProperty("modifiedBy")]
        [ColumnAutoValue(ColumnAutoValues.ModifiedUser)]
        [ValueTypeForeignKey()]
        public ForeignKey<User> ModifiedBy { get; internal protected set; }
        #endregion

        #region " IDisposable "
        ~Model()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {

        }
        #endregion

    }
}
