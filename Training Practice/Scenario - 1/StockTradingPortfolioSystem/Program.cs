// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStockApp
{
    public class InvalidTradeException : Exception
    {
        public InvalidTradeException(string msg) : base(msg) { }
    }

    public class Investor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Investor(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Stock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }

        private double price;
        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPriceChanged?.Invoke(this, price);
            }
        }

        public event Action<Stock, double> OnPriceChanged;

        public Stock(string symbol, string name, double price)
        {
            Symbol = symbol;
            Name = name;
            this.price = price;
        }
    }

    public class Transaction
    {
        public Investor Investor { get; set; }
        public Stock Stock { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }

        public Transaction(Investor investor, Stock stock, int qty, double price, DateTime date, string type)
        {
            if (date > DateTime.Now)
                throw new InvalidTradeException("Future transaction not allowed");

            Investor = investor;
            Stock = stock;
            Quantity = qty;
            Price = price;
            Date = date;
            Type = type;
        }

        public double Total()
        {
            return Quantity * Price;
        }
    }

    public interface IRiskStrategy
    {
        double CalculateRisk(List<Transaction> transactions);
    }

    public class SimpleRiskStrategy : IRiskStrategy
    {
        public double CalculateRisk(List<Transaction> transactions)
        {
            double total = transactions.Sum(t => t.Total());
            return total * 0.05;
        }
    }

    public class HighRiskStrategy : IRiskStrategy
    {
        public double CalculateRisk(List<Transaction> transactions)
        {
            double total = transactions.Sum(t => t.Total());
            return total * 0.1;
        }
    }

    public class Portfolio
    {
        public Investor Investor { get; set; }
        public List<Transaction> Transactions { get; set; }
        public IRiskStrategy RiskStrategy { get; set; }

        public Portfolio(Investor investor)
        {
            Investor = investor;
            Transactions = new List<Transaction>();
            RiskStrategy = new SimpleRiskStrategy();
        }

        public void AddTransaction(Transaction t)
        {
            if (t.Type == "Sell")
            {
                int owned = Transactions
                    .Where(x => x.Stock.Symbol == t.Stock.Symbol)
                    .Aggregate(0, (total, x) =>
                        x.Type == "Buy" ? total + x.Quantity : total - x.Quantity);

                if (t.Quantity > owned)
                    throw new InvalidTradeException("Cannot sell more than owned");
            }

            Transactions.Add(t);
        }

        public double NetProfit()
        {
            return Transactions.Aggregate(0.0, (total, t) =>
                t.Type == "Sell" ? total + t.Total() : total - t.Total());
        }

        public double CalculateRisk()
        {
            return RiskStrategy.CalculateRisk(Transactions);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Investor> investors = new List<Investor>();
            List<Stock> stocks = new List<Stock>();
            List<Transaction> transactions = new List<Transaction>();
            Dictionary<string, List<Transaction>> transactionMap = new Dictionary<string, List<Transaction>>();

            investors.Add(new Investor(1, "Rahul"));
            investors.Add(new Investor(2, "Ritika"));
            investors.Add(new Investor(3, "Amit"));

            stocks.Add(new Stock("TCS", "TCS Ltd", 3500));
            stocks.Add(new Stock("INFY", "Infosys", 1500));
            stocks.Add(new Stock("HDFC", "HDFC Bank", 1700));

            foreach (var stock in stocks)
            {
                stock.OnPriceChanged += (s, price) =>
                {
                    Console.WriteLine("Price updated for " + s.Symbol + " : " + price);
                };
            }

            stocks[0].Price = 3600;

            Portfolio p1 = new Portfolio(investors[0]);
            Portfolio p2 = new Portfolio(investors[1]);
            Portfolio p3 = new Portfolio(investors[2]);

            try
            {
                var t1 = new Transaction(investors[0], stocks[0], 10, 3500, DateTime.Now.AddDays(-5), "Buy");
                var t2 = new Transaction(investors[0], stocks[0], 5, 3600, DateTime.Now.AddDays(-2), "Sell");
                var t3 = new Transaction(investors[1], stocks[1], 20, 1500, DateTime.Now.AddDays(-3), "Buy");
                var t4 = new Transaction(investors[2], stocks[2], 15, 1700, DateTime.Now.AddDays(-1), "Buy");

                p1.AddTransaction(t1);
                p1.AddTransaction(t2);
                p2.AddTransaction(t3);
                p3.AddTransaction(t4);

                transactions.AddRange(new[] { t1, t2, t3, t4 });

                foreach (var t in transactions)
                {
                    if (!transactionMap.ContainsKey(t.Stock.Symbol))
                        transactionMap[t.Stock.Symbol] = new List<Transaction>();

                    transactionMap[t.Stock.Symbol].Add(t);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            List<Portfolio> portfolios = new List<Portfolio> { p1, p2, p3 };

            Console.WriteLine("Most Profitable Investor:");
            var best = portfolios
                .OrderByDescending(p => p.NetProfit())
                .First();
            Console.WriteLine(best.Investor.Name);

            Console.WriteLine("Stock with highest volume:");
            var highestVolume = transactions
                .GroupBy(t => t.Stock.Symbol)
                .OrderByDescending(g => g.Sum(x => x.Quantity))
                .First();
            Console.WriteLine(highestVolume.Key);

            Console.WriteLine("Transactions grouped by stock:");
            foreach (var kv in transactionMap)
                Console.WriteLine(kv.Key + " - " + kv.Value.Count);

            Console.WriteLine("Net Profit/Loss:");
            foreach (var p in portfolios)
                Console.WriteLine(p.Investor.Name + " : " + p.NetProfit());

            Console.WriteLine("Investors with negative returns:");
            var negative = portfolios.Where(p => p.NetProfit() < 0);
            foreach (var p in negative)
                Console.WriteLine(p.Investor.Name);

            Console.WriteLine("Portfolio Risk:");
            foreach (var p in portfolios)
                Console.WriteLine(p.Investor.Name + " Risk: " + p.CalculateRisk());
        }
    }
}
