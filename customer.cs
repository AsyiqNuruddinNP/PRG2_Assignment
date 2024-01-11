﻿//==========================================================
// Student Number : S10257702
// Student Name : Jia Xiang
// Partner Name : Asyiq Nuruddin
//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRG2_Assignment_PointCard;
using PRG2_Assignment_Order;

namespace PRG2_Assignment_Customer
{
    internal class customer
    {
        public string name { get; set; }
        public int memberid { get; set; }
        public DateTime dob { get; set; }
        public Order currentOrder { get; set; }
        public List<Order> orderHistory { get; set; }
        public PointCard rewards { get; set; }
        public customer() { }
        public customer(string nam,int member,DateTime brith) 
        {

            name = nam;
            memberid = member;
            dob = brith;
            orderHistory = new List<Order>();
        
        
        }
        public void MakeOrder()
        {
            // Increment the order ID to make it unique
            int newOrderId = orderHistory.Count + 1;

            // Create a new order with a unique ID and the current timestamp
            currentOrder = new Order(newOrderId, DateTime.Now);

            // Move the current order to order history
            if (currentOrder != null)
            {
                orderHistory.Add(currentOrder);
            }
        }
        public static bool isBirthday() {

            return true;
        }
        public override string ToString()
        {
            return $"{name}{memberid}{dob}";
        }

    }
}
