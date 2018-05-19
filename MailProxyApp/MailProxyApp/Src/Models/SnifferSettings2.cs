using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailProxyApp.Src.Models
{
    public class SnifferSettings2
    {
        public SnifferSettings2()
        {
            if (WarningFilterList == null)
                WarningFilterList = new List<string>();

            if (BlockFilterList == null)
                BlockFilterList = new List<string>();

            if (EmployeesOnProbationIPList == null)
                EmployeesOnProbationIPList = new List<string>();

            if (EmployeesOnDismissalIPList == null)
                EmployeesOnDismissalIPList = new List<string>();
        }

        /// <summary>
        /// Список фильтров, на которые необходимо обратить внимание.
        /// </summary>
        /// <remarks>
        /// Доменное имя указывать в формате "*@company.com"
        /// Имя почтового ящика "job@*"
        /// Полностью адресата "job@*company.com"
        /// Слово и часть предложения "Первый отдел"
        /// </remarks>
        public List<string> WarningFilterList { get; set; }

        /// <summary>
        /// Список фильтров, по которым нужно прервать поток.
        /// </summary>
        /// <remarks>
        /// Доменное имя указывать в формате "*@company.com"
        /// Имя почтового ящика "job@*"
        /// Полностью адресата "job@*company.com"
        /// Слово и часть предложения "Первый отдел"
        /// </remarks>
        public List<string> BlockFilterList { get; set; }

        /// <summary>
        /// Интервал обновления настроек, часы
        /// </summary>
        public long UpdateSettingsInterval { get; set; }

        /// <summary>
        /// Полный монитор сотрудников на испытательном сроке
        /// </summary>
        public bool MonitorEmployeesOnProbation { get; set; }

        /// <summary>
        /// Список IP-адресов компьютеров сотруников на испытательном сроке
        /// </summary>
        public List<string> EmployeesOnProbationIPList { get; set; }

        /// <summary>
        /// Полный монитор сотрудников, подавших заявление на уволнение
        /// </summary>
        public bool MonitorEmployeesOnDismissal { get; set; }

        /// <summary>
        /// Список IP-адресов компьютеров сотруников, подавших заявление на увольнение
        /// </summary>
        public List<string> EmployeesOnDismissalIPList { get; set; }
    }
}
