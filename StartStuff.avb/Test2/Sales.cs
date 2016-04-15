using System;

namespace StarStuff.avb.Test2
{
    // покупки
    public class Sales
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public DateTime Date { get; set; }

        public int CustomerId { get; set; }
    }

    // минимальные даты покупки продуктов
    public class ProductMinDaysView
    {
        public int ProductId { get; set; }

        public DateTime Date { get; set; }
    }

    // количество первичных покупок продукта
    public class ProductFirstSalesCountView
    {
        public int ProductId { get; set; }

        public int Count { get; set; }
    }
}
