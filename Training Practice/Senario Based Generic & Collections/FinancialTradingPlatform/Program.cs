// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;
using System.Collections.Generic;
using System.Linq;

public interface IFinancialInstrument
{
    string Symbol { get; }
    decimal CurrentPrice { get; }
    InstrumentType Type { get; }
}

public enum InstrumentType { Stock, Bond, Option, Future }

// 1. Generic portfolio
public class Portfolio<T> where T : IFinancialInstrument
{
    private Dictionary<T, int> _holdings = new(); // Instrument -> Quantity
    
    // TODO: Buy instrument
    public void Buy(T instrument, int quantity, decimal price)
    {
        // Validate: quantity > 0, price > 0
        if (quantity <= 0 || price <= 0)
        {
            Console.WriteLine("Invalid buy order.");
            return;
        }

        if (_holdings.ContainsKey(instrument))
            _holdings[instrument] += quantity;
        else
            _holdings[instrument] = quantity;

        Console.WriteLine($"Bought {quantity} of {instrument.Symbol} at {price}");
    }
    
    // TODO: Sell instrument
    public decimal? Sell(T instrument, int quantity, decimal currentPrice)
    {
        if (!_holdings.ContainsKey(instrument) || _holdings[instrument] < quantity)
        {
            Console.WriteLine("Not enough quantity to sell.");
            return null;
        }

        _holdings[instrument] -= quantity;

        if (_holdings[instrument] == 0)
            _holdings.Remove(instrument);

        decimal proceeds = quantity * currentPrice;
        Console.WriteLine($"Sold {quantity} of {instrument.Symbol} at {currentPrice}");
        return proceeds;
    }
    
    // TODO: Calculate total value
    public decimal CalculateTotalValue()
    {
        decimal total = 0;
        foreach (var item in _holdings)
        {
            total += item.Key.CurrentPrice * item.Value;
        }
        return total;
    }
    
    // TODO: Get top performing instrument
    public (T instrument, decimal returnPercentage)? GetTopPerformer(
        Dictionary<T, decimal> purchasePrices)
    {
        if (!_holdings.Any())
            return null;

        T bestInstrument = default;
        decimal bestReturn = decimal.MinValue;

        foreach (var holding in _holdings)
        {
            if (!purchasePrices.ContainsKey(holding.Key))
                continue;

            decimal buyPrice = purchasePrices[holding.Key];
            decimal current = holding.Key.CurrentPrice;

            decimal returnPercent = ((current - buyPrice) / buyPrice) * 100;

            if (returnPercent > bestReturn)
            {
                bestReturn = returnPercent;
                bestInstrument = holding.Key;
            }
        }

        if (bestInstrument == null)
            return null;

        return (bestInstrument, bestReturn);
    }

    public IEnumerable<T> GetInstruments()
    {
        return _holdings.Keys;
    }
}

// 2. Specialized instruments
public class Stock : IFinancialInstrument
{
    public string Symbol { get; set; }
    public decimal CurrentPrice { get; set; }
    public InstrumentType Type => InstrumentType.Stock;
    public string CompanyName { get; set; }
    public decimal DividendYield { get; set; }
}

public class Bond : IFinancialInstrument
{
    public string Symbol { get; set; }
    public decimal CurrentPrice { get; set; }
    public InstrumentType Type => InstrumentType.Bond;
    public DateTime MaturityDate { get; set; }
    public decimal CouponRate { get; set; }
}

// 3. Generic trading strategy
public class TradingStrategy<T> where T : IFinancialInstrument
{
    // TODO: Execute strategy on portfolio
    public void Execute(Portfolio<T> portfolio, 
        IEnumerable<T> marketData,
        Func<T, bool> buyCondition,
        Func<T, bool> sellCondition)
    {
        foreach (var instrument in marketData)
        {
            if (buyCondition(instrument))
                portfolio.Buy(instrument, 10, instrument.CurrentPrice);

            if (sellCondition(instrument))
                portfolio.Sell(instrument, 5, instrument.CurrentPrice);
        }
    }
    
    // TODO: Calculate risk metrics
    public Dictionary<string, decimal> CalculateRiskMetrics(IEnumerable<T> instruments)
    {
        var prices = instruments.Select(i => i.CurrentPrice).ToList();

        if (!prices.Any())
            return new Dictionary<string, decimal>();

        decimal avg = prices.Average();
        decimal variance = prices.Sum(p => (p - avg) * (p - avg)) / prices.Count;
        decimal volatility = (decimal)Math.Sqrt((double)variance);

        return new Dictionary<string, decimal>
        {
            { "Volatility", volatility },
            { "Beta", 1.0m }, // simplified
            { "SharpeRatio", avg != 0 ? avg / volatility : 0 }
        };
    }
}

