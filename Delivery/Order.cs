using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveMinyurov
{
    public class Order
    {
        public Order(int _id, double _weight, int _district, DateTime _deliveryTime)
        {
            Id = _id;
            Weight = _weight;
            District = _district;
            DeliveryTime = _deliveryTime;
        }

        // Номер заказа
        public int Id;

        // Вес заказа в килограммах
        public double Weight;

        // Район заказа
        public int District;

        // Время доставки заказа - в формате: yyyy-MM-dd HH:mm:ss.
        public DateTime DeliveryTime;
    }
}
