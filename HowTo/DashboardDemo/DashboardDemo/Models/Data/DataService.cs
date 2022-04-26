using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
#if ASPNETCORE
using System.Reflection;
#else
using C1.Web.Mvc;
using System.ComponentModel;
using System.Reflection;
#endif

namespace DashboardModel
{
    public class DataService
    {
        private Model _model;
        private readonly string _dataXmlFolderPath;

        private DateRange _dateRangeFilter;
        private CategoryType _categoryFilter;

        private List<SaleGoalItem> _categorySalesVsGoal;
        private List<SaleGoalItem> _regionSalesVsGoal;
        private List<SaleItem> _dateSaleList;
        private List<CurrentPriorBudgetItem> _budgetItemList;
        private List<Opportunity> _opportunityList;
        private List<BudgetItem> _budgetCollection;
        private List<Sale> _saleCollection;
        private List<Product> _productCollection;
        private List<OpportunityItem> _opportunityCollection;
        private List<RegionWiseSale> _regionWiseSaleCollection;
        private List<RegionSalesGoal> _regionSaleGoalCollection;
        private List<CampainTaskItem> _campainTaskCollection;
        private List<ProductsWiseSaleItem> _productWiseSaleCollection;
        private List<SaleItem> _customerSaleCollection;
        private List<SaleItem> _productSaleCollection;

        private DataService(string dataXmlFolderPath)
        {
            _dataXmlFolderPath = dataXmlFolderPath;
        }

        public static DataService GetService(string dataXmlFolderPath)
        {
            var dataService = new DataService(dataXmlFolderPath);
            dataService.InitializeDataService();
            return dataService;
        }

        #region Comparers
        private class RegionWiseSaleComparer : IComparer<RegionWiseSale>
        {
            public int Compare(RegionWiseSale x, RegionWiseSale y)
            {
                return x.Date.CompareTo(y.Date);
            }
        }

        private class SaleComparer : IComparer<Sale>
        {
            public int Compare(Sale x, Sale y)
            {
                return x.Date.CompareTo(y.Date);
            }
        }

        private class SaleItemComparer : IComparer<SaleItem>
        {
            public int Compare(SaleItem x, SaleItem y)
            {
                return y.Sales.CompareTo(x.Sales);
            }
        }

        #endregion

        public void InitializeDataService()
        {
            VerifyDatabase();

            _budgetCollection = new List<BudgetItem>(_model.Budget.ToList());
            _productCollection = new List<Product>(_model.Products.ToList());
            _saleCollection = new List<Sale>(_model.Sales.ToList());
            _opportunityCollection = new List<OpportunityItem>(_model.Opportunities.ToList());
            _regionWiseSaleCollection = new List<RegionWiseSale>(_model.RegionWiseSales.ToList());
            _regionSaleGoalCollection = new List<RegionSalesGoal>(_model.RegionSales.ToList());
            _regionWiseSaleCollection.Sort(new RegionWiseSaleComparer());
            _saleCollection.Sort(new SaleComparer());

            var dateSaleMap = new Dictionary<DateTime, SaleItem>();
            var productsWiseSaleItems = new List<ProductsWiseSaleItem>();
            var i = 0;
            foreach (Sale sale in _saleCollection)
            {
                ProductsWiseSaleItem productsWiseSale = new ProductsWiseSaleItem();
                productsWiseSale.Date = sale.Date;
                productsWiseSale.Quantity = sale.Quantity;
                productsWiseSale.Sale = sale.Summ;
                productsWiseSale.ID = sale.ProductId;
                var product = sale.GetProduct(_model);
                var category = _model.Categories.First(x => x.Id == product.CategoryId);
                productsWiseSale.Category = category.ToString();
                productsWiseSale.Product = product.Name;
                var customer = _model.Customers.First(x => x.Id == sale.CustomerId);
                var region = _model.Regions.First(x => x.Id == customer.RegionId);
                productsWiseSale.Region = region.ToString();
                productsWiseSaleItems.Add(productsWiseSale);
                if (dateSaleMap.ContainsKey(sale.Date))
                {
                    dateSaleMap[sale.Date].Sales += sale.Summ;
                }
                else
                {
                    dateSaleMap.Add(sale.Date, new SaleItem { Sales = sale.Summ, Date = sale.Date, Id = i++ });
                }
            }

            _dateSaleList = dateSaleMap.Values.ToList();
            _productWiseSaleCollection = new List<ProductsWiseSaleItem>(productsWiseSaleItems);
            var budgetSaleProfitMap = new Dictionary<int, CurrentPriorBudgetItem>();
            foreach (BudgetItem budget in _budgetCollection)
            {
                if (budget.Expences > 0)
                {
                    int month = budget.Date.Month;
                    int year = budget.Date.Year;
                    if (budgetSaleProfitMap.ContainsKey(month))
                    {
                        if (year == 2017)
                        {
                            budgetSaleProfitMap[month].Sales += budget.Sales;
                            budgetSaleProfitMap[month].Profit += budget.Profit;
                        }
                        else
                        {
                            budgetSaleProfitMap[month].ProirSales += budget.Sales;
                            budgetSaleProfitMap[month].ProirProfit += budget.Profit;
                        }
                    }
                    else
                    {
                        if (year == 2017)
                        {
                            budgetSaleProfitMap.Add(month, new CurrentPriorBudgetItem { Date = budget.Date, Sales = budget.Sales, Profit = budget.Profit });
                        }
                        else
                        {
                            budgetSaleProfitMap.Add(month, new CurrentPriorBudgetItem { Date = budget.Date, ProirSales = budget.Sales, ProirProfit = budget.Profit });
                        }
                    }
                }
            }

            _budgetItemList = budgetSaleProfitMap.Values.ToList();

            UpdataDataByDateRange();
        }

