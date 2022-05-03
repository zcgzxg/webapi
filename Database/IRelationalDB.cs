using System.Data.Common;

namespace webapi.Database
{
    /// <summary>
    /// the relation database
    /// </summary>
    public interface IRelationalDB : IDisposable
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        DbConnection Conn { get; }
    }
}