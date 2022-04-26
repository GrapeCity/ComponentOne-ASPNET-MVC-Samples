using System.Collections.Generic;

namespace TransposedGridExplorer.Models
{
    public class ProductSheet
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
        public string Screen { get; set; }
        public string Camera { get; set; }
        public string OS { get; set; }
        public string CPU { get; set; }
        public int RAM { get; set; }
        public int ROM { get; set; }
        public string SIM { get; set; }
        public double Battery { get; set; }

        public static List<ProductSheet> GetData()
        {
            return new List<ProductSheet>()
            {
                new ProductSheet() {
                    Name = "OPPO A5 (2020) 64GB",
                    Image = "Content/images/ProductSheet/1.jpg",
                    Rating = 4,
                    Price = 199,
                    Screen = "TFT, 6.5\", HD+",
                    Camera = "Main: 12 MP & Others: 8 MP, 2 MP, 2 MP",
                    OS = "Android 9.0 (Pie)",
                    CPU = "Snapdragon 665 8 cores",
                    RAM = 3,
                    ROM = 64,
                    SIM = "2 Nano SIM, Support 4G",
                    Battery = 5000
                },
                new ProductSheet() {
                    Name = "Samsung Galaxy A10s",
                    Image = "Content/images/ProductSheet/2.jpg",
                    Rating = 2,
                    Price = 155,
                    Screen = "IPS TFT, 6.2\", HD+",
                    Camera = "Main: 13 MP & Second: 2 MP",
                    OS = "Android 9.0 (Pie)",
                    CPU = "MediaTek MT6762 8 cores (Helio P22)",
                    RAM = 2,
                    ROM = 32,
                    SIM = "2 Nano SIM, Support 4G",
                    Battery = 4000
                },
                new ProductSheet() {
                    Name = "iPhone 11 64GB",
                    Image = "Content/images/ProductSheet/3.jpg",
                    Rating = 5,
                    Price = 999,
                    Screen = "IPS LCD, 6.1\", Liquid Retina",
                    Camera = "Main: 12 MP & Second: 12 MP",
                    OS = "iOS 13",
                    CPU = "Apple A13 Bionic 6 cores",
                    RAM = 4,
                    ROM = 64,
                    SIM = "1 eSIM & 1 Nano SIM, Support 4G",
                    Battery = 3110
                },
                new ProductSheet() {
                    Name = "Samsung Galaxy Fold",
                    Image = "Content/images/ProductSheet/4.jpg",
                    Rating = 5,
                    Price = 1898,
                    Screen = "Dynamic AMOLED 7.3\" & Super AMOLED 4.6\", QuadHD(2K)",
                    Camera = "Main: 12 MP & Others: 12 MP, 16 MP",
                    OS = "Android 9.0 (Pie)",
                    CPU = "Snapdragon 855 8 cores",
                    RAM = 12,
                    ROM = 512,
                    SIM = "1 eSIM & 1 Nano SIM, Support 4G",
                    Battery = 4380
                },
                new ProductSheet() {
                    Name = "Realme 5 (3GB/64GB)",
                    Image = "Content/images/ProductSheet/5.jpg",
                    Rating = 3.5,
                    Price = 166,
                    Screen = "IPS LCD, 6.5\", HD+",
                    Camera = "Main: 12 MP & Others: 8 MP, 2 MP, 2 MP",
                    OS = "Android 9.0 (Pie)",
                    CPU = "Snapdragon 665 8 cores",
                    RAM = 3,
                    ROM = 64,
                    SIM = "2 Nano SIM, Support 4G",
                    Battery = 5000
                },
                new ProductSheet() {
                    Name = "BlackBerry KEY2",
                    Image = "Content/images/ProductSheet/6.jpg",
                    Rating = 4.5,
                    Price = 711,
                    Screen = "IPS LCD, 4.5\", Full HD",
                    Camera = "Main: 12 MP & Second: 12 MP",
                    OS = "Android 8.1 (Oreo)",
                    CPU = "Snapdragon 660 8 cores",
                    RAM = 6,
                    ROM = 64,
                    SIM = "2 Nano SIM, Support 4G",
                    Battery = 3500
                },
            };
        }
    }
}