using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StockChart.Models
{
    public class StockDataModel
    {
        public string CurrentSymbol { get; set; }
        public string AddSymbol { get; set; }

        public bool NeedReset { get; set; }

        public StockSymbol CurrentStockSymbol { get; set; }
        public List<StockSymbol> StockSymbolList
        {
            get
            {
                return StockDataStatic.StockSymbolListAll.Values.ToList();
            }
        }

        private static List<string> _currentSymbolList;
        public List<string> CurrentSymbolList
        {
            get { return _currentSymbolList; }
        }

        public Dictionary<string, List<StockData>> StockDataDic;

        public Dictionary<string, List<StockEvent>> StockEventDic;

        public List<StockData> IXIC;

        private static List<string> _palettes;
        public List<string> Palettes
        {
            get { return _palettes; }
        }
        private static int PalettesIndex = 1;

        //Constructor for Initialization of object of StockData Class
        public StockDataModel()
        {
            InitialCurrentStockSymbolData();
            IXIC = StockDataStatic.GetStockPricesBySymbol("AMZN");
        }

        public StockDataModel(string currentSymbol)
        {
            _currentSymbolList[0] = currentSymbol;
            InitialCurrentStockSymbolData();
        }

        public void InitialCurrentStockSymbolData()
        {
            if (_currentSymbolList == null)
            {
                _currentSymbolList = new List<string> { "AMZN" };
            }
            if (_palettes == null)
            {
                _palettes = new List<string> { StockDataStatic.Company_Palette[1] };
            }

            StockDataDic = new Dictionary<string, List<StockData>>();
            StockEventDic = new Dictionary<string, List<StockEvent>>();

            for (int i = 0; i < _currentSymbolList.Count; i++)
            {
                StockDataDic[_currentSymbolList[i]] = StockDataStatic.GetStockPricesBySymbol(_currentSymbolList[i]);
                StockEventDic[_currentSymbolList[i]] = StockDataStatic.GetStockEventsBySymbol(_currentSymbolList[i]);
            }
            CurrentStockSymbol = StockDataStatic.StockSymbolListAll[_currentSymbolList[0]];
        }

        public void AddStockDataIntoList(string symbol)
        {
            if (!StockDataStatic.StockSymbolListAll.Keys.Contains(symbol))
            {
                return;
            }
            if (_currentSymbolList.Contains(symbol))
            {
                return;
            }

            _currentSymbolList.Add(symbol);
            if (StockDataDic == null)
            {
                StockDataDic = new Dictionary<string, List<StockData>>();
            }
            if (StockEventDic == null)
            {
                StockEventDic = new Dictionary<string, List<StockEvent>>();
            }
            StockDataDic.Add(symbol, StockDataStatic.GetStockPricesBySymbol(symbol));
            StockEventDic.Add(symbol, StockDataStatic.GetStockEventsBySymbol(symbol));
        }

        public float RangeMin { get; set; }
        public float RangeMax { get; set; }

        public void RefershStockDataModel()
        {
            SetCurrentSymbolStock();

            CurrentSymbol = _currentSymbolList[0];

            AddSymbolStock();

            if (NeedReset)
            {
                _currentSymbolList.Clear();
                _currentSymbolList.Add(CurrentSymbol);
                List<StockData> current = StockDataDic[CurrentSymbol];
                StockDataDic.Clear();
                StockDataDic.Add(CurrentSymbol, current);
                List<StockEvent> currentEvent = StockEventDic[CurrentSymbol];
                StockEventDic.Clear();
                StockEventDic.Add(CurrentSymbol, currentEvent);
            }
            NeedReset = false;
        }

        private void SetCurrentSymbolStock()
        {
            if (!string.IsNullOrEmpty(CurrentSymbol) && CurrentSymbol != _currentSymbolList[0])
            {
                StockDataDic.Remove(_currentSymbolList[0]);
                StockEventDic.Remove(_currentSymbolList[0]);
                int sIndex = _currentSymbolList.IndexOf(CurrentSymbol);
                if (sIndex > -1)
                {
                    _currentSymbolList.RemoveAt(sIndex);
                    _palettes[0] = _palettes[sIndex];
                    _palettes.RemoveAt(sIndex);
                }
                else
                {
                    StockDataDic.Add(CurrentSymbol, StockDataStatic.GetStockPricesBySymbol(CurrentSymbol));
                    StockEventDic.Add(CurrentSymbol, StockDataStatic.GetStockEventsBySymbol(CurrentSymbol));
                    PalettesIndex++;
                    _palettes[0] = StockDataStatic.Company_Palette[PalettesIndex];
                }
                _currentSymbolList[0] = CurrentSymbol;
                CurrentStockSymbol = StockDataStatic.StockSymbolListAll[CurrentSymbol];
            }
        }

        private void AddSymbolStock()
        {
            if (!string.IsNullOrEmpty(AddSymbol))
            {
                if (!_currentSymbolList.Contains(AddSymbol))
                {
                    _currentSymbolList.Add(AddSymbol);
                    StockDataDic.Add(AddSymbol, StockDataStatic.GetStockPricesBySymbol(AddSymbol));
                    StockEventDic.Add(AddSymbol, StockDataStatic.GetStockEventsBySymbol(AddSymbol));
                    PalettesIndex++;
                    _palettes.Add(StockDataStatic.Company_Palette[PalettesIndex]);
                }
                AddSymbol = "";
            }
        }
    }
}