        private void VerifyDatabase()
        {
            var initialPath = Path.Combine(_dataXmlFolderPath, "InitialData.xml");
            if (!File.Exists(initialPath))
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream("DashboardDemo.InitialData.xml"))
                {
                    _model = Model.GetPopulated(stream);
                }
            }
            else
            {
                _model = Model.GetPopulated(initialPath);
            }
        }

        internal DateRange fullRange;
        
        public DateRange DateRange
        {
            get { return _dateRangeFilter; }
            set
            {
                if (fullRange == null) fullRange = value;
                _dateRangeFilter = value;
                UpdataDataByDateRange();
            }
        }

        public List<CampainTaskItem> CampainTaskCollction { get { return _campainTaskCollection; } }

        public List<ProductsWiseSaleItem> ProductWiseSaleCollection { get { return _productWiseSaleCollection; } }

        public List<CurrentPriorBudgetItem> BudgetItemList { get { return _budgetItemList; } }

        public List<SaleItem> DateSaleList { get { return _dateSaleList; } }

        public List<Opportunity> OpportunityItemList { get { return _opportunityList; } }
        
        public List<RegionSaleItem> GetRegionWiseSales()
        {
            var regionSaleMap = new Dictionary<int, RegionSaleItem>();
            foreach (var regionWiseSale in _regionWiseSaleCollection)
            {
                if (regionSaleMap.ContainsKey(regionWiseSale.RegionId))
                {
                    regionSaleMap[regionWiseSale.RegionId].Sales += regionWiseSale.Sales;
                    regionSaleMap[regionWiseSale.RegionId].Profit += regionWiseSale.Profit;
                }
                else
                {
                    regionSaleMap.Add(regionWiseSale.RegionId, new RegionSaleItem { Id = regionWiseSale.RegionId, Locat = new Point((int)regionWiseSale.X, (int)regionWiseSale.Y), Sales = regionWiseSale.Sales, Profit = regionWiseSale.Profit, Name = regionWiseSale.Region });
                }
            }
            return regionSaleMap.Values.ToList();
        }

        private struct CategoryRegionKey
        {
            public int CategoryId;
            public int RegionId;

            public CategoryRegionKey(int categoryId, int regionId)
            {
                CategoryId = categoryId;
                RegionId = regionId;
            }
        }

        private class CategoryRegionComparer : IEqualityComparer<CategoryRegionKey>
        {
            public bool Equals(CategoryRegionKey x, CategoryRegionKey y)
            {
                return x.CategoryId == y.CategoryId && x.RegionId == y.RegionId;
            }

            public int GetHashCode(CategoryRegionKey obj)
            {
                return obj.CategoryId;
            }
        }

        public List<ProductsWiseSaleItem> GetCategoryRegionWiseSales()
        {
            var categoryRegionSaleMap = new Dictionary<CategoryRegionKey, ProductsWiseSaleItem>(new CategoryRegionComparer());
            foreach (var s in _saleCollection)
            {
                var product = s.GetProduct(_model);
                var customer = s.GetCustomer(_model);
                var key = new CategoryRegionKey(product.CategoryId, customer.RegionId);
                if (categoryRegionSaleMap.ContainsKey(key))
                {
                    var si = categoryRegionSaleMap[key];
                    si.Sale += s.Cost;
                    si.Quantity += s.Quantity;
                }
                else
                {
                    var category = product.GetCategory(_model);
                    var region = customer.GetRegion(_model);
                    categoryRegionSaleMap.Add(key, new ProductsWiseSaleItem
                    {
                        Category = category.Name,
                        Region = region.Name,
                        Sale = s.Cost,
                        Quantity = s.Quantity
                    });
                }
            }

            return categoryRegionSaleMap.Values.ToList();
        }

        public List<ProductItem> GetProductItemList(CategoryType filter)
        {
            _categoryFilter = filter;
            _productCollection = _model.Products.ToList().FindAll(new Predicate<object>(FilterByCategoryType));
            var list = new List<ProductItem>();
            foreach (Product product in _productCollection)
            {
                list.Add(new ProductItem(_model, product));
            }

            return list;
        }

        public List<SaleItem> GetTopSaleProductList(int top)
        {
            var item = new List<SaleItem>();
            foreach (var productSale in _productSaleCollection)
            {
                item.Add(productSale);
                if (item.Count == top)
                {
                    break;
                }
            }

            return item;
        }

        public List<SaleItem> GetTopSaleCustomerList(int top)
        {
            var item = new List<SaleItem>();
            foreach (var customerSale in _customerSaleCollection)
            {
                item.Add(customerSale);
                if (item.Count == top)
                {
                    break;
                }
            }

            return item;
        }

        public List<SaleGoalItem> CategorySalesVsGoal { get { return _categorySalesVsGoal; } }
        public List<SaleGoalItem> RegionSalesVsGoal { get { return _regionSalesVsGoal; } }

        private void UpdataDataByDateRange()
        {
            _saleCollection = _model.Sales.ToList().FindAll(new Predicate<object>(FilterByDateRange));
            _saleCollection.Sort(new SaleComparer());
            _budgetCollection = _model.Budget.ToList().FindAll(new Predicate<object>(FilterByDateRange));
            _opportunityCollection = _model.Opportunities.ToList().FindAll(new Predicate<object>(FilterByDateRange));
            _regionWiseSaleCollection = _model.RegionWiseSales.ToList().FindAll(new Predicate<object>(FilterByDateRange));
            _regionSaleGoalCollection = _model.RegionSales.ToList().FindAll(new Predicate<object>(FilterByDateRange));
            _opportunityList = new List<Opportunity>();
            foreach (OpportunityLevel value in Enum.GetValues(typeof(OpportunityLevel)))
            {
                _opportunityList.Add(new Opportunity { Level = value, Sales = 0 });
            }

            var campainTaskItems = new List<CampainTaskItem>();
            var productSaleMap = new Dictionary<int, SaleItem>();
            var customerSaleMap = new Dictionary<int, SaleItem>();
            foreach (var sale in _saleCollection)
            {
                var profit = FindProfitFormCurrentSale(_model, sale);
                var campain = _model.Campaigns.First(x => x.Id == sale.CampaignId);
                if (campain.Name != "None")
                {
                    campainTaskItems.Add(new CampainTaskItem(_model, sale));
                }

                if (customerSaleMap.ContainsKey(sale.CustomerId))
                {
                    customerSaleMap[sale.CustomerId].Sales += sale.Summ;
                    customerSaleMap[sale.CustomerId].Profit += profit;
                }
                else
                {
                    var customer = sale.GetCustomer(_model);
                    customerSaleMap.Add(sale.CustomerId, new SaleItem { Id = sale.CustomerId, Sales = sale.Summ, Profit = profit, Name = customer.ToString(), Date = sale.Date });
                }

                if (productSaleMap.ContainsKey(sale.ProductId))
                {
                    productSaleMap[sale.ProductId].Sales += sale.Summ;
                    productSaleMap[sale.ProductId].Profit += profit;
                }
                else
                {
                    var product = sale.GetProduct(_model);
                    productSaleMap.Add(sale.ProductId, new SaleItem { Id = sale.ProductId, Sales = sale.Summ, Profit = profit, Name = product.ToString(), Date = sale.Date });
                }
            }

            var regionSaleMap = new Dictionary<int, SaleGoalItem>();
            var categorySaleMap = new Dictionary<int, SaleGoalItem>();
            foreach (var budget in _budgetCollection)
            {
                if (categorySaleMap.ContainsKey(budget.CategoryId))
                {
                    categorySaleMap[budget.CategoryId].Sales += budget.Sales;
                    categorySaleMap[budget.CategoryId].Profit += budget.Profit;
                    categorySaleMap[budget.CategoryId].Goal += budget.Goal;
                }
                else
                {
                    var item = new SaleGoalItem();
                    var category = _model.Categories.First(x => x.Id == budget.CategoryId);
                    item.Name = category.ToString();
                    item.Goal = budget.Goal;
                    item.Sales = budget.Sales;
                    item.Profit = budget.Profit;
                    categorySaleMap.Add(budget.CategoryId, item);
                }
            }

            foreach (var region in _regionSaleGoalCollection)
            {
                if (regionSaleMap.ContainsKey(region.RegionId))
                {
                    regionSaleMap[region.RegionId].Sales += region.Sales;
                    regionSaleMap[region.RegionId].Profit += region.Profit;
                    regionSaleMap[region.RegionId].Goal += region.Goal;
                }
                else
                {
                    var item = new SaleGoalItem();
                    var objRegion = _model.Regions.First(x => x.Id == region.RegionId);
                    item.Name = objRegion.ToString();
                    item.Goal = region.Goal;
                    item.Sales = region.Sales;
                    item.Profit = region.Profit;
                    regionSaleMap.Add(region.RegionId, item);
                }
            }

            _campainTaskCollection = new List<CampainTaskItem>(campainTaskItems);
            _productSaleCollection = new List<SaleItem>(productSaleMap.Values.ToList());
            _productSaleCollection.Sort(new SaleItemComparer());
            _customerSaleCollection = new List<SaleItem>(customerSaleMap.Values.ToList());
            _customerSaleCollection.Sort(new SaleItemComparer());
            _regionSalesVsGoal = regionSaleMap.Values.ToList();
            _categorySalesVsGoal = categorySaleMap.Values.ToList();
            foreach (var opportunity in _opportunityCollection)
            {
                OpportunityItemList[opportunity.LevelId].Sales += opportunity.Sales;
            }
        }

        private bool FilterByCategoryType(object product)
        {
            var data = product as Product;
            if (data.Name == "None")
            {
                return false;
            }

            return _categoryFilter == CategoryType.All ? true : data.CategoryId == (int)_categoryFilter;
        }

        private bool FilterByDateRange(object item)
        {
            if (_dateRangeFilter == null)
            {
                return true;
            }

            var date = DateTime.MinValue;
            if (item is Sale)
            {
                date = (item as Sale).Date;
            }
            else if (item is BudgetItem)
            {
                date = (item as BudgetItem).Date;
            }
            else if (item is RegionWiseSale)
            {
                date = (item as RegionWiseSale).Date;
            }
            else if (item is RegionSalesGoal)
            {
                date = (item as RegionSalesGoal).Date;
            }
            else
            {
                date = (item as OpportunityItem).Date;
            }

            return date >= _dateRangeFilter.Start && date <= _dateRangeFilter.End;
        }

        private double FindProfitFormCurrentSale(Model model, Sale sale)
        {
            foreach (var budget in _budgetCollection)
            {
                var product = sale.GetProduct(model);
                if (budget.Date.Year == sale.Date.Year 
                    && budget.Date.Month == sale.Date.Month 
                    && budget.CategoryId == product.CategoryId)
                {
                    double precent = sale.Summ / budget.Sales;
                    return budget.Profit * precent;
                }
            }
            return 0;
        }

        public IEnumerable<CampainTaskItem> GetTaskItem(CampainTaskType? taskType = CampainTaskType.All)
        {
            return CampainTaskCollction.FindAll((campainTask) =>
            {
                if (campainTask == null)
                {
                    return false;
                }

                switch (taskType)
                {
                    case CampainTaskType.Completed:
                        return campainTask.Complete == 1.0;
                    case CampainTaskType.InProgress:
                        return (campainTask.Complete != 1.0 && campainTask.Complete > 0);
                    case CampainTaskType.Deferred:
                        return campainTask.Deferred;
                    case CampainTaskType.Urgent:
                        return campainTask.Urgent;
                }

                return true;
            });
        }
    }
}
