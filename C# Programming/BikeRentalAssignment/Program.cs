// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;
using System.Collections.Generic;

public class Bike
{
    public string Model { get; set; }
    public int PricePerDay { get; set; }
    public string Brand { get; set; }

    public static SortedDictionary<int, Bike> bikeDetails = new SortedDictionary<int, Bike>();
}

public class BikeUtility
{
    public void AddBikeDetails (string model, string brand, int pricePerDay)
    {
        Bike bike = new Bike
        {
            Model = model,
            Brand = brand,
            PricePerDay = pricePerDay
        };
        Bike.bikeDetails.Add(Bike.bikeDetails.Count + 1, bike);
    }

    public SortedDictionary<string, List<Bike>> GroupBikesByBrand()
    {
        SortedDictionary<string, List<Bike>> ansDict = new SortedDictionary<string, List<Bike>>();

        foreach (var elem in Bike.bikeDetails)
        {
            if (!ansDict.ContainsKey(elem.Value.Brand))
            {
                ansDict[elem.Value.Brand] = new List<Bike>();
            }
            ansDict[elem.Value.Brand].Add(elem.Value);
        }

        return ansDict;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BikeUtility bikeUtility = new BikeUtility();

        bikeUtility.AddBikeDetails("Model1", "BrandA", 100);
        bikeUtility.AddBikeDetails("Model2", "BrandB", 150);
        bikeUtility.AddBikeDetails("Model3", "BrandA", 120);
        bikeUtility.AddBikeDetails("Model5", "BrandA", 150);

        SortedDictionary<string, List<Bike>> groupedBikes = bikeUtility.GroupBikesByBrand();

        foreach (var brand in groupedBikes)
        {
            Console.WriteLine($"Brand: {brand.Key}");
            foreach (var bike in brand.Value)
            {
                Console.WriteLine($"  Model: {bike.Model}, Price Per Day: {bike.PricePerDay}");
            }
        }
    }
}