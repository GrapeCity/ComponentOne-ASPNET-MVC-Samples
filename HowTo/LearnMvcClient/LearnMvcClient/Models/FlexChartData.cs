using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnMvcClient.Models
{
    public static class FlexChartData
    {
        #region Sale
        public class Sale
        {
            public string Country { get; set; }
            public int Sales { get; set; }
            public int Expenses { get; set; }
            public int Downloads { get; set; }
        }

        private static IEnumerable<Sale> _sales1;
        public static IEnumerable<Sale> GetSales1()
        {
            if (_sales1 == null)
            {
                _sales1 = GetSalesData(new[] { "US", "Germany", "UK", "Japan", "Italy", "Greece" });
            }

            return _sales1;
        }

        private static IEnumerable<Sale> _sales2;
        private static string[] _countries2 = new[] { "US", "Canada", "Mexico", "UK", "Germany", "France", "Japan", "Korea", "China" };
        public static IEnumerable<Sale> GetSales2(int minSales = 0)
        {
            if (minSales > 0)
            {
                return GetSalesData(_countries2, minSales);
            }

            if (_sales2 == null)
            {
                _sales2 = GetSalesData(_countries2);
            }

            return _sales2;
        }

        private static IEnumerable<Sale> _sales3;
        private static string[] _countries3 = new[] { "US", "Canada", " Mexico", " UK", " Germany", " France", " Italy", " Japan", " Korea", " China", " Autralia", " New Zealand" };
        public static IEnumerable<Sale> GetSales3(int minSales = 0)
        {
            if (minSales > 0)
            {
                return GetSalesData(_countries3, minSales);
            }

            if (_sales3 == null)
            {
                _sales3 = GetSalesData(_countries3);
            }

            return _sales3;
        }

        private static IEnumerable<Sale> GetSalesData(string[] countries, int minSales = 0, int minExpenses = 0, int minDownloads = 0)
        {
            var rand = new Random(0);
            var list = new List<Sale>();
            for (var i = 0; i < countries.Length; i++)
            {
                list.Add(new Sale
                {
                    Country = countries[i],
                    Sales = minSales + rand.Next(10000),
                    Expenses = minExpenses + rand.Next(5000),
                    Downloads = minDownloads + rand.Next(20000)
                });
            }

            return list;
        }
        #endregion

        #region Stock
        public class Stock
        {
            public DateTime Date { get; set; }
            public double Open { get; set; }
            public double High { get; set; }
            public double Low { get; set; }
            public double Close { get; set; }
            public double Vol { get; set; }
        }

        private static IEnumerable<Stock> _stocks1;
        public static IEnumerable<Stock> GetStocks1()
        {
            if (_stocks1 == null)
            {
                _stocks1 = GetStocks1Data();
            }

            return _stocks1;
        }

        // some random stock data
        private static IEnumerable<Stock> GetStocks1Data()
        {
            var list = new List<Stock>();
            list.Add(new Stock { Date = new DateTime(2017, 3, 23), Open = 841.39, High = 841.69, Low = 833, Close = 839.65, Vol = 3287669 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 22), Open = 849.48, High = 855.35, Low = 847, Close = 849.8, Vol = 1366749 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 21), Open = 870.06, High = 873.47, Low = 847.69, Close = 850.14, Vol = 2537978 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 20), Open = 869.48, High = 870.34, Low = 864.66, Close = 867.91, Vol = 1542199 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 17), Open = 873.68, High = 874.42, Low = 868.37, Close = 872.37, Vol = 1868252 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 16), Open = 870.53, High = 872.71, Low = 867.52, Close = 870, Vol = 1104452 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 15), Open = 867.94, High = 869.88, Low = 861.3, Close = 868.39, Vol = 1332885 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 14), Open = 863.75, High = 867.58, Low = 860.13, Close = 865.91, Vol = 1061692 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 13), Open = 860.83, High = 867.13, Low = 860.82, Close = 864.58, Vol = 1166605 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 10), Open = 862.7, High = 864.23, Low = 857.61, Close = 861.4, Vol = 1336585 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 9), Open = 853.69, High = 860.71, Low = 852.67, Close = 857.84, Vol = 1347697 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 8), Open = 853.12, High = 856.93, Low = 851.25, Close = 853.64, Vol = 1029834 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 7), Open = 847.26, High = 853.33, Low = 845.52, Close = 851.15, Vol = 1038696 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 6), Open = 846.86, High = 848.94, Low = 841.17, Close = 847.27, Vol = 1047872 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 3), Open = 848.94, High = 850.82, Low = 844.7, Close = 849.08, Vol = 1006612 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 2), Open = 856.31, High = 856.49, Low = 848.72, Close = 849.85, Vol = 1250938 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 1), Open = 851.38, High = 858, Low = 849.02, Close = 856.75, Vol = 1818671 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 28), Open = 847.35, High = 848.83, Low = 841.44, Close = 844.93, Vol = 1383119 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 27), Open = 844.95, High = 850.67, Low = 843.01, Close = 849.67, Vol = 1010333 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 24), Open = 847.65, High = 848.36, Low = 842.96, Close = 847.81, Vol = 1346189 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 23), Open = 851.08, High = 852.62, Low = 842.5, Close = 851, Vol = 1386681 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 22), Open = 848, High = 853.79, Low = 846.71, Close = 851.36, Vol = 1224889 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 21), Open = 847.99, High = 852.2, Low = 846.55, Close = 849.27, Vol = 1261062 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 17), Open = 841.31, High = 846.94, Low = 839.78, Close = 846.55, Vol = 1461210 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 16), Open = 838.5, High = 842.69, Low = 837.26, Close = 842.17, Vol = 1005084 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 15), Open = 838.81, High = 841.77, Low = 836.22, Close = 837.32, Vol = 1357185 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 14), Open = 839.77, High = 842, Low = 835.83, Close = 840.03, Vol = 1363345 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 13), Open = 837.7, High = 841.74, Low = 836.25, Close = 838.96, Vol = 1295708 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 10), Open = 832.95, High = 837.15, Low = 830.51, Close = 834.85, Vol = 1415128 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 9), Open = 831.73, High = 831.98, Low = 826.5, Close = 830.06, Vol = 1194238 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 8), Open = 830.53, High = 834.25, Low = 825.11, Close = 829.88, Vol = 1302225 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 7), Open = 825.5, High = 831.92, Low = 823.29, Close = 829.23, Vol = 1666605 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 6), Open = 820.92, High = 822.39, Low = 814.29, Close = 821.62, Vol = 1350875 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 3), Open = 823.13, High = 826.13, Low = 819.35, Close = 820.13, Vol = 1528095 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 2), Open = 815, High = 824.56, Low = 812.05, Close = 818.26, Vol = 1689179 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 1), Open = 824, High = 824, Low = 812.25, Close = 815.24, Vol = 2251047 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 31), Open = 819.5, High = 823.07, Low = 813.4, Close = 820.19, Vol = 2020180 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 30), Open = 837.06, High = 837.23, Low = 821.03, Close = 823.83, Vol = 3516933 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 27), Open = 859, High = 867, Low = 841.9, Close = 845.03, Vol = 3752497 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 26), Open = 859.05, High = 861, Low = 850.52, Close = 856.98, Vol = 3493251 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 25), Open = 853.55, High = 858.79, Low = 849.74, Close = 858.45, Vol = 1662148 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 24), Open = 846.98, High = 851.52, Low = 842.28, Close = 849.53, Vol = 1688375 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 23), Open = 831.61, High = 845.54, Low = 828.7, Close = 844.43, Vol = 2457377 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 20), Open = 829.09, High = 829.24, Low = 824.6, Close = 828.17, Vol = 1306183 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 19), Open = 829, High = 833, Low = 823.96, Close = 824.37, Vol = 1070454 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 18), Open = 829.8, High = 829.81, Low = 824.08, Close = 829.02, Vol = 1027698 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 17), Open = 830, High = 830.18, Low = 823.2, Close = 827.46, Vol = 1440905 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 13), Open = 831, High = 834.65, Low = 829.52, Close = 830.94, Vol = 1290182 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 12), Open = 828.38, High = 830.38, Low = 821.01, Close = 829.53, Vol = 1350308 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 11), Open = 826.62, High = 829.9, Low = 821.47, Close = 829.86, Vol = 1325394 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 10), Open = 827.07, High = 829.41, Low = 823.14, Close = 826.01, Vol = 1197442 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 9), Open = 826.37, High = 830.43, Low = 821.62, Close = 827.18, Vol = 1408924 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 6), Open = 814.99, High = 828.96, Low = 811.5, Close = 825.21, Vol = 2017097 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 5), Open = 807.5, High = 813.74, Low = 805.92, Close = 813.02, Vol = 1340535 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 4), Open = 809.89, High = 813.43, Low = 804.11, Close = 807.77, Vol = 1515339 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 3), Open = 800.62, High = 811.44, Low = 796.89, Close = 808.01, Vol = 1959033 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 30), Open = 803.21, High = 803.28, Low = 789.62, Close = 792.45, Vol = 1735879 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 29), Open = 802.33, High = 805.75, Low = 798.14, Close = 802.88, Vol = 1057392 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 28), Open = 813.33, High = 813.33, Low = 802.44, Close = 804.57, Vol = 1214756 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 27), Open = 808.68, High = 816, Low = 805.8, Close = 809.93, Vol = 975962 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 23), Open = 808.01, High = 810.97, Low = 805.11, Close = 807.8, Vol = 765537 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 22), Open = 809.1, High = 811.07, Low = 806.03, Close = 809.68, Vol = 1132119 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 21), Open = 815.72, High = 815.72, Low = 805.1, Close = 812.2, Vol = 1459628 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 20), Open = 813.37, High = 816.49, Low = 811, Close = 815.2, Vol = 1271883 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 19), Open = 809.28, High = 816.22, Low = 804.5, Close = 812.5, Vol = 1263581 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 16), Open = 818.31, High = 819.2, Low = 808.12, Close = 809.84, Vol = 2598866 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 15), Open = 817.36, High = 823, Low = 812, Close = 815.65, Vol = 1769719 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 14), Open = 815.92, High = 824.26, Low = 812.78, Close = 817.89, Vol = 1799654 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 13), Open = 812.39, High = 824.3, Low = 811.94, Close = 815.34, Vol = 2128210 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 12), Open = 804.82, High = 811.35, Low = 804.53, Close = 807.9, Vol = 1628602 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 9), Open = 799.3, High = 809.95, Low = 798.05, Close = 809.45, Vol = 1904463 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 8), Open = 792.95, High = 799, Low = 787.9, Close = 795.17, Vol = 1612531 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 7), Open = 779.95, High = 792, Low = 773.53, Close = 791.47, Vol = 2029415 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 6), Open = 780.19, High = 785.28, Low = 773.32, Close = 776.18, Vol = 1734099 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 5), Open = 770, High = 780, Low = 766.97, Close = 778.22, Vol = 1688473 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 2), Open = 761.9, High = 770.5, Low = 759, Close = 764.46, Vol = 1718846 });
            list.Add(new Stock { Date = new DateTime(2016, 12, 1), Open = 778.55, High = 778.6, Low = 753.36, Close = 764.33, Vol = 2867074 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 30), Open = 789.1, High = 791.51, Low = 773.14, Close = 775.88, Vol = 2279054 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 29), Open = 788.38, High = 796.44, Low = 785.34, Close = 789.44, Vol = 1561981 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 28), Open = 778.35, High = 799.74, Low = 778.1, Close = 785.79, Vol = 2575432 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 25), Open = 782.61, High = 782.9, Low = 778.19, Close = 780.23, Vol = 613549 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 23), Open = 789.52, High = 789.52, Low = 772.65, Close = 779, Vol = 1312981 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 22), Open = 788.99, High = 793.77, Low = 783.74, Close = 785, Vol = 1394174 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 21), Open = 778.1, High = 786.55, Low = 776.3, Close = 784.8, Vol = 1630835 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 18), Open = 787.17, High = 791.29, Low = 775.35, Close = 775.97, Vol = 1806264 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 17), Open = 782.5, High = 788.9, Low = 779.85, Close = 786.16, Vol = 1533679 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 16), Open = 770.42, High = 783.5, Low = 766.33, Close = 779.98, Vol = 1798360 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 15), Open = 765.47, High = 780.24, Low = 765.22, Close = 775.16, Vol = 2943889 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 14), Open = 771.76, High = 771.78, Low = 743.59, Close = 753.22, Vol = 3688274 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 11), Open = 776.81, High = 777.29, Low = 765.54, Close = 771.75, Vol = 3592641 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 10), Open = 810, High = 810.06, Low = 768.23, Close = 780.29, Vol = 5909609 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 9), Open = 801.83, High = 811.71, Low = 792.04, Close = 805.59, Vol = 3098510 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 8), Open = 802.03, High = 816.04, Low = 799.62, Close = 811.98, Vol = 1769069 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 7), Open = 794.95, High = 805, Low = 792.9, Close = 802.03, Vol = 1992570 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 4), Open = 771.3, High = 788.48, Low = 771, Close = 781.1, Vol = 1970603 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 3), Open = 784.5, High = 790, Low = 778.63, Close = 782.19, Vol = 2175216 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 2), Open = 806.76, High = 806.76, Low = 785, Close = 788.42, Vol = 2350736 });
            list.Add(new Stock { Date = new DateTime(2016, 11, 1), Open = 810.87, High = 813.96, Low = 798.26, Close = 805.48, Vol = 2355890 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 31), Open = 822.43, High = 822.63, Low = 808, Close = 809.9, Vol = 2242679 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 28), Open = 829.94, High = 839, Low = 817, Close = 819.56, Vol = 4354884 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 27), Open = 823.01, High = 826.58, Low = 814.61, Close = 817.35, Vol = 2973486 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 26), Open = 827.12, High = 827.71, Low = 816.35, Close = 822.1, Vol = 1794868 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 25), Open = 838.5, High = 838.5, Low = 825.3, Close = 828.55, Vol = 1890712 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 24), Open = 830.09, High = 837.94, Low = 829.04, Close = 835.74, Vol = 1447616 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 21), Open = 820, High = 824.29, Low = 818.31, Close = 824.06, Vol = 1615814 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 20), Open = 827.56, High = 828.46, Low = 820.55, Close = 821.63, Vol = 1393870 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 19), Open = 824.52, High = 829.81, Low = 823.2, Close = 827.09, Vol = 1500142 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 18), Open = 814.21, High = 828.81, Low = 813.33, Close = 821.49, Vol = 2289265 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 17), Open = 805.99, High = 813.49, Low = 803.83, Close = 806.84, Vol = 1056367 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 14), Open = 807.45, High = 810.09, Low = 802.32, Close = 804.6, Vol = 1111934 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 13), Open = 806.07, High = 806.56, Low = 798.62, Close = 804.08, Vol = 1368981 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 12), Open = 811.96, High = 814.5, Low = 808.55, Close = 811.77, Vol = 907876 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 11), Open = 814.17, High = 819.86, Low = 807.37, Close = 809.57, Vol = 1721575 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 10), Open = 803.93, High = 817.38, Low = 802.24, Close = 814.17, Vol = 1496115 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 7), Open = 805.93, High = 805.94, Low = 796.82, Close = 800.71, Vol = 1163899 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 6), Open = 804.08, High = 806.94, Low = 800.51, Close = 803.08, Vol = 1099909 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 5), Open = 806, High = 808.5, Low = 800.71, Close = 801.23, Vol = 1213820 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 4), Open = 805, High = 806.5, Low = 799.67, Close = 802.79, Vol = 1258706 });
            list.Add(new Stock { Date = new DateTime(2016, 10, 3), Open = 802.55, High = 803.54, Low = 796.23, Close = 800.38, Vol = 1489212 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 30), Open = 803.6, High = 808.09, Low = 801.5, Close = 804.06, Vol = 1660201 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 29), Open = 807.23, High = 813.91, Low = 800.86, Close = 802.64, Vol = 1349974 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 28), Open = 804.08, High = 810.25, Low = 802.78, Close = 810.06, Vol = 1470280 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 27), Open = 801.83, High = 813.49, Low = 801.83, Close = 810.73, Vol = 1367271 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 26), Open = 809.82, High = 810.08, Low = 800.45, Close = 802.65, Vol = 1472732 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 23), Open = 815.14, High = 817, Low = 812.73, Close = 814.96, Vol = 1411673 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 22), Open = 810, High = 819.06, Low = 807.71, Close = 815.95, Vol = 1759290 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 21), Open = 801.26, High = 805.91, Low = 796.03, Close = 805.03, Vol = 1348476 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 20), Open = 800, High = 802.75, Low = 798.26, Close = 799.78, Vol = 1050041 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 19), Open = 801.11, High = 803.99, Low = 793.56, Close = 795.39, Vol = 1149522 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 16), Open = 799.02, High = 799.02, Low = 793.62, Close = 797.97, Vol = 2130571 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 15), Open = 790.01, High = 803.64, Low = 788.66, Close = 801.23, Vol = 1566360 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 14), Open = 787.53, High = 796.33, Low = 787.53, Close = 790.46, Vol = 1313912 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 13), Open = 794.01, High = 795.79, Low = 784.33, Close = 788.72, Vol = 1809044 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 12), Open = 784.52, High = 800.17, Low = 783.5, Close = 798.82, Vol = 1863737 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 9), Open = 798.77, High = 801.75, Low = 788.05, Close = 788.48, Vol = 1887633 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 8), Open = 805.22, High = 808.42, Low = 801.01, Close = 802.84, Vol = 1177660 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 7), Open = 807.93, High = 810.6, Low = 803.72, Close = 807.99, Vol = 1145724 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 6), Open = 798.39, High = 810.89, Low = 795.43, Close = 808.02, Vol = 1989537 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 2), Open = 795.27, High = 797.1, Low = 793.26, Close = 796.87, Vol = 1347368 });
            list.Add(new Stock { Date = new DateTime(2016, 9, 1), Open = 791.98, High = 792.89, Low = 786.33, Close = 791.4, Vol = 1303460 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 31), Open = 789.6, High = 791.57, Low = 787.2, Close = 789.85, Vol = 1071420 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 30), Open = 792.88, High = 798, Low = 789.47, Close = 791.92, Vol = 1167413 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 29), Open = 793.05, High = 798.52, Low = 790.32, Close = 795.82, Vol = 773698 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 26), Open = 792.49, High = 799.4, Low = 789.41, Close = 793.22, Vol = 1248881 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 25), Open = 792, High = 794.72, Low = 787.23, Close = 791.3, Vol = 1202680 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 24), Open = 796.86, High = 798.46, Low = 790.76, Close = 793.6, Vol = 1284437 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 23), Open = 800.48, High = 801, Low = 795.99, Close = 796.59, Vol = 917513 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 22), Open = 798.51, High = 799.3, Low = 794.33, Close = 796.95, Vol = 853365 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 19), Open = 799.79, High = 801.23, Low = 796.88, Close = 799.65, Vol = 1120763 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 18), Open = 805.36, High = 808, Low = 801.63, Close = 802.75, Vol = 865160 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 17), Open = 800, High = 805.63, Low = 796.3, Close = 805.42, Vol = 1066070 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 16), Open = 803.5, High = 804.26, Low = 797, Close = 801.19, Vol = 1057897 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 15), Open = 807.21, High = 811.36, Low = 804.03, Close = 805.96, Vol = 930074 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 12), Open = 805.09, High = 807.19, Low = 803.64, Close = 807.05, Vol = 897283 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 11), Open = 810.47, High = 813.88, Low = 806, Close = 808.2, Vol = 1282274 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 10), Open = 807.05, High = 810.88, Low = 806.49, Close = 808.49, Vol = 918514 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 9), Open = 804.49, High = 813.33, Low = 804.06, Close = 807.48, Vol = 1607685 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 8), Open = 806, High = 807.6, Low = 801.69, Close = 805.23, Vol = 1221609 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 5), Open = 800.11, High = 807.22, Low = 797.81, Close = 806.93, Vol = 1807271 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 4), Open = 798.24, High = 800.2, Low = 793.92, Close = 797.25, Vol = 1076031 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 3), Open = 796.47, High = 799.54, Low = 793.02, Close = 798.92, Vol = 1461025 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 2), Open = 797.33, High = 802.32, Low = 794.53, Close = 800.12, Vol = 1996354 });
            list.Add(new Stock { Date = new DateTime(2016, 8, 1), Open = 786.67, High = 807.49, Low = 785.04, Close = 800.94, Vol = 3029658 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 29), Open = 797.71, High = 803.94, Low = 790, Close = 791.34, Vol = 5090527 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 28), Open = 768.84, High = 768.97, Low = 759.09, Close = 765.84, Vol = 3673221 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 27), Open = 758.97, High = 764.45, Low = 755.93, Close = 761.97, Vol = 1608589 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 26), Open = 757.52, High = 759.26, Low = 752.75, Close = 757.65, Vol = 1189573 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 25), Open = 757.68, High = 759.82, Low = 754.07, Close = 757.52, Vol = 1073310 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 22), Open = 757.32, High = 759.45, Low = 752.66, Close = 759.28, Vol = 1046024 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 21), Open = 757, High = 758.15, Low = 751.52, Close = 754.41, Vol = 953053 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 20), Open = 754.05, High = 760.64, Low = 754.05, Close = 757.08, Vol = 1125117 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 19), Open = 749.87, High = 756.59, Low = 748.49, Close = 753.41, Vol = 1521795 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 18), Open = 737.91, High = 755.14, Low = 736.51, Close = 753.2, Vol = 1934900 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 15), Open = 741, High = 741, Low = 734.64, Close = 735.63, Vol = 1617087 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 14), Open = 733.94, High = 736.14, Low = 730.59, Close = 735.8, Vol = 1070351 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 13), Open = 735.52, High = 735.52, Low = 729.02, Close = 729.48, Vol = 1021827 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 12), Open = 731.92, High = 735.6, Low = 727.5, Close = 732.51, Vol = 1328680 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 11), Open = 719.42, High = 728.93, Low = 718.86, Close = 727.2, Vol = 1441113 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 8), Open = 710.56, High = 717.9, Low = 708.11, Close = 717.78, Vol = 1497323 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 7), Open = 710.11, High = 710.17, Low = 700.67, Close = 707.26, Vol = 1058698 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 6), Open = 699.84, High = 713, Low = 699, Close = 708.97, Vol = 1445126 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 5), Open = 705.01, High = 708.12, Low = 699.13, Close = 704.89, Vol = 1422028 });
            list.Add(new Stock { Date = new DateTime(2016, 7, 1), Open = 705.1, High = 712.53, Low = 703.73, Close = 710.25, Vol = 1549160 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 30), Open = 697.65, High = 703.77, Low = 694.9, Close = 703.53, Vol = 2112513 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 29), Open = 694.26, High = 699.5, Low = 692.68, Close = 695.19, Vol = 2156218 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 28), Open = 691.37, High = 692.74, Low = 684.85, Close = 691.26, Vol = 1912280 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 27), Open = 682.49, High = 683.32, Low = 672.66, Close = 681.14, Vol = 2919486 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 24), Open = 690.17, High = 705, Low = 684.91, Close = 685.2, Vol = 4771780 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 23), Open = 710.55, High = 714.88, Low = 700.25, Close = 714.87, Vol = 2125028 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 22), Open = 714.05, High = 714.21, Low = 705.88, Close = 710.47, Vol = 1452884 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 21), Open = 710.05, High = 715.38, Low = 704.66, Close = 708.88, Vol = 1515918 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 20), Open = 710.31, High = 715.87, Low = 705.41, Close = 706.13, Vol = 2282725 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 17), Open = 721.39, High = 721.39, Low = 701.12, Close = 704.25, Vol = 4113085 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 16), Open = 727.96, High = 730.39, Low = 715.54, Close = 724.25, Vol = 2250083 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 15), Open = 734.92, High = 737.15, Low = 731.31, Close = 732.19, Vol = 1161701 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 14), Open = 729.31, High = 736, Low = 726.5, Close = 733.25, Vol = 1329176 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 13), Open = 729.82, High = 739, Low = 729.82, Close = 731.88, Vol = 1167736 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 10), Open = 735.95, High = 739.64, Low = 730.51, Close = 733.19, Vol = 1452456 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 9), Open = 737.07, High = 743.93, Low = 736.5, Close = 742.52, Vol = 958963 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 8), Open = 739.5, High = 743.81, Low = 735.76, Close = 742.93, Vol = 1615727 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 7), Open = 733.27, High = 736.71, Low = 730.8, Close = 731.09, Vol = 1215741 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 6), Open = 738.5, High = 738.5, Low = 728.29, Close = 730.06, Vol = 1499648 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 3), Open = 741.49, High = 741.49, Low = 733.91, Close = 735.86, Vol = 1230376 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 2), Open = 746.1, High = 747.3, Low = 737, Close = 744.27, Vol = 1695824 });
            list.Add(new Stock { Date = new DateTime(2016, 6, 1), Open = 748.47, High = 751.37, Low = 744.34, Close = 748.46, Vol = 1039847 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 31), Open = 748.76, High = 753.48, Low = 745.57, Close = 748.85, Vol = 2124248 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 27), Open = 737.51, High = 747.91, Low = 737.01, Close = 747.6, Vol = 1738913 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 26), Open = 736, High = 741.1, Low = 733, Close = 736.93, Vol = 1298295 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 25), Open = 735, High = 739.89, Low = 732.6, Close = 738.1, Vol = 1610773 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 24), Open = 719.85, High = 734.2, Low = 719.64, Close = 733.03, Vol = 1890195 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 23), Open = 719.98, High = 723.5, Low = 716.94, Close = 717.25, Vol = 1233407 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 20), Open = 716.46, High = 727.7, Low = 715.01, Close = 721.71, Vol = 1722506 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 19), Open = 718.3, High = 720.5, Low = 710.3, Close = 715.31, Vol = 1495741 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 18), Open = 718.5, High = 725.57, Low = 715.02, Close = 721.78, Vol = 1620691 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 17), Open = 731.06, High = 735.85, Low = 718, Close = 720.19, Vol = 1667998 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 16), Open = 724.32, High = 732.68, Low = 720, Close = 730.3, Vol = 1124220 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 13), Open = 726.62, High = 731.29, Low = 723.51, Close = 724.83, Vol = 1241301 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 12), Open = 732, High = 735.37, Low = 724.27, Close = 728.07, Vol = 1352779 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 11), Open = 740.52, High = 740.8, Low = 727.9, Close = 730.55, Vol = 1484998 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 10), Open = 734.96, High = 740, Low = 731.61, Close = 739.38, Vol = 1627899 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 9), Open = 726.7, High = 734.29, Low = 723.5, Close = 729.13, Vol = 1898866 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 6), Open = 712.2, High = 725.99, Low = 711.95, Close = 725.18, Vol = 1982458 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 5), Open = 715, High = 717.55, Low = 709.45, Close = 714.71, Vol = 1479828 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 4), Open = 706.77, High = 715.05, Low = 704.05, Close = 711.37, Vol = 1610174 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 3), Open = 712.5, High = 713.37, Low = 707.33, Close = 708.44, Vol = 1922797 });
            list.Add(new Stock { Date = new DateTime(2016, 5, 2), Open = 711.92, High = 715.41, Low = 706.36, Close = 714.41, Vol = 1672285 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 29), Open = 704.12, High = 712.11, Low = 703.78, Close = 707.88, Vol = 2906358 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 28), Open = 723.29, High = 729.26, Low = 703.2, Close = 705.06, Vol = 3114203 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 27), Open = 725.32, High = 727.15, Low = 709.08, Close = 721.46, Vol = 3289534 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 26), Open = 744.42, High = 745.59, Low = 720.32, Close = 725.37, Vol = 2754324 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 25), Open = 735.35, High = 744.88, Low = 735.1, Close = 742.21, Vol = 2469163 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 22), Open = 743.91, High = 753.92, Low = 730.37, Close = 737.77, Vol = 6978506 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 21), Open = 777.31, High = 781.68, Low = 771.55, Close = 780, Vol = 3326803 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 20), Open = 779.16, High = 779.66, Low = 771.27, Close = 774.92, Vol = 1706231 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 19), Open = 790.5, High = 790.95, Low = 770.27, Close = 776.25, Vol = 2191366 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 18), Open = 780.19, High = 788.55, Low = 777.61, Close = 787.68, Vol = 1669701 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 15), Open = 775.5, High = 780.93, Low = 774.93, Close = 780, Vol = 1555865 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 14), Open = 775.36, High = 779.61, Low = 773.22, Close = 775.39, Vol = 1348011 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 13), Open = 770.31, High = 775.75, Low = 764.59, Close = 771.91, Vol = 1768980 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 12), Open = 758.43, High = 764.92, Low = 751.56, Close = 764.32, Vol = 1366559 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 11), Open = 765.45, High = 767.22, Low = 757.34, Close = 757.54, Vol = 1584036 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 8), Open = 765.87, High = 767.13, Low = 755.77, Close = 759.47, Vol = 1171738 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 7), Open = 765.32, High = 769.36, Low = 757.5, Close = 760.12, Vol = 1254028 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 6), Open = 757.84, High = 768.42, Low = 756.3, Close = 768.07, Vol = 1257118 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 5), Open = 758.13, High = 762.87, Low = 755.6, Close = 758.57, Vol = 1222147 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 4), Open = 769.51, High = 772.44, Low = 761.78, Close = 765.12, Vol = 1343934 });
            list.Add(new Stock { Date = new DateTime(2016, 4, 1), Open = 757.16, High = 770.04, Low = 755.2, Close = 769.67, Vol = 1581222 });
            list.Add(new Stock { Date = new DateTime(2016, 3, 31), Open = 768.34, High = 769.08, Low = 758.25, Close = 762.9, Vol = 1623821 });
            list.Add(new Stock { Date = new DateTime(2016, 3, 30), Open = 768.21, High = 777.31, Low = 767.58, Close = 768.34, Vol = 2017229 });
            list.Add(new Stock { Date = new DateTime(2016, 3, 29), Open = 753.68, High = 767.18, Low = 748.29, Close = 765.89, Vol = 1987121 });
            list.Add(new Stock { Date = new DateTime(2016, 3, 28), Open = 756.17, High = 758.3, Low = 752.04, Close = 753.28, Vol = 1082515 });
            return list;
        }

        private static IEnumerable<Stock> _stocks2;
        public static IEnumerable<Stock> GetStocks2()
        {
            if (_stocks2 == null)
            {
                _stocks2 = GetStocks2Data();
            }

            return _stocks2;
        }

        // some stock data from Google Financce
        private static IEnumerable<Stock> GetStocks2Data()
        {
            var list = new List<Stock>();
            list.Add(new Stock { Date = new DateTime(2017, 3, 17), Open = 851.61, High = 853.4, Low = 847.11, Close = 852.12, Vol = 1716471 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 16), Open = 849.03, High = 850.85, Low = 846.13, Close = 848.78, Vol = 977560 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 15), Open = 847.59, High = 848.63, Low = 840.77, Close = 847.2, Vol = 1381474 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 14), Open = 843.64, High = 847.24, Low = 840.8, Close = 845.62, Vol = 780198 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 13), Open = 844, High = 848.68, Low = 843.25, Close = 845.54, Vol = 1223647 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 10), Open = 843.28, High = 844.91, Low = 839.5, Close = 843.25, Vol = 1704024 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 9), Open = 836, High = 842, Low = 834.21, Close = 838.68, Vol = 1261517 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 8), Open = 833.51, High = 838.15, Low = 831.79, Close = 835.37, Vol = 989773 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 7), Open = 827.4, High = 833.41, Low = 826.52, Close = 831.91, Vol = 1037630 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 6), Open = 826.95, High = 828.88, Low = 822.4, Close = 827.78, Vol = 1109037 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 3), Open = 830.56, High = 831.36, Low = 825.75, Close = 829.08, Vol = 896378 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 2), Open = 833.85, High = 834.51, Low = 829.64, Close = 830.63, Vol = 942476 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 1), Open = 828.85, High = 836.26, Low = 827.26, Close = 835.24, Vol = 1496540 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 28), Open = 825.61, High = 828.54, Low = 820.2, Close = 823.21, Vol = 2260769 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 27), Open = 824.55, High = 830.5, Low = 824, Close = 829.28, Vol = 1101466 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 24), Open = 827.73, High = 829, Low = 824.2, Close = 828.64, Vol = 1392202 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 23), Open = 830.12, High = 832.46, Low = 822.88, Close = 831.33, Vol = 1472771 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 22), Open = 828.66, High = 833.25, Low = 828.64, Close = 830.76, Vol = 987248 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 21), Open = 828.66, High = 833.45, Low = 828.35, Close = 831.66, Vol = 1262337 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 17), Open = 823.02, High = 828.07, Low = 821.66, Close = 828.07, Vol = 1611039 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 16), Open = 819.93, High = 824.4, Low = 818.98, Close = 824.16, Vol = 1287626 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 15), Open = 819.36, High = 823, Low = 818.47, Close = 818.98, Vol = 1313617 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 14), Open = 819, High = 823, Low = 816, Close = 820.45, Vol = 1054732 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 13), Open = 816, High = 820.96, Low = 815.49, Close = 819.24, Vol = 1213324 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 10), Open = 811.7, High = 815.25, Low = 809.78, Close = 813.67, Vol = 1134976 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 9), Open = 809.51, High = 810.66, Low = 804.54, Close = 809.56, Vol = 990391 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 8), Open = 807, High = 811.84, Low = 803.19, Close = 808.38, Vol = 1155990 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 7), Open = 803.99, High = 810.5, Low = 801.78, Close = 806.97, Vol = 1241221 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 6), Open = 799.7, High = 801.67, Low = 795.25, Close = 801.34, Vol = 1184483 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 3), Open = 802.99, High = 806, Low = 800.37, Close = 801.49, Vol = 1463448 });
            return list;
        }

        private static IEnumerable<Stock> _stocks3;
        public static IEnumerable<Stock> GetStocks3()
        {
            if (_stocks3 == null)
            {
                _stocks3 = GetStocks3Data();
            }

            return _stocks3;
        }

        // some stock data from Google Financce
        private static IEnumerable<Stock> GetStocks3Data()
        {
            var list = new List<Stock>();
            list.Add(new Stock { Date = new DateTime(2017, 3, 15), Open = 867.94, High = 869.88, Low = 861.3, Close = 868.39, Vol = 1332885 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 14), Open = 863.75, High = 867.58, Low = 860.13, Close = 865.91, Vol = 1061692 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 13), Open = 860.83, High = 867.13, Low = 860.82, Close = 864.58, Vol = 1166605 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 10), Open = 862.7, High = 864.23, Low = 857.61, Close = 861.4, Vol = 1336585 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 9), Open = 853.69, High = 860.71, Low = 852.67, Close = 857.84, Vol = 1347697 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 8), Open = 853.12, High = 856.93, Low = 851.25, Close = 853.64, Vol = 1029834 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 7), Open = 847.26, High = 853.33, Low = 845.52, Close = 851.15, Vol = 1038696 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 6), Open = 846.86, High = 848.94, Low = 841.17, Close = 847.27, Vol = 1047872 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 3), Open = 848.94, High = 850.82, Low = 844.7, Close = 849.08, Vol = 1006612 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 2), Open = 856.31, High = 856.49, Low = 848.72, Close = 849.85, Vol = 1250938 });
            list.Add(new Stock { Date = new DateTime(2017, 3, 1), Open = 851.38, High = 858, Low = 849.02, Close = 856.75, Vol = 1818671 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 28), Open = 847.35, High = 848.83, Low = 841.44, Close = 844.93, Vol = 1383119 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 27), Open = 844.95, High = 850.67, Low = 843.01, Close = 849.67, Vol = 1010333 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 24), Open = 847.65, High = 848.36, Low = 842.96, Close = 847.81, Vol = 1346189 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 23), Open = 851.08, High = 852.62, Low = 842.5, Close = 851, Vol = 1386681 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 22), Open = 848, High = 853.79, Low = 846.71, Close = 851.36, Vol = 1224889 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 21), Open = 847.99, High = 852.2, Low = 846.55, Close = 849.27, Vol = 1261062 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 17), Open = 841.31, High = 846.94, Low = 839.78, Close = 846.55, Vol = 1461210 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 16), Open = 838.5, High = 842.69, Low = 837.26, Close = 842.17, Vol = 1005084 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 15), Open = 838.81, High = 841.77, Low = 836.22, Close = 837.32, Vol = 1357185 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 14), Open = 839.77, High = 842, Low = 835.83, Close = 840.03, Vol = 1363345 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 13), Open = 837.7, High = 841.74, Low = 836.25, Close = 838.96, Vol = 1295708 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 10), Open = 832.95, High = 837.15, Low = 830.51, Close = 834.85, Vol = 1415128 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 9), Open = 831.73, High = 831.98, Low = 826.5, Close = 830.06, Vol = 1194238 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 8), Open = 830.53, High = 834.25, Low = 825.11, Close = 829.88, Vol = 1302225 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 7), Open = 825.5, High = 831.92, Low = 823.29, Close = 829.23, Vol = 1666605 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 6), Open = 820.92, High = 822.39, Low = 814.29, Close = 821.62, Vol = 1350875 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 3), Open = 823.13, High = 826.13, Low = 819.35, Close = 820.13, Vol = 1528095 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 2), Open = 815, High = 824.56, Low = 812.05, Close = 818.26, Vol = 1689179 });
            list.Add(new Stock { Date = new DateTime(2017, 2, 1), Open = 824, High = 824, Low = 812.25, Close = 815.24, Vol = 2251047 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 31), Open = 819.5, High = 823.07, Low = 813.4, Close = 820.19, Vol = 2020180 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 30), Open = 837.06, High = 837.23, Low = 821.03, Close = 823.83, Vol = 3516933 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 27), Open = 859, High = 867, Low = 841.9, Close = 845.03, Vol = 3752497 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 26), Open = 859.05, High = 861, Low = 850.52, Close = 856.98, Vol = 3493251 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 25), Open = 853.55, High = 858.79, Low = 849.74, Close = 858.45, Vol = 1662148 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 24), Open = 846.98, High = 851.52, Low = 842.28, Close = 849.53, Vol = 1688375 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 23), Open = 831.61, High = 845.54, Low = 828.7, Close = 844.43, Vol = 2457377 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 20), Open = 829.09, High = 829.24, Low = 824.6, Close = 828.17, Vol = 1306183 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 19), Open = 829, High = 833, Low = 823.96, Close = 824.37, Vol = 1070454 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 18), Open = 829.8, High = 829.81, Low = 824.08, Close = 829.02, Vol = 1027698 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 17), Open = 830, High = 830.18, Low = 823.2, Close = 827.46, Vol = 1440905 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 13), Open = 831, High = 834.65, Low = 829.52, Close = 830.94, Vol = 1290182 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 12), Open = 828.38, High = 830.38, Low = 821.01, Close = 829.53, Vol = 1350308 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 11), Open = 826.62, High = 829.9, Low = 821.47, Close = 829.86, Vol = 1325394 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 10), Open = 827.07, High = 829.41, Low = 823.14, Close = 826.01, Vol = 1197442 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 9), Open = 826.37, High = 830.43, Low = 821.62, Close = 827.18, Vol = 1408924 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 6), Open = 814.99, High = 828.96, Low = 811.5, Close = 825.21, Vol = 2017097 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 5), Open = 807.5, High = 813.74, Low = 805.92, Close = 813.02, Vol = 1340535 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 4), Open = 809.89, High = 813.43, Low = 804.11, Close = 807.77, Vol = 1515339 });
            list.Add(new Stock { Date = new DateTime(2017, 1, 3), Open = 800.62, High = 811.44, Low = 796.89, Close = 808.01, Vol = 1959033 });
            return list;
        }
        #endregion

        #region StageCount
        public class StageCount
        {
            public string Stage { get; set; }
            public int Count { get; set; }
        }

        private static IEnumerable<StageCount> _stages;
        public static IEnumerable<StageCount> GetStages()
        {
            return _stages ?? (_stages = GetStagesData());
        }

        private static IEnumerable<StageCount> GetStagesData()
        {
            var list = new List<StageCount>();
            list.Add(new StageCount { Stage = "Prospects", Count = 750 });
            list.Add(new StageCount { Stage = "Qualified Prospects", Count = 520 });
            list.Add(new StageCount { Stage = "Needs Analysis", Count = 200 });
            list.Add(new StageCount { Stage = "Price Quotes", Count = 150 });
            list.Add(new StageCount { Stage = "Negotiations", Count = 100 });
            list.Add(new StageCount { Stage = "Closed Sales", Count = 90 });
            return list;
        }
        #endregion StageCount

        #region Summary
        public class Summary
        {
            public string Country { get; set; }
            public int Leads { get; set; }
            public int Qualified { get; set; }
            public int Analysis { get; set; }
            public int Quotes { get; set; }
            public int Negotiations { get; set; }
            public int Sales { get; set; }
        }

        private static IEnumerable<Summary> _summaries;
        public static IEnumerable<Summary> GetSummaries()
        {
            return _summaries ?? (_summaries = GetSummariesData());
        }

        private static IEnumerable<Summary> GetSummariesData()
        {
            var rand = new Random(0);
            var list = new List<Summary>();
            var countries = new[] { "US","Canada","Mexico","Japan","Korea","China","Germany","France","UK","Italy","Greece" };
            for(var i = 0; i < countries.Length; i++)
            {
                list.Add(new Summary()
                {
                    Country = countries[i],
                    Leads=rand.Next(100000),
                    Qualified = rand.Next(50000),
                    Analysis = rand.Next(5000),
                    Quotes = rand.Next(4000),
                    Negotiations = rand.Next(1000),
                    Sales = rand.Next(800),
                });
            }
            return list;
        }
        #endregion Summary

        #region Point
        public class Point
        {
            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }

            public double X { get; set; }
            public double Y { get; set; }
        }

        public static IEnumerable<Point> GetPoints(int cnt, int a, int b)
        {
            var rand = new Random(0);
            var list = new List<Point>();
            var x = -5d * cnt / 2d;
            for(int i = 0; i < cnt; i++)
            {
                var rnd = rand.NextDouble() * cnt - cnt / 2d;
                var y = a + x * (b + rnd) + rnd;
                list.Add(new Point(x, y));
                x += 0.5d + rand.NextDouble() * 10;
            }
            return list;
        }
        #endregion Point
    }
}