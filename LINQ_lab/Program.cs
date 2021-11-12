using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_lab
{
    #region Задача
    /*+ Модель  компьютера  характеризуется  кодом          Art
     *+ и  названием  марки компьютера,                     Brand
     *+ типом  процессора,                                  Cpu
     *+ частотой  работы  процессора,                       CpuFrequency
     *+ объемом оперативной памяти,                         Ram
     *+ объемом жесткого диска,                             HardVolume
     *+ объемом памяти видеокарты,                          Vram
     *+ стоимостью компьютера в условных единицах           Price
     *+ и количеством экземпляров, имеющихся в наличии.     Stock
     *
     *+ Создать список, содержащий 6-10 записей с различным набором значений характеристик.
     *- Определить:
     *++ все компьютеры с указанным процессором. Название процессора запросить у пользователя;
     *++ все компьютеры с объемом ОЗУ не ниже, чем указано. Объем ОЗУ запросить у пользователя;
     *++ вывести весь список, отсортированный по увеличению стоимости;
     *++ вывести весь список, сгруппированный по типу процессора;
     *++ найти самый дорогой и самый бюджетный компьютер;
     *++ есть ли хотя бы один компьютер в количестве не менее 30 штук?
     */
    #endregion

    class Pc
    {
        public string Art { get; set; }
        public string Brand { get; set; }
        public string Cpu { get; set; }
        public int CpuFrequency { get; set; }
        public int Ram { get; set; }
        public int HardVolume { get; set; }
        public int Vram { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Pc> listPc = new List<Pc>()
            {
                new Pc(){ Art="10MKS10V00", Brand="Lenovo ThinkCentre M910s SFF", Cpu="Intel Core i5", CpuFrequency=3400, Ram=8, HardVolume=256, Vram=0, Price=432.96, Stock=5 },
                new Pc(){ Art="3080-9872", Brand="Dell OptiPlex 3080 Micro", Cpu="Intel Core i3", CpuFrequency=3000, Ram=8, HardVolume=256, Vram=0, Price=490.28, Stock=2 },
                new Pc(){ Art="30E0008LRU", Brand="Lenovo ThinkStation P620", Cpu="AMD Ryzen Threadripper", CpuFrequency=3500, Ram=32, HardVolume=2048, Vram=0, Price=6696.20, Stock=1 },
                new Pc(){ Art="5820-8109", Brand="Dell Precision 5820", Cpu="Intel Xeon", CpuFrequency=3600, Ram=16, HardVolume=1024, Vram=5120, Price=2421.83, Stock=5},
                new Pc(){ Art="MXNG2RU/A", Brand="Apple Mac Mini", Cpu="Intel Core i5", CpuFrequency=3000, Ram=8, HardVolume=512, Vram=0, Price=1692.39, Stock=3 },
                new Pc(){ Art="3650-7012", Brand="Dell Precision 3650 MT", Cpu="Intel Xeon", CpuFrequency=3300, Ram=16, HardVolume=512, Vram=4096, Price=1999.15, Stock=9 },
                new Pc(){ Art="3240-5214", Brand="Dell Precision 3240", Cpu="Intel Core i5", CpuFrequency=3100, Ram=8, HardVolume=256, Vram=2048, Price=1263.80, Stock=5 },
                new Pc(){ Art="273F1EA", Brand="HP EliteDesk 805 G6 SFF", Cpu="AMD Ryzen 7", CpuFrequency=3600, Ram=8, HardVolume=256, Vram=4096, Price=1140.42, Stock=10 }
            };
            #region Поиск компьютеров по запрашиваемому названию процессора
            Console.Write("Введите название процессора: ");
            string cpuQuery = Console.ReadLine().ToLower();
            List<Pc> cpuQ = (from c in listPc
                             where c.Cpu.ToLower().Contains(cpuQuery)
                             select c).ToList();
            foreach (var c in cpuQ)
                Console.WriteLine($"{c.Brand} ({c.Art}) процессор: {c.Cpu} - ${c.Price}");
            #endregion
            #region Поиск компьютеров по запрашиваемому объему ОЗУ, не менее
            Console.Write("\nВведите требуемый объем ОЗУ: ");
            int ramQuery = Convert.ToInt32(Console.ReadLine());
            var ramQ = listPc
                .Where(r => r.Ram >= ramQuery)
                .ToList();
            foreach (var r in ramQ)
                Console.WriteLine($"{r.Brand} ({r.Art}) размер ОЗУ: {r.Ram}Гб - ${r.Price}");
            #endregion
            #region Сортировка по стоимости
            var sortList = listPc
                .OrderBy(s => s.Price)
                .ToList();
            Console.WriteLine("\nОтсортированный список по стоимости:");
            foreach (var s in sortList)
                Console.WriteLine($"{s.Brand} ({s.Art}), {s.Cpu}, {s.CpuFrequency}МГц, RAM {s.Ram}Гб, HDD {s.HardVolume}Гб, VRAM {s.Vram}Мб, ${s.Price}");
            #endregion
            #region Группировка по типу процессора
            var groupList = from g in listPc
                            group g by g.Cpu;
            Console.WriteLine($"Группировка списка по типу процессора:");
            foreach (var g in groupList)
            {
                foreach (var t in g)
                {
                    Console.WriteLine($"{t.Brand} {t.Cpu}");
                }
            }
            #endregion
            #region Самый дорогой и самый бюджетный компьютеры
            foreach (Pc member in listPc)
            {
                if (member.Price == listPc.Max(m => m.Price))
                    Console.WriteLine($"\nСамый дорогой компьютер: {member.Brand} стоимостью ${member.Price}");
                if (member.Price == listPc.Min(m => m.Price))
                    Console.WriteLine($"\nСамый бюджетный компьютер: {member.Brand} стоимостью ${member.Price}");
            }
            #endregion
            Console.WriteLine((listPc.Any(s => s.Stock >= 30))? "\nВ наличии присутствует позиция с количеством более 30":"\nОтсутствует в наличии позиция с количеством больше 30");
            Console.ReadKey();
        }
    }
}