// 4. Price history with generics
public class PriceHistory<T> where T : IFinancialInstrument
{
    private Dictionary<T, List<(DateTime, decimal)>> _history = new();
    
    // TODO: Add price point
    public void AddPrice(T instrument, DateTime timestamp, decimal price)
    {
        if (!_history.ContainsKey(instrument))
            _history[instrument] = new List<(DateTime, decimal)>();

        _history[instrument].Add((timestamp, price));
    }
    
    // TODO: Get moving average
    public decimal? GetMovingAverage(T instrument, int days)
    {
        if (!_history.ContainsKey(instrument))
            return null;

        var prices = _history[instrument]
            .OrderByDescending(p => p.Item1)
            .Take(days)
            .Select(p => p.Item2);

        if (!prices.Any())
            return null;

        return prices.Average();
    }
    
    // TODO: Detect trends
    public Trend DetectTrend(T instrument, int period)
    {
        if (!_history.ContainsKey(instrument))
            return Trend.Sideways;

        var prices = _history[instrument]
            .OrderByDescending(p => p.Item1)
            .Take(period)
            .Select(p => p.Item2)
            .ToList();

        if (prices.Count < 2)
            return Trend.Sideways;

        if (prices.First() > prices.Last())
            return Trend.Upward;
        else if (prices.First() < prices.Last())
            return Trend.Downward;
        else
            return Trend.Sideways;
    }
}

public enum Trend { Upward, Downward, Sideways }

// 5. TEST SCENARIO: Trading simulation
// a) Create portfolio with mixed instruments
// b) Implement buy/sell logic
// c) Create trading strategy with lambda conditions
// d) Track price history
// e) Demonstrate:
//    - Portfolio rebalancing
//    - Risk calculation
//    - Trend detection
//    - Performance comparison

class Program
{
    static void Main()
    {
        var stock1 = new Stock { Symbol = "AAPL", CompanyName = "Apple", CurrentPrice = 150 };
        var stock2 = new Stock { Symbol = "TSLA", CompanyName = "Tesla", CurrentPrice = 250 };
        var bond1 = new Bond { Symbol = "GOVT10Y", CurrentPrice = 1000, MaturityDate = DateTime.Now.AddYears(10), CouponRate = 5 };

        var portfolio = new Portfolio<IFinancialInstrument>();

        // Buy some instruments
        portfolio.Buy(stock1, 20, stock1.CurrentPrice);
        portfolio.Buy(stock2, 15, stock2.CurrentPrice);
        portfolio.Buy(bond1, 5, bond1.CurrentPrice);

        Console.WriteLine("\nTotal Portfolio Value: " + portfolio.CalculateTotalValue());

        // Trading strategy
        var strategy = new TradingStrategy<IFinancialInstrument>();

        strategy.Execute(
            portfolio,
            new List<IFinancialInstrument> { stock1, stock2, bond1 },
            buyCondition: i => i.CurrentPrice < 200,
            sellCondition: i => i.CurrentPrice > 240
        );

        Console.WriteLine("\nAfter Strategy Execution:");
        Console.WriteLine("Total Value: " + portfolio.CalculateTotalValue());

        // Price history
        var history = new PriceHistory<IFinancialInstrument>();
        history.AddPrice(stock1, DateTime.Now.AddDays(-3), 140);
        history.AddPrice(stock1, DateTime.Now.AddDays(-2), 145);
        history.AddPrice(stock1, DateTime.Now.AddDays(-1), 150);

        Console.WriteLine("\nMoving Average (3 days): " + history.GetMovingAverage(stock1, 3));
        Console.WriteLine("Trend: " + history.DetectTrend(stock1, 3));

        // Risk metrics
        var risk = strategy.CalculateRiskMetrics(new List<IFinancialInstrument> { stock1, stock2, bond1 });
        Console.WriteLine("\nRisk Metrics:");
        foreach (var r in risk)
        {
            Console.WriteLine($"{r.Key}: {r.Value:F2}");
        }

        // Performance comparison
        var purchasePrices = new Dictionary<IFinancialInstrument, decimal>
        {
            { stock1, 120 },
            { stock2, 200 },
            { bond1, 950 }
        };

        var top = portfolio.GetTopPerformer(purchasePrices);
        if (top != null)
            Console.WriteLine($"\nTop Performer: {top?.instrument.Symbol} ({top?.returnPercentage:F2}%)");

        Console.WriteLine("\nSimulation Complete.");
    }
}
