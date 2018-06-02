using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmaIntegration.Models
{
    public class SnifferSettings
    {
        public string FilterString { get; set; }

        /// <summary>
        /// Список фильтров, на которые необходимо обратить внимание.
        /// </summary>
        /// <remarks>
        /// Доменное имя указывать в формате "*@company.com"
        /// Имя почтового ящика "job@*"
        /// Полностью адресата "job@*company.com"
        /// Слово и часть предложения "Первый отдел"
        /// </remarks>
        public List<string> FilterList
        {
            get
            {
                var list = !string.IsNullOrWhiteSpace(FilterString) ?
                    FilterString.Trim().Split('\n').ToList() :
                    new List<string>();
                return list;
            }
        }

        public string BlockFilterString { get; set; }

        /// <summary>
        /// Список фильтров, по которым нужно прервать поток.
        /// </summary>
        /// <remarks>
        /// Доменное имя указывать в формате "*@company.com"
        /// Имя почтового ящика "job@*"
        /// Полностью адресата "job@*company.com"
        /// Слово и часть предложения "Первый отдел"
        /// </remarks>
        public List<string> BlockFilterList
        {
            get
            {
                var list = !string.IsNullOrWhiteSpace(BlockFilterString) ?
                    BlockFilterString.Trim().Split('\n').ToList() :
                    new List<string>();
                return list;
            }
        }

        /// <summary>
        /// Список IP-адресов компьютеров сотруников на испытательном сроке
        /// </summary>
        public List<string> IpUsersOnProbation { get; set; }

        /// <summary>
        /// Список IP-адресов компьютеров сотруников, подавших заявление на увольнение
        /// </summary>
        public List<string> IpUsersOnDismissal { get; set; }

        /// <summary>
        /// Список IP-адресов, для которых фильтры применяться не будут
        /// </summary>
        public List<string> IpExceptionUsers { get; set; }
    }
}
