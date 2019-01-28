using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalBot.DialogManager;

namespace MedicalBot.Common
{
    class Drug
    {
        public String Name { get; private set; }
        public Contraindications Contraindications { get; private set; }

        public Drug(String name, Contraindications contraindications)
        {
            Name = name;
            Contraindications = contraindications;
        }
    }

    class Drugs
    {
        public static List<Drug> All = new List<Drug>
        {
            new Drug("Отипакс", Contraindications.Hypersensitivity | Contraindications.TympanicMembranePerforation),
            new Drug("Отирелакс", Contraindications.Hypersensitivity | Contraindications.TympanicMembranePerforation),
            new Drug("Отофа", Contraindications.Pregnancy),
            new Drug("Нормакс", Contraindications.Pregnancy | Contraindications.Lactation),
            new Drug("Анауран", Contraindications.Pregnancy | Contraindications.Lactation),
            new Drug("Кандибиотик", Contraindications.OneYearsOld),
            new Drug("Полидекса", Contraindications.TympanicMembranePerforation),
            new Drug("Макситрол", Contraindications.TympanicMembranePerforation),
            new Drug("Софрадекс", Contraindications.Pregnancy | Contraindications.Lactation | Contraindications.OneYearsOld),
        };
    }
}
