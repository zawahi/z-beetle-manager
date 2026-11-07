using System;

namespace KuwagataApp.Backend.Models
{
    public class BottleRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = string.Empty; // ユーザーごと
        
        public Guid BeetleId { get; set; } // 紐づく幼虫
        public Beetle? Beetle { get; set; }

        public DateTime ExchangeDate { get; set; } = DateTime.UtcNow; // 交換日・投入日
        public string BottleType { get; set; } = "菌糸ビン"; // 種類(菌糸, マットなど)
        public int Capacity { get; set; } = 800; // 容量(cc)
        
        public decimal? Weight { get; set; } // 交換時の幼虫体重(g)
        public decimal? HeadWidth { get; set; } // 頭幅(mm)
        
        public string? Notes { get; set; } // メモ
    }
}
