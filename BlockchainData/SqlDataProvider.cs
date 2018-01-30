using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockchainData
{
    /// <summary>
    /// Провайдер данных SQL.
    /// </summary>
    public class SqlDataProvider : IDataProvider
    {
        /// <summary>
        /// Добавление блока данных.
        /// </summary>
        /// <param name="version"> Версия блока. </param>
        /// <param name="createdOn"> Дата создания блока. </param>
        /// <param name="hash"> Хеш блока. </param>
        /// <param name="previousHash"> Хеш предыдущего блока. </param>
        /// <param name="data"> Данные блока. </param>
        /// <param name="user"> Данные о пользователе. </param>
        public void AddBlock(int version, DateTime createdOn, string hash, string previousHash, string data, string user)
        {
            using (var db = new BlockSqlContext())
            {
                var block = new Block()
                {
                    Version = version,
                    CreatedOn = createdOn.ToLocalTime(),
                    Hash = hash,
                    PreviousHash = previousHash,
                    Data = data,
                    User = user
                };

                db.Blocks.Add(block);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Очистить хранилище. Удаление всех блоков.
        /// </summary>
        public void Crear()
        {
            using (var db = new BlockSqlContext())
            {
                db.Blocks.RemoveRange(db.Blocks);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Получить все блоки.
        /// </summary>
        /// <returns> Список всех блоков. </returns>
        public List<Block> GetBlocks()
        {
            using (var db = new BlockSqlContext())
            {
                var blocks = db.Blocks.ToList();
                
                return blocks;
            }
        }
    }
}
