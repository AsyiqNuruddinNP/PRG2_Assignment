﻿//==========================================================
// Student Number : S10257702
// Student Name : Jia Xiang
// Partner Name : Asyiq Nuruddin
//==========================================================
using System;
using PRG2_Assignment_IceCream;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using PRG2_Assignment;
using PRG2_Assignment_Cone;
using PRG2_Assignment_Cup;
using PRG2_Assignment_Customer;
using PRG2_Assignment_Flavour;
using PRG2_Assignment_Topping;

namespace PRG2_Assignment_Order
{
    internal class Order
    {

        public int id { get; set; }
        public DateTime timeRecieved { get; set; }
        public DateTime? timeFulfilled { get; set; }
        public List<IceCream> IceCreamlist
        { get; set; }
        public Order()
        {
            IceCreamlist = new List<IceCream>();
        }
        public Order(int Id, DateTime tR)
        {
            id = Id;
            timeRecieved = tR;
            IceCreamlist = new List<IceCream>();

        }
        public void Modifyicecream(int id)
        {
            try
            {
                IceCream result = IceCreamlist[id - 1];

                Dictionary<int, Flavour> DictFlavour = new Dictionary<int, Flavour>();
                Dictionary<int, Topping> DictTopping = new Dictionary<int, Topping>();
                InitFlavours("flavours.csv", DictFlavour);
                InitToppings("toppings.csv", DictTopping);
                
                void DisplayFlavours(Dictionary<int, Flavour> df)
                {
                    foreach (var v in df)
                    {
                        Console.WriteLine($"[{v.Key}]: {v.Value.Type,-10}");
                    }
                }
                void DisplayToppings(Dictionary<int, Topping> dt)
                {
                    foreach (var v in dt)
                    {
                        Console.WriteLine($"[{v.Key}]: {v.Value.Type,-10}");
                    }
                }
                void InitFlavours(string txtfile, Dictionary<int, Flavour> df)
                {
                    using (StreamReader sr = new StreamReader(txtfile))
                    {
                        List<string> headers = new List<string>();
                        List<string> rowList = new List<string>();
                        int count = 1;

                        string? s = sr.ReadLine();
                        if (s != null)
                        {
                            headers = s.Split(",").ToList();
                        }
                        while ((s = sr.ReadLine()) != null)
                        {
                            rowList = s.Split(',').ToList();

                            Flavour flav;
                            if (Convert.ToInt32(rowList[1]) > 0)
                            {
                                flav = new Flavour(rowList[0], true, 1);
                            }
                            else
                            {
                                flav = new Flavour(rowList[0], false, 1);
                            }
                            df.Add(count, flav);
                            count++;
                        }
                    }
                }

                void InitToppings(string txtfile, Dictionary<int, Topping> dt)
                {
                    using (StreamReader sr = new StreamReader(txtfile))
                    {
                        List<string> headers = new List<string>();
                        List<string> rowList = new List<string>();
                        int count = 1;
                        string? s = sr.ReadLine();
                        if (s != null)
                        {
                            headers = s.Split(",").ToList();
                        }
                        while ((s = sr.ReadLine()) != null)
                        {
                            rowList = s.Split(',').ToList();

                            dt.Add(count, new Topping(rowList[0]));
                            count++;
                        }
                    }
                }

                void initwaffle(Dictionary<int, string> wafflelist)
                {
                    wafflelist.Add(1, "regular");
                    wafflelist.Add(2, "Red velvet");
                    wafflelist.Add(3, "charcoal");
                    wafflelist.Add(4, "pandan waffle");




                }
                void displaywaffle(Dictionary<int, string> wafflelist)
                {
                    foreach (var kpv in wafflelist)
                    {
                        Console.WriteLine($"[{kpv.Key}]: {kpv.Value,-10}");

                    }

                }
                void modifydiplay()
                {
                    Console.WriteLine("[1] change type");
                    Console.WriteLine("[2] cahnge number of scoop");
                    Console.WriteLine("[3] cahnge flavour of scoop");
                    Console.WriteLine("[4] change toppings");
                }
                
                    Dictionary<int, string> wafflelist = new Dictionary<int, string>();
                    initwaffle(wafflelist);
                    List<Flavour> flavlist = new List<Flavour>();
                    IceCream modifiedice = null;
                    if (result is Cone)
                    {
                        modifydiplay();
                        Console.WriteLine("[5] cone dipped option");
                        Console.Write("Enter an option: ");
                        string option = Console.ReadLine();
                        if (option == "1")
                        {
                            Console.Write("Enter new type(waffle or cup): ");
                            string newtype = Console.ReadLine();
                            if (newtype == "cup")
                            {
                                modifiedice = new Cup("cup", result.Scoops, result.Flavours, result.Toppings);
                                IceCreamlist[id - 1] = modifiedice;

                            }
                            else if (newtype == "waffle")
                            {
                                displaywaffle(wafflelist);
                                Console.Write("Enter waffle flavour: ");
                                int wafflenum = Convert.ToInt32(Console.ReadLine());
                                string waffeflav = wafflelist[wafflenum];
                                modifiedice = new Waffle("waffle", result.Scoops, result.Flavours, result.Toppings, waffeflav);
                                IceCreamlist[id - 1] = modifiedice;
                            }
                            else { Console.WriteLine("invalid input."); }



                        }
                        else if (option == "2")
                        {
                            Console.Write("Enter number of scoops: ");


                            int newscp = Convert.ToInt16(Console.ReadLine());

                            DisplayFlavours(DictFlavour);
                            for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
                            {
                                Console.Write($"Enter flavour number {scoopIndex} : ");
                                int newflav = Convert.ToInt32(Console.ReadLine());
                                Flavour addflact = DictFlavour[newflav];
                                if (addflact != null)
                                {
                                    flavlist.Add(addflact);
                                }
                                else
                                {
                                    foreach (var flav in flavlist)
                                    {
                                        if (flav.Type == addflact.Type)
                                        {
                                            flav.Quantity += 1;


                                        }


                                    }







                                }



                            }
                            result.Flavours = flavlist;
                        }
                        else if (option == "3")
                        {
                            flavlist.Clear();
                            int count = 1;
                            foreach (var flavour in result.Flavours)
                            {
                                Console.Write($"Enter flavour number {count} : ");
                                count++;
                                int newflav = Convert.ToInt32(Console.ReadLine());
                                Flavour addflact = DictFlavour[newflav];
                                if (addflact != null)
                                {
                                    flavlist.Add(addflact);
                                }
                                else
                                {
                                    foreach (var flav in flavlist)
                                    {
                                        if (flav.Type == addflact.Type)
                                        {
                                            flav.Quantity += 1;


                                        }


                                    }







                                }


                            }
                            result.Flavours = flavlist;

                        }
                        else if (option == "4")
                        {
                            List<Topping> toplist = new List<Topping>();
                            Console.Write("Enter number of toppings: ");


                            int newtop = Convert.ToInt16(Console.ReadLine());
                            DisplayToppings(DictTopping);
                            for (int topIndex = 1; topIndex <= newtop; topIndex++)
                            {
                                Console.Write($"Enter toping number {topIndex} : ");
                                int addtop = Convert.ToInt32(Console.ReadLine());
                                Topping toppingtolist = DictTopping[addtop];
                                toplist.Add(toppingtolist);


                            }
                            result.Toppings = toplist;
                        }
                        else if (option == "5")
                        {
                            Console.Write("Do you want your cone dipped?(Y/N):");
                            string dipped = Console.ReadLine();
                            if (dipped.ToLower() == "y")
                            {
                                modifiedice = new Cone("cone", result.Scoops, result.Flavours, result.Toppings, true);
                                IceCreamlist[id - 1] = modifiedice;



                            }
                            else if (dipped.ToLower() == "n")
                            {
                                modifiedice = new Cone("cone", result.Scoops, result.Flavours, result.Toppings, false);
                                IceCreamlist[id - 1] = modifiedice;



                            }
                            else { Console.WriteLine("invalid input"); }


                        }


                    }
                    else if (result is Waffle)
                    {
                        modifydiplay();
                        Console.WriteLine("[5] change waffle flavour");
                        Console.Write("Enter an option: ");
                        string option = Console.ReadLine();
                        if (option == "1")
                        {
                            Console.Write("Enter new type(Cone or cup): ");
                            string newtype = Console.ReadLine();
                            if (newtype == "cone")
                            {
                                Console.Write("Do you want your cone dipped?(Y/N):");
                                string dipped = Console.ReadLine();
                                if (dipped.ToLower() == "y")
                                {
                                    modifiedice = new Cone("cone", result.Scoops, result.Flavours, result.Toppings, true);



                                }
                                else if (dipped.ToLower() == "n")
                                {
                                    modifiedice = new Cone("cone", result.Scoops, result.Flavours, result.Toppings, false);



                                }
                            }
                            else if (newtype == "cup")
                            {
                                modifiedice = new Cup("cup", result.Scoops, result.Flavours, result.Toppings);

                            }
                            IceCreamlist[id - 1] = modifiedice;
                        }
                        else if (option == "2")
                        {
                            Console.Write("Enter number of scoops: ");


                            int newscp = Convert.ToInt16(Console.ReadLine());

                            DisplayFlavours(DictFlavour);
                            for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
                            {
                                Console.Write($"Enter flavour number {scoopIndex} : ");
                                int newflav = Convert.ToInt32(Console.ReadLine());
                                Flavour addflact = DictFlavour[newflav];
                                if (addflact != null)
                                {
                                    flavlist.Add(addflact);
                                }
                                else
                                {
                                    foreach (var flav in flavlist)
                                    {
                                        if (flav.Type == addflact.Type)
                                        {
                                            flav.Quantity += 1;


                                        }


                                    }







                                }



                            }
                            result.Flavours = flavlist;
                        }
                        else if (option == "3")
                        {
                            flavlist.Clear();
                            int count = 1;
                            foreach (var flavour in result.Flavours)
                            {
                                Console.Write($"Enter flavour number {count} : ");
                                count++;
                                int newflav = Convert.ToInt32(Console.ReadLine());
                                Flavour addflact = DictFlavour[newflav];
                                if (addflact != null)
                                {
                                    flavlist.Add(addflact);
                                }
                                else
                                {
                                    foreach (var flav in flavlist)
                                    {
                                        if (flav.Type == addflact.Type)
                                        {
                                            flav.Quantity += 1;


                                        }


                                    }







                                }


                            }
                            result.Flavours = flavlist;

                        }
                        else if (option == "4")
                        {
                            List<Topping> toplist = new List<Topping>();
                            Console.Write("Enter number of toppings: ");


                            int newtop = Convert.ToInt16(Console.ReadLine());
                            DisplayToppings(DictTopping);
                            for (int topIndex = 1; topIndex <= newtop; topIndex++)
                            {
                                Console.Write($"Enter toping number {topIndex} : ");
                                int addtop = Convert.ToInt32(Console.ReadLine());
                                Topping toppingtolist = DictTopping[addtop];
                                toplist.Add(toppingtolist);


                            }
                            result.Toppings = toplist;
                        }
                        else if (option == "5")
                        {
                            displaywaffle(wafflelist);
                            Console.Write("Enter waffle flavour: ");
                            int wafflenum = Convert.ToInt32(Console.ReadLine());
                            string waffeflav = wafflelist[wafflenum];
                            modifiedice = new Waffle("waffle", result.Scoops, result.Flavours, result.Toppings, waffeflav);
                            IceCreamlist[id - 1] = modifiedice;

                        }

                    }
                    else if (result is Cup)
                    {
                        modifydiplay();
                        Console.Write("Enter an option: ");
                        string option = Console.ReadLine();
                        if (option == "1")
                        {
                            Console.Write("Enter new type(Cone or Waffle): ");
                            string newtype = Console.ReadLine();
                            if (newtype == "cone")
                            {
                                Console.Write("Do you want your cone dipped?(Y/N):");
                                string dipped = Console.ReadLine();
                                if (dipped.ToLower() == "y")
                                {
                                    modifiedice = new Cone("cone", result.Scoops, result.Flavours, result.Toppings, true);



                                }
                                else if (dipped.ToLower() == "n")
                                {
                                    modifiedice = new Cone("cone", result.Scoops, result.Flavours, result.Toppings, false);



                                }
                            }
                            else if (newtype == "waffle")
                            {
                                displaywaffle(wafflelist);
                                Console.Write("Enter waffle flavour: ");
                                int wafflenum = Convert.ToInt32(Console.ReadLine());
                                string waffeflav = wafflelist[wafflenum];
                                modifiedice = new Waffle("waffle", result.Scoops, result.Flavours, result.Toppings, waffeflav);
                            }
                            else {
                                Console.WriteLine("invalid input");

                            }
                            IceCreamlist[id - 1] = modifiedice;


                        }
                        else if (option == "2")
                        {
                            Console.Write("Enter number of scoops [1-3]: ");


                            int newscp = Convert.ToInt16(Console.ReadLine());
                            if (newscp > 3)
                            {
                                Console.WriteLine("invalid input");

                            }
                            else
                            {

                                DisplayFlavours(DictFlavour);
                                for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
                                {
                                    Console.Write($"Enter flavour number {scoopIndex} : ");
                                    int newflav = Convert.ToInt32(Console.ReadLine());
                                    Flavour addflact = DictFlavour[newflav];
                                    if (addflact != null)
                                    {
                                        flavlist.Add(addflact);
                                    }
                                    else
                                    {
                                        foreach (var flav in flavlist)
                                        {
                                            if (flav.Type == addflact.Type)
                                            {
                                                flav.Quantity += 1;


                                            }


                                        }







                                    }



                                }
                                result.Flavours = flavlist;
                            }
                        }
                        else if (option == "3")
                        {
                            flavlist.Clear();
                            int count = 1;
                            foreach (var flavour in result.Flavours)
                            {
                                Console.Write($"Enter flavour number {count} : ");
                                count++;
                                int newflav = Convert.ToInt32(Console.ReadLine());
                                Flavour addflact = DictFlavour[newflav];
                                if (addflact != null)
                                {
                                    flavlist.Add(addflact);
                                }
                                else
                                {
                                    foreach (var flav in flavlist)
                                    {
                                        if (flav.Type == addflact.Type)
                                        {
                                            flav.Quantity += 1;


                                        }


                                    }







                                }


                            }
                            result.Flavours = flavlist;

                        }
                        else if (option == "4")
                        {
                            List<Topping> toplist = new List<Topping>();
                            Console.Write("Enter number of toppings: ");


                            int newtop = Convert.ToInt16(Console.ReadLine());
                            DisplayToppings(DictTopping);
                            for (int topIndex = 1; topIndex <= newtop; topIndex++)
                            {
                                Console.Write($"Enter toping number {topIndex} : ");
                                int addtop = Convert.ToInt32(Console.ReadLine());
                                Topping toppingtolist = DictTopping[addtop];
                                toplist.Add(toppingtolist);


                            }
                            result.Toppings = toplist;
                        }


                    }

                
            }
            catch (IndexOutOfRangeException) {
                Console.WriteLine("invalid option");

            }





        }
        public void AddIceCream(IceCream ice)
        {
            IceCreamlist.Add(ice);

        }
        public void DeleteIceCream(int id)
        {
            IceCreamlist.Remove(IceCreamlist[id - 1]);

        }
        public double CalcualteTotal()
        {
            double total = 0;

            foreach (IceCream ice in IceCreamlist)
            {

                double price = ice.CalculatePrice();
                total += price;


            }
            return total;

        }
        public override string ToString()
        {
            return $"{id}{Convert.ToString(timeRecieved)}";
        }
    }

}